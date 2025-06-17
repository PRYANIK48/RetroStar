using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class HoverTextChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText; 
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.text = "СОЗДАЙТЕ МИР...";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.text = "НЕ ЗАГРУЖЕНО";
    }
}
