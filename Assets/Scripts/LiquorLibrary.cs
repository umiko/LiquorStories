using System.Collections.Generic;
using System;
using UnityEngine;

public class LiquorLibrary : MonoBehaviour
{
    private static List<Drink> recipes = new List<Drink>();

    public void Awake()
    {
        addRecipe(DrinkType.TequilaSunrise,
            Pairing.Of(SolidType.OrangeSlice, 1),
            Pairing.Of(LiquidType.Tequila, 45),
            Pairing.Of(LiquidType.OrangeJuice, 90),
             Pairing.Of(LiquidType.Grenadine, 15)
        );

        addRecipe(DrinkType.Caipirinha,
            Pairing.Of(SolidType.Ice, 1),
            Pairing.Of(SolidType.Lime, 1),
            Pairing.Of(SolidType.Sugar, 2),
            Pairing.Of(LiquidType.Cachaca, 50)
        );

        addRecipe(DrinkType.Mojito,
            Pairing.Of(SolidType.Ice, 1),
            Pairing.Of(SolidType.Mint, 3),
            Pairing.Of(SolidType.Sugar, 2),
            Pairing.Of(LiquidType.Rum, 60),
            Pairing.Of(LiquidType.LimeJuice, 30),
            Pairing.Of(LiquidType.SimpleSyrup, 20),
            Pairing.Of(LiquidType.Soda, 20)
        );

        addRecipe(DrinkType.LongIslandIcedTea,
            Pairing.Of(SolidType.Ice, 1),
            Pairing.Of(SolidType.LemonSlice, 1),
            Pairing.Of(LiquidType.Tequila, 15),
            Pairing.Of(LiquidType.Vodka, 15),
            Pairing.Of(LiquidType.TripleSec, 15),
            Pairing.Of(LiquidType.Gin, 15),
            Pairing.Of(LiquidType.LemonJuice, 25),
            Pairing.Of(LiquidType.GommeSyrup, 30),
            Pairing.Of(LiquidType.Cola, 10)
        );

        addRecipe(DrinkType.Cosmopolitan,
            Pairing.Of(SolidType.Lime, 1),
            Pairing.Of(LiquidType.Vodka, 20),
            Pairing.Of(LiquidType.LimeJuice, 10),
            Pairing.Of(LiquidType.TripleSec, 10),
            Pairing.Of(LiquidType.Cointreau, 10),
            Pairing.Of(LiquidType.CranberryJuice, 20)
        );

        addRecipe(DrinkType.PinaColada,
           Pairing.Of(SolidType.Ice, 1),
           Pairing.Of(SolidType.Cherry, 1),
           Pairing.Of(LiquidType.Rum, 30),
           Pairing.Of(LiquidType.CoconutCream, 40),
           Pairing.Of(LiquidType.PineappleJuice, 90),
           Pairing.Of(LiquidType.Cream, 10)
       );

        addRecipe(DrinkType.CubraLibre,
            Pairing.Of(SolidType.Ice, 1),
            Pairing.Of(SolidType.Lime, 1),
            Pairing.Of(LiquidType.Rum, 50),
            Pairing.Of(LiquidType.Cola, 120),
            Pairing.Of(LiquidType.LimeJuice, 10)
        );

        addRecipe(DrinkType.Magarita,
             Pairing.Of(SolidType.Ice, 1),
             Pairing.Of(SolidType.Lime, 1),
             Pairing.Of(LiquidType.Tequila, 40),
             Pairing.Of(LiquidType.Cointreau, 20),
             Pairing.Of(LiquidType.LimeJuice, 20),
             Pairing.Of(LiquidType.SimpleSyrup, 10)
         );

        addRecipe(DrinkType.Daiquiri,
             Pairing.Of(SolidType.Ice, 1),
             Pairing.Of(SolidType.Lime, 1),
             Pairing.Of(LiquidType.Rum, 45),
             Pairing.Of(LiquidType.LimeJuice, 25),
             Pairing.Of(LiquidType.SimpleSyrup, 15)
         );

        //addRecipe(DrinkType.Manhatten,
        //    Pairing.Of(SolidType.Cherry, 1),
        //    Pairing.Of(LiquidType.Whiskey, 40),
        //    Pairing.Of(LiquidType.Vermouth, 20),
        //    Pairing.Of(LiquidType.Angostura, 20)
        //);

        //print();
    }

    public static DrinkType discoverDrinkType(Drink drinkToDiscover)
    {
        //Debug.Log("DiscoverDrinkType");
        DrinkType drinkType = DrinkType.Default;

        foreach (Drink drink in recipes)
        {
            if (drink.Equals(drinkToDiscover))
            {
                return drink.type;
            }
        }
        return drinkType;
    }

    public static Drink getDrink(DrinkType type)
    {
        foreach (Drink drink in recipes)
        {
            if (drink.type.Equals(type))
            {
                return drink;
            }
        }

        return new Drink(DrinkType.Default, new Dictionary<Ingredient, int>());
    }

    public static void addRecipe(DrinkType type, params KeyValuePair<Enum, int>[] pairs)
    {
        Dictionary<Ingredient, int> dictionary = new Dictionary<Ingredient, int>();

        try
        {
            //Debug.Log("Add " + type);
            for (int i = 0; i < pairs.Length; i++)
            {
                //Debug.Log(pairs[i].Key + " ");
                if (pairs[i].Key is SolidType)
                {
                    dictionary.Add(new SolidIngredient((SolidType)pairs[i].Key), pairs[i].Value);
                }
                else if (pairs[i].Key is LiquidType)
                {
                    dictionary.Add(new LiquidIngredient((LiquidType)pairs[i].Key), pairs[i].Value);
                }
            }

            //Check if a drink with the same name and/ or ingredient list allready exists
            Drink newDrink = new Drink(type, dictionary);

            foreach (Drink drink in recipes)
            {
                if (drink.Equals(newDrink))
                {
                    Debug.LogWarning("Drink already exists in LiquorLibrary");
                    return;
                }
            }
            recipes.Add(newDrink);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    private static class Pairing
    {
        public static KeyValuePair<Enum, int> Of(Enum key, int value)
        {
            return new KeyValuePair<Enum, int>(key, value);
        }
    }

    public static void print()
    {
        foreach (Drink drink in recipes)
        {
            Debug.Log(drink.ToString());
        }
    }
}