using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmissionPad : MonoBehaviour
{
    private DrinkType targetDrinkType;

    private Canvas padUI;
    private GameObject orderPanel;
    private GameObject cooldownPanel;

    public TextMeshProUGUI orderText;
    public TextMeshProUGUI statusText;

    private Image panelImage;
    private RectTransform cooldownRect;
    private Color lerpedColor;
    public Color successColor;
    public Color failureColor;
    public Color lerp1;
    public Color lerp2;

    public Material pendingMaterial;
    public Material cooldownMaterial;
    public Material successMaterial;
    public Material failureMaterial;

    private Renderer matRenderer;
    private Shaker shaker;

    private Order order;
    private System.Random random;

    public float rotateSpeed = 40;

    // Start is called before the first frame update
    private void Start()
    {
        padUI = gameObject.GetComponentInChildren<Canvas>();
        orderPanel = padUI.transform.Find("OrderPanel").gameObject;
        cooldownPanel = padUI.transform.Find("CooldownPanel").gameObject;

        matRenderer = transform.Find("Trigger").GetComponent<Renderer>();

        panelImage = orderPanel.GetComponent<Image>();
        cooldownRect = cooldownPanel.transform.GetChild(0).GetComponent<RectTransform>();

        random = new System.Random();
        random.Next(0, 10);
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
                    }
                    else
                    {
                        updateUI(failureMaterial, order.DrinkTypeOrdered, Status.Failure);
                    }
                    StartCoroutine("ResetPad");
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
        //order.DrinkTypeOrdered = (DrinkType)random.Next(1, drinkTypeCount);
        //test
        order.DrinkTypeOrdered = DrinkType.Caipirinha;
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
        StartCoroutine("Cooldown");
    }
}