using System;
using System.Collections.Generic;

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