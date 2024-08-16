using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : MonoBehaviour
{
    public RectTransform position;
    private Image image;
    public int slotPositionX, slotPositionY;
    public Slot motherSlot;

    public bool isActing;

    public void setHighLight()
    {
            image = GetComponent<Image>();
            image.color = new Color(243f/255f, 237f/255f, 35f/255f, 255f/255f); // RGB는 0~1값만 받으므로, 255로 나눠주어야함.
    }
    public void offHighLight()
    {
        Debug.Log("offHighLight 테스트");
        image = GetComponent<Image>();
        image.color = new Color(255f/255f, 255f/255f, 255f/255f, 100f/255f);
    }


}

