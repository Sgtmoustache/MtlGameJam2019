using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour, IPointerEnterHandler
{
    public Sprite hoverButtonImage;
    public Sprite normalButtonImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //GetComponent<Button>().targetGraphic = hoverButtonImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //GetComponent<Button>().targetGraphic = normalButtonImage;
    }
}