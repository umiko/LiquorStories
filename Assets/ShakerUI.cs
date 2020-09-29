using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShakerUI : MonoBehaviour
{
    public GameObject contentBody;
    public GameObject uiTemplate;
    private GameObject tmpUIElement;
    private List<GameObject> uiElements = new List<GameObject>();
    public TextMeshProUGUI ingCount;
    public ProgressBar progressbar;
    private Shaker shaker;

    // Start is called before the first frame update
    private void Start()
    {
        progressbar = gameObject.GetComponentInChildren<ProgressBar>();
        ingCount.text = "0";
        shaker = gameObject.GetComponentInParent<Shaker>();
    }

    public void AddUIElement(Ingredient ingredient, int quantity)
    {
        tmpUIElement = Instantiate(uiTemplate, transform.position, Quaternion.identity);
        tmpUIElement.name = ingredient.Type.ToString();
        tmpUIElement.transform.SetParent(contentBody.transform);

        tmpUIElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ingredient.Type.ToString();
        tmpUIElement.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = quantity + "";
        tmpUIElement.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = ingredient is LiquidIngredient ? "ml" : "st";

        tmpUIElement.transform.localScale = new Vector3(1, 1, 1);
        uiElements.Add(tmpUIElement);
    }

    public void UpdateUIText(Ingredient ingredient)
    {
        foreach (GameObject child in uiElements)
        {
            if (child.name == ingredient.Type.ToString())
            {
                child.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = shaker.GetIngredientAmount(ingredient) + "";
            }
        }
    }

    public void ClearUIText()
    {
        foreach (Transform child in contentBody.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}