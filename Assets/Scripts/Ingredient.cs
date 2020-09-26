using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ingredient
{
    public abstract Enum Type
    {
        get;
    }
}

public enum SolidType
{
    Ice,
    LemonSlice,
    Mint,
    Lime,
    Sugar,
    Cherry,
    OrangeSlice
}

public enum LiquidType
{
    Cola,
    Rum,
    Vodka,
    Gin,
    TonicWater,
    Tequila,
    OrangeJuice,
    Grenadine,
    Cachaca,
    LimeJuice,
    SimpleSyrup,
    Soda,
    TripleSec,
    LemonJuice,
    GommeSyrup,
    CranberryJuice,
    PineappleJuice,
    CoconutCream,
    Cream,
    Vermouth,
    Angostura,
    Cointreau,
    Whiskey
}

public static class LiquidColour
{
    private static Dictionary<LiquidType, Color> liquidColors = new Dictionary<LiquidType, Color>
    {
        {LiquidType.Cola,           new Color( 60 , 48 , 36, 1 ) },
        {LiquidType.Rum,            new Color( 131 , 95 , 83, 1 ) },
        {LiquidType.Vodka,          new Color( 191 , 192 , 238, 1 ) },
        {LiquidType.Gin,            new Color( 217, 228, 201, 1 ) },
        {LiquidType.TonicWater,     new Color( 113, 146, 153, 1 ) },
        {LiquidType.Tequila,        new Color( 244 , 208 , 164, 1 ) },
        {LiquidType.OrangeJuice,    new Color( 255 , 127 , 0, 1 ) },
        {LiquidType.Grenadine,      new Color( 249 , 103 , 122, 1 ) },
        {LiquidType.Cachaca,        new Color( 240, 248, 255, 1 ) },
        {LiquidType.LimeJuice,      new Color( 158 , 253 , 56, 1 ) },
        {LiquidType.SimpleSyrup,    new Color( 240, 248, 255, 1 ) },
        {LiquidType.Soda,           new Color( 240, 248, 255, 1 ) },
        {LiquidType.TripleSec,      new Color( 254 , 161 , 125, 1 ) },
        {LiquidType.LemonJuice,     new Color( 252 , 233 , 3, 1 ) },
        {LiquidType.GommeSyrup,     new Color( 240 , 184 , 122, 1 ) },
        {LiquidType.CranberryJuice, new Color( 135 , 65 , 97, 1 ) },
        {LiquidType.PineappleJuice, new Color( 243 , 214 , 79, 1 ) },
        {LiquidType.CoconutCream,   new Color( 254 , 9 , 0, 1 ) },
        {LiquidType.Cream,          new Color( 241 , 238 , 230, 1 ) },
        {LiquidType.Vermouth,       new Color( 18 , 118 , 10, 1 ) },
        {LiquidType.Angostura,      new Color( 241 , 250 , 206, 1 ) },
        {LiquidType.Cointreau,      new Color( 164 , 89 , 61, 1 ) },
    };

    public static Color getLiquidColor(LiquidType liquidType)
    {
        return liquidColors[liquidType];
    }
}

public class IngredientComparer<Ingredient> : IEqualityComparer<Ingredient>
{
    public bool Equals(Ingredient x, Ingredient y)
    {
        return x.Equals(y);
    }

    public int GetHashCode(Ingredient obj)
    {
        return 0;
    }
}

public class SolidIngredient : Ingredient
{
    private SolidType type;

    public SolidIngredient(SolidType type)
    {
        this.type = type;
    }

    public override Enum Type
    {
        get
        {
            return this.type;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is SolidIngredient)
        {
            return this.type == ((SolidIngredient)obj).type;
        }
        return base.Equals(obj);
    }
}

public class LiquidIngredient : Ingredient
{
    private LiquidType type;

    public LiquidIngredient(LiquidType type)
    {
        this.type = type;
    }

    public override Enum Type
    {
        get
        {
            return this.type;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is LiquidIngredient)
        {
            return this.type == ((LiquidIngredient)obj).type;
        }
        return base.Equals(obj);
    }
}