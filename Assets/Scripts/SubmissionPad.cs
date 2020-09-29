using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SubmissionPad : MonoBehaviour
{
    private DrinkType targetDrinkType;

    private Canvas padUI;
    private GameObject orderPanel;
    private GameObject cooldownPanel;
    private IncrementScore scoreBoard;
    
    public TextMeshProUGUI orderText;
    public TextMeshProUGUI statusText;

    private Image panelImage;
    private RectTransform cooldownRect;

    public Material pendingMaterial;
    public Material cooldownMaterial;
    public Material successMaterial;
    public Material failureMaterial;

    private Renderer matRenderer;
    private Shaker shaker;

    private Order order;

    public float rotateSpeed = 40;

    // Start is called before the first frame update
    private void Start()
    {
        padUI = gameObject.GetComponentInChildren<Canvas>();
        orderPanel = padUI.transform.Find("OrderPanel").gameObject;
        cooldownPanel = padUI.transform.Find("CooldownPanel").gameObject;
        scoreBoard = GameObject.FindWithTag("ScoreBoard").GetComponent<IncrementScore>();
        matRenderer = transform.Find("Trigger").GetComponent<Renderer>();

        panelImage = orderPanel.GetComponent<Image>();
        cooldownRect = cooldownPanel.transform.GetChild(0).GetComponent<RectTransform>();

        order = new Order(DrinkType.Default);
        StartCoroutine("Cooldown");
    }

    private void Update()
    {
        if (cooldownPanel.activeSelf)
        {
            cooldownRect.Rotate(0f, 0f, -(rotateSpeed * Time.deltaTime));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.name);
        if (other.gameObject.name == "Shaker_Mesh")
        {
            shaker = other.GetComponentInParent<Shaker>();

            if (!shaker.IsInHand)
            {
                if (shaker.IsDrinkMixed)
                {
                    if (order.DrinkTypeOrdered == shaker.Drink.type)
                    {
                        updateUI(successMaterial, order.DrinkTypeOrdered, Status.Success);
                        scoreBoard.Increment();
                    }
                    else
                    {
                        updateUI(failureMaterial, order.DrinkTypeOrdered, Status.Failure);
                    }

                    shaker.emptyShaker();
                    StartCoroutine(nameof(ResetPad));
                }
                else
                {
                    Debug.Log("Drink has to be mixed");
                }
            }
        }
    }

    private void updateUI(Material material, DrinkType drinkType, Status status)
    {
        matRenderer.material = material;
        orderText.text = drinkType.ToString();
        statusText.text = status.ToString();
        
    }

    private void updateUI(Material material, Order order)
    {
        updateUI(material, order.DrinkTypeOrdered, order.Status);
    }

    private void newOrder()
    {
        int drinkTypeCount = Enum.GetNames(typeof(DrinkType)).Length;
        order.DrinkTypeOrdered = (DrinkType)Random.Range(1, drinkTypeCount);
        //test
        //order.DrinkTypeOrdered = DrinkType.Caipirinha;
        order.Status = Status.Pending;

        updateUI(pendingMaterial, order);
    }

    private IEnumerator Cooldown()
    {
        matRenderer.material = cooldownMaterial;
        cooldownPanel.SetActive(true);
        orderPanel.SetActive(false);
        yield return new WaitForSeconds(5f);
        cooldownPanel.SetActive(false);
        orderPanel.SetActive(true);

        newOrder();
        yield return null;
    }

    private IEnumerator ResetPad()
    {
        yield return new WaitForSeconds(6f);
        shaker.emptyShaker();
        StartCoroutine(nameof(Cooldown));
    }
}