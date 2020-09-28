using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIScript : MonoBehaviour
{
    public RectTransform drinkHolder;
    public RectTransform drinRecipe;

    private TextMeshProUGUI[] buttons;
    public TextMeshProUGUI drinkName;
    public TextMeshProUGUI drinkIngredients;

    public bool isFirstPage = true;

    private Drink drink;

    public void tooglePage()
    {
        isFirstPage = !isFirstPage;
        UpdateDrinks();
    }

    private void Awake()
    {
        //Button[] buttons = transform.Find("DrinkHolder").GetComponentsInChildren<Button>();
        buttons = new TextMeshProUGUI[8];
        buttons = drinkHolder.GetComponentsInChildren<TextMeshProUGUI>(true);
        drink = new Drink();
        UpdateDrinks();
    }

    private void UpdateDrinks()
    {
        if (drinkHolder.gameObject.activeSelf)
        {
            // nur die ersten 6 - die anderen sind die seitennavigation
            for (int i = 1; i <= 6; i++)
            {
                //Debug.Log(i + " von " + buttons.Length);
                DrinkType drinkType = isFirstPage ? (DrinkType)i : (DrinkType)(i +6);
                buttons[i - 1].text = drinkType.ToString();
                //Debug.Log(buttons[i - 1].transform.name + " " + drinkType.ToString());
            }
        }
    }

    public void SelectDrink(int index)
    {
        toggleUIPanels();

        if (drinRecipe.gameObject.activeSelf)
        {
            //drink = new Drink(LiquorLibrary.getDrink(isFirstPage ? (DrinkType)index : (DrinkType)(index * 2)));
            DrinkType drinkType = isFirstPage ? (DrinkType)index : (DrinkType)(index +6);
            drink = LiquorLibrary.getDrink(drinkType);
            drinkName.text = drink.type.ToString();
            drinkIngredients.text = drink.ToString();

            Debug.Log(drinkType + drink.ToString());
        }
    }

    public void toggleUIPanels()
    {
        drinkHolder.gameObject.SetActive(drinkHolder.gameObject.activeSelf ? false : true);
        drinRecipe.gameObject.SetActive(drinRecipe.gameObject.activeSelf ? false : true);
    }
}