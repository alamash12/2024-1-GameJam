using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : MonoBehaviour
{
    // 술마시기 -> 물뿌리기
    // 잠 -> 깨우기
    // 멘붕 -> 격려
    public RectTransform position;
    private Image image;
    public int slotPositionX, slotPositionY;
    public Slot motherSlot;

    public bool isActing = false;
    public int ActingType; // Water->0 , WakeUp->1, CheerUp->2

    public void setHighLight()
    {
            image = GetComponent<Image>();
        // Water->0일때
        if (ActingType == 0)
        {
            image.color = new Color(243f / 255f, 237f / 255f, 35f / 255f, 255f / 255f); // RGB는 0~1값만 받으므로, 255로 나눠주어야함.
        }
        else if (ActingType == 1)
        {// WakeUp->1일때,
            image.color = new Color(255f / 255f, 10f / 255f, 10f / 255f, 255f / 255f); // RGB는 0~1값만 받으므로, 255로 나눠주어야함.
        }
        // CheerUp->2일때,
        else if (ActingType == 2)
        {
            image.color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f); // RGB는 0~1값만 받으므로, 255로 나눠주어야함.
        }
        else Debug.Log("Type오류");

    }
    public void offHighLight()
    {
            Debug.Log("offHighLight 테스트");
            image = GetComponent<Image>();
            image.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 100f / 255f);
        isActing = false;
    }

    public void SetActingtype(int type)
    {
        ActingType = type;
        isActing = true;
        setHighLight();
    }

    public void NowTmpStudent()
    {
        Managers.Slot.tmpSlot = this;
    }

}

