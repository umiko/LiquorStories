using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UIElements;

//using UnityEngine.UI;

public class Shaker : MonoBehaviour
{
    private Dictionary<Ingredient, int> ingrediens;

    public GameObject txtTemp = null;
    public GameObject content = null;
    public GameObject txtDrinkName = null;
    private List<GameObject> ingredients_txt = new List<GameObject>();

    public Canvas shakerUI;
    public RectTransform toolTipPanel;
    private Camera mainCamera;

    public bool isCoverAttached;
    private Rigidbody rb;

    private ProgressBar progressbar;
    private float currentTime;
    private float timeToWait = 0;

    private Drink drink;
    public Drink Drink { get; private set; }

    [SerializeField]
    private bool isInHand = false;

    public bool IsInHand { get => isInHand; set => isInHand = value; }

    [SerializeField]
    private bool isDrinkMixed = false;

    public bool IsDrinkMixed { get => isDrinkMixed; set => isDrinkMixed = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        //liquorLibrary = new LiquorLibrary();
        IngredientComparer<Ingredient> ingredientComparer = new IngredientComparer<Ingredient>();
        ingrediens = new Dictionary<Ingredient, int>(ingredientComparer);
        progressbar = shakerUI.gameObject.GetComponentInChildren<ProgressBar>();
        //Ingredient ice = new SolidIngredient(SolidType.Ice);
        //Ingredient ice2 = new SolidIngredient(SolidType.Ice);
        //Ingredient water = new LiquidIngredient(LiquidType.Cola);

        addIngredient(new SolidIngredient(SolidType.Ice), 1);
        addIngredient(new SolidIngredient(SolidType.Lime), 1);
        addIngredient(new SolidIngredient(SolidType.Sugar), 2);
        //addIngredient(new SolidIngredient(SolidType.Sugar), 1);
        addIngredient(new LiquidIngredient(LiquidType.Cachaca), 50);

        //mixDrink();

        //AddUItext("adawd");
        //AddUItext("adwad");
        //AddUItext("adhghhwerr");
    }

    public void AddUItext(Ingredient ingredient, int quantity)
    {
        GameObject tmp = Instantiate(txtTemp, content.transform.position, content.transform.rotation);
        tmp.name = ingredient.Type.ToString();
        tmp.transform.SetParent(content.transform);
        tmp.GetComponent<TMP_Text>().text = ingredient.Type.ToString() + "\t\t" + quantity;
        tmp.GetComponent<TMP_Text>().text += ingredient is LiquidIngredient ? "ml" : "st";

        tmp.GetComponent<RectTransform>().localScale = Vector3.one;

        ingredients_txt.Add(tmp);
    }

    public void UpdateUItext(Ingredient ingredient)
    {
        foreach (GameObject child in ingredients_txt)
        {
            if (child.name == ingredient.Type.ToString())
            {
                //print("jo");
                child.GetComponent<TMP_Text>().text = ingredient.Type + "\t\t" + ingrediens[ingredient];
                child.GetComponent<TMP_Text>().text += ingredient is LiquidIngredient ? "ml" : "st";
            }
        }
    }

    public void addIngredient(Ingredient ingredient, int quantity)
    {
        int value = 0;

        if (ingrediens.TryGetValue(ingredient, out value)) // Fügt einem gespeicherten Ingredient etwas hinzu oder legt es an
        {
            ingrediens[ingredient] += quantity;
            UpdateUItext(ingredient);
        }
        else
        {
            ingrediens.Add(ingredient, quantity);
            AddUItext(ingredient, quantity);
        }

        Debug.Log(ingredient.Type + ": " + ingrediens[ingredient]);
    }

    public void mixDrink()
    {
        //Debug.Log("MixDrink");
        // Search for a Recepie for the ingredients added
        //Drink newDrink = new Drink(DrinkType.Default, ingrediens);
        Drink = new Drink(DrinkType.Default, ingrediens);

        //LiquorLibrary.discoverDrinkType(newDrink);
        // return the Drink if it exists or default Drink
        DrinkType drinkType = LiquorLibrary.discoverDrinkType(Drink);
        Drink.type = drinkType;
        if (drinkType != DrinkType.Default)
        {
            Debug.Log("Success: " + drinkType);
        }
        else
        {
            Debug.Log("Failure: " + drinkType);
        }
        IsDrinkMixed = true;
    }

    //private void OnGUI()
    //{
    //    GUI.Button(new Rect(0, 0, 100, 20), new GUIContent("A Button", "This is the tooltip"));
    //    // If the user hovers the mouse over the button, the global tooltip gets set
    //    GUI.Label(new Rect(0, 40, 100, 40), GUI.tooltip);
    //}

    //public void toggleToolTip()
    //{
    //    toolTipPanel.gameObject.SetActive(toolTipPanel.gameObject.activeSelf ? false : true);
    //}

    private void Update()
    {
        //shakerUI.transform.rotation = mainCamera.transform.rotation; // ist eher störend
        currentTime = Time.deltaTime;

        if (currentTime >= timeToWait)
        {
            shakeDetection();
            timeToWait += 0.15f;
            //Debug.Log("Jetzt");
        }
        else
        {
            timeToWait -= currentTime;
        }

        if (progressbar.IsComplete && !IsDrinkMixed)
        {
            mixDrink();
            progressbar.progressText.text = Drink.type.ToString();
        }
    }

    private void shakeDetection()
    {
        if (!isCoverAttached)
        {
            progressbar.progressText.text = "Put the Lid on";
        }
        else if (ingrediens.Count == 0)
        {
            progressbar.progressText.text = "Add more ingredients";
        }
        else if (rb.velocity.magnitude >= 1)
        {
            float progress = 0.05f;
            //Debug.Log("Shaker velocity" + rb.velocity.magnitude + "  Pr: " + progress);
            progressbar.IncrementProgress(progress);
        }
    }

    public void emptyShaker()
    {
        //reset stuff
        IsDrinkMixed = false;
        ingrediens.Clear();
        Drink.type = DrinkType.Default;
        progressbar.Reset();
        //...
    }
}