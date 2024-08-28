using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text buttonText;

    private void Start()
    {
        buttonText = gameObject.GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontStyle = FontStyle.Normal;
    }
}
