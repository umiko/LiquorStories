using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIScript : MonoBehaviour
{
    public RectTransform drinkHolder;
    public RectTransform drinRecipe;

    private Button[] buttons;
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
        buttons = new Button[8];
        buttons = drinkHolder.GetComponentsInChildren<Button>(true);
        drink = new Drink();
        UpdateDrinks();

        SelectDrink(1);
        SelectDrink(2);
        SelectDrink(3);
        SelectDrink(4);
        SelectDrink(5);
    }

    private void UpdateDrinks()
    {
        if (drinkHolder.gameObject.activeSelf)
        {
            // nur die ersten 6 - die anderen sind die seitennavigation
            for (int i = 1; i <= 6; i++)
            {
                //Debug.Log(i + " von " + buttons.Length);
                DrinkType drinkType = isFirstPage ? (DrinkType)i : (DrinkType)(i * 2);
                buttons[i - 1].GetComponentInChildren<TextMeshProUGUI>().text = drinkType.ToString();
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
            DrinkType drinkType = isFirstPage ? (DrinkType)index : (DrinkType)(index * 2);
            drink = LiquorLibrary.getDrink(drinkType);
            drinkName.text = drink.type.ToString();
            drinkIngredients.text = drink.ToString();

            Debug.Log(drinkType + drink.ToString());
        }
    }

    public void toggleUIPanels()
    {
        //Debug.Log(drinkHolder.gameObject.activeSelf + "drinkHolder");
        //drinkHolder.gameObject.SetActive(!drinkHolder.gameObject.activeSelf);
        //Debug.Log(drinkHolder.gameObject.activeSelf + "drinkHolder");
        //Debug.Log(drinkIngredients.gameObject.activeSelf + "drinkIngredients");
        //drinkIngredients.gameObject.SetActive(!drinkIngredients.gameObject.activeSelf);
        //Debug.Log(drinkIngredients.gameObject.activeSelf + "drinkIngredients");

        drinkHolder.gameObject.SetActive(false);
        drinRecipe.gameObject.SetActive(true);
    }
}