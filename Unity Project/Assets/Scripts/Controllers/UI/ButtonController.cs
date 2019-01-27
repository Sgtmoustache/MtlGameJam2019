using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler , IPointerExitHandler, IPointerClickHandler
{
    public Sprite hoverButtonImage;
    public Sprite normalButtonImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = hoverButtonImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = normalButtonImage;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<Button>().enabled = false;
    }
}