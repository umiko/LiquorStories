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
    private Dictionary<Ingredient, int> ingredients;
    private List<GameObject> ingredients_txt = new List<GameObject>();

    public bool isCoverAttached;
    private Rigidbody rb;
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

    private ShakerUI shakerUI;
    private Camera mainCamera;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        IngredientComparer<Ingredient> ingredientComparer = new IngredientComparer<Ingredient>();
        ingredients = new Dictionary<Ingredient, int>(ingredientComparer);

        mainCamera = Camera.main;
        shakerUI = gameObject.GetComponentInChildren<ShakerUI>();

        //addIngredient(new SolidIngredient(SolidType.Ice), 1);
        //addIngredient(new SolidIngredient(SolidType.Lime), 1);
        //addIngredient(new SolidIngredient(SolidType.Sugar), 2);
        //addIngredient(new SolidIngredient(SolidType.Sugar), 1);
        //addIngredient(new LiquidIngredient(LiquidType.Cachaca), 50);
    }

    public void addIngredient(Ingredient ingredient, int quantity)
    {
        int value = 0;

        if (ingredients.TryGetValue(ingredient, out value)) // Fügt einem gespeicherten Ingredient etwas hinzu oder legt es an
        {
            ingredients[ingredient] += quantity;
            shakerUI.UpdateUIText(ingredient);
        }
        else
        {
            ingredients.Add(ingredient, quantity);
            shakerUI.AddUIElement(ingredient, quantity);
        }

        //Debug.Log(ingredient.Type + ": " + ingredients[ingredient]);
    }

    public void mixDrink()
    {
        // Search for a Recepie for the ingredients added
        Drink = new Drink(DrinkType.Default, ingredients);

        // return the Drink if it exists or default Drink
        DrinkType drinkType = LiquorLibrary.discoverDrinkType(Drink);
        Drink.type = drinkType;
        //if (drinkType != DrinkType.Default)
        //{
        //    Debug.Log("Success: " + drinkType);
        //}
        //else
        //{
        //    Debug.Log("Failure: " + drinkType);
        //}
        IsDrinkMixed = true;
    }

    private void Update()
    {
        //shakerUI.transform.rotation = mainCamera.transform.rotation; // ist eher störend
        if (mainCamera)
        {
            // shakerUI.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.y, 0);
            shakerUI.transform.rotation = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0);
        }

        currentTime = Time.deltaTime;

        if (currentTime >= timeToWait)
        {
            shakeDetection();
            timeToWait += 0.15f;
        }
        else
        {
            timeToWait -= currentTime;
        }

        if (shakerUI.progressbar.IsComplete && !IsDrinkMixed)
        {
            mixDrink();
            shakerUI.progressbar.progressText.text = Drink.type.ToString();
        }
    }

    private void shakeDetection()
    {
        if (!isCoverAttached)
        {
            shakerUI.progressbar.progressText.text = "Put the Lid on";
        }
        else if (ingredients.Count == 0)
        {
            shakerUI.progressbar.progressText.text = "Add more ingredients";
        }
        else if (rb.velocity.magnitude >= 1)
        {
            float progress = 0.05f;
            //Debug.Log("Shaker velocity" + rb.velocity.magnitude + "  Pr: " + progress);
            shakerUI.progressbar.IncrementProgress(progress);
        }
    }

    public void emptyShaker()
    {
        //reset stuff
        IsDrinkMixed = false;
        ingredients.Clear();
        ingredients_txt.Clear();
        if (Drink != null)
        {
            Drink.type = DrinkType.Default;
        }

        shakerUI.progressbar.Reset();
        shakerUI.ClearUIText();
        //...
    }

    public int GetIngredientAmount(Ingredient ingredient)
    {
        return ingredients[ingredient];
    }
}