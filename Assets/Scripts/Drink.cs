using System;
using System.Collections.Generic;
using UnityEngine;

public enum DrinkType
{
    Default,
    RumAndCoke,
    GinTonic,
    FlyingHirsch,
    TequilaSunrise,
    Caipirinha,
    Mojito,
    LongIslandIcedTea,
    Cosmopolitan,
    PinaColada,
    CubraLibre,
    Magarita,
    Daiquiri,
    Manhatten
}

public class Drink : DrinkComparer
{
    public DrinkType type;

    //public Dictionary<Ingredient, int> ingrediens;
    public IngredientDictionary<Ingredient, int> ingredienstDic;

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
        string output = "" + type;
        foreach (KeyValuePair<Ingredient, int> entry in ingredienstDic.Ingrediens)
        {
            output += "\n" + entry.Key.Type + " " + entry.Value;
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