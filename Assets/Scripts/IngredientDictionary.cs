using System.Collections.Generic;

public class IngredientDictionary<TKey, TValue>
{
    private Dictionary<Ingredient, int> ingrediens;

    public Dictionary<Ingredient, int> Ingrediens { get { return ingrediens; } set { ingrediens = value; } }
    public int Count { get { return ingrediens.Count; } }

    public IngredientDictionary(Dictionary<Ingredient, int> dictionary)
    {
        //this.ingrediens = dictionary;
        ingrediens = new Dictionary<Ingredient, int>(dictionary);
    }

    public bool Equals(IngredientDictionary<Ingredient, int> compareDictionary)
    {
        if (ingrediens.Count != compareDictionary.Count)
        {
            //Debug.Log("IngrDic - ingr count does not match " + ingrediens.Count + " != " + compareDictionary.Count);
            return false;
        }

        int matchingIngredients = 0;

        foreach (KeyValuePair<Ingredient, int> x_ingredient in ingrediens)
        {
            foreach (KeyValuePair<Ingredient, int> y_ingredient in compareDictionary.ingrediens)
            {
                if (x_ingredient.Key is SolidIngredient && y_ingredient.Key is SolidIngredient || x_ingredient.Key is LiquidIngredient && y_ingredient.Key is LiquidIngredient)
                {
                    //Debug.Log("IngrDic - compare: " + x_ingredient.Key.Type + "? " + y_ingredient.Key.Type);
                    if (x_ingredient.Key.Equals(y_ingredient.Key))
                    {
                        //Compare quantitys
                        //...
                        matchingIngredients++;
                        break;
                    }
                }
            }

            if (ingrediens.Count == matchingIngredients)
            {
                //Debug.Log("IngrDic - all ingr match");
                return true;
            }
        }
        //Debug.Log("IngrDic - ingr missmatch - " + matchingIngredients + " matches");
        return false;
    }
}