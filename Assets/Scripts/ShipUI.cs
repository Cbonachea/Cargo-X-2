using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{

    [SerializeField] Transform shipTransform;
    RectTransform shipUI_Rect;
    Vector2 shipUI;

    private void Update()
    {
        shipUI_Rect.anchoredPosition = shipUI;
    }

}
