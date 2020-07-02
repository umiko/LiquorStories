using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    private Dictionary<Ingredient, int> ingrediens;
    //private LiquorLibrary liquorLibrary;

    private void Start()
    {
        //liquorLibrary = new LiquorLibrary();
        IngredientComparer<Ingredient> ingredientComparer = new IngredientComparer<Ingredient>();
        ingrediens = new Dictionary<Ingredient, int>(ingredientComparer);

        //Ingredient ice = new SolidIngredient(SolidType.Ice);
        //Ingredient ice2 = new SolidIngredient(SolidType.Ice);
        //Ingredient water = new LiquidIngredient(LiquidType.Cola);

        addIngredient(new SolidIngredient(SolidType.Ice), 1);
        addIngredient(new SolidIngredient(SolidType.Lime), 1);
        addIngredient(new SolidIngredient(SolidType.Sugar), 1);
        addIngredient(new SolidIngredient(SolidType.Sugar), 1);
        addIngredient(new LiquidIngredient(LiquidType.Cachaca), 50);

        mixDrink();
    }

    public void addIngredient(Ingredient ingredient, int quantity)
    {
        int value = 0;

        if (ingrediens.TryGetValue(ingredient, out value)) // Fügt einem gespeicherten Ingredient etwas hinzu oder legt es an
        {
            ingrediens[ingredient] += quantity;
        }
        else
        {
            ingrediens.Add(ingredient, quantity);
        }

        Debug.Log(ingredient.Type + ": " + ingrediens[ingredient]);
    }

    public void mixDrink()
    {
        //Debug.Log("MixDrink");
        // Search for a Recepie for the ingredients added
        Drink newDrink = new Drink(DrinkType.Default, ingrediens);

        //LiquorLibrary.discoverDrinkType(newDrink);
        // return the Drink if it exists or default Drink
        DrinkType drinkType = LiquorLibrary.discoverDrinkType(newDrink);
        if (drinkType != DrinkType.Default)
        {
            Debug.Log("Success: " + drinkType);
        }
        else
        {
            Debug.Log("Failure: " + drinkType);
        }

        //return new Drink();
    }
}