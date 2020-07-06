using UnityEngine;
using UnityEngine.EventSystems;

public class MyClass : MonoBehaviour, IPointerEnterHandler
{
    public GameObject tooltip = null;

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("hover on");
        if (!tooltip.activeSelf)
        {
            tooltip.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("hover off");
        if (tooltip.activeSelf)
        {
            tooltip.SetActive(false);
        }
    }
}