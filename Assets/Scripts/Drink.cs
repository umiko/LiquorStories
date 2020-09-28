using System;
using System.Collections.Generic;
using UnityEngine;

public enum DrinkType
{
    Default = 0,
    RumAndCoke = 1,
    GinTonic = 2,

    //FlyingHirsch = 3,
    Manhatten = 3,

    TequilaSunrise = 4,
    Caipirinha = 5,
    Mojito = 6,
    LongIslandIcedTea = 7,
    Cosmopolitan = 8,
    PinaColada = 9,
    CubraLibre = 10,
    Magarita = 11,
    Daiquiri = 12
}

public class Drink : DrinkComparer
{
    public DrinkType type;

    //public Dictionary<Ingredient, int> ingrediens;
    public IngredientDictionary<Ingredient, int> ingredienstDic;

    public Drink(Drink drink)
    {
        type = drink.type;
        ingredienstDic = drink.ingredienstDic;
    }

    public Drink()
    {
    }

    public Drink(DrinkType type, Dictionary<Ingredient, int> ingrediens)
    {
        this.type = type;
        this.ingredienstDic = new IngredientDictionary<Ingredient, int>(ingrediens);
    }

    public bool Equals(Drink drink)
    {
        //Debug.Log("Drink equals method");
        return base.Equals(this, drink);
    }

    public override string ToString()
    {
        string output = "";
        foreach (KeyValuePair<Ingredient, int> entry in ingredienstDic.Ingrediens)
        {
            output +=  entry.Key.Type + " " + entry.Value + "\n";
        }
        return output;
    }
}

public class DrinkComparer : IEqualityComparer<Drink>
{
    public bool Equals(Drink x, Drink y)
    {
        //Debug.Log("DrinkEquals: " + x.type + " ? " + y.type);
        bool drinksAreEqual = false;

        // Compare types if it is not the default one
        if (x.type != DrinkType.Default && y.type != DrinkType.Default && x.type == y.type)
        {
            //Debug.Log("names are equal");
            drinksAreEqual = true;
        }

        // Compare ingredients
        //Debug.Log("Compare ingredients");
        if (x.ingredienstDic.Equals(y.ingredienstDic))
        {
            //Debug.Log("Ingredients are equal");
            drinksAreEqual = true;
        }

        return drinksAreEqual;
    }

    public int GetHashCode(Drink obj)
    {
        //Debug.Log("DrinkEquals: hash");
        return 0;
    }
}