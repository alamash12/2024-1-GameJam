using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Slot : MonoBehaviour
{
    // �����ñ� -> ���Ѹ���
    // �� -> �����
    // ��� -> �ݷ�
    public RectTransform position;
    private Image image;
    public int slotPositionX, slotPositionY;
    public Slot motherSlot;

    public bool isActing = false;
    public int ActingType; // Water->0 , WakeUp->1, CheerUp->2

    public void setHighLight()
    {
            image = GetComponent<Image>();
        // Water->0�϶�
        if (ActingType == 0)
        {
            image.color = new Color(243f / 255f, 237f / 255f, 35f / 255f, 255f / 255f); // RGB�� 0~1���� �����Ƿ�, 255�� �����־����.
        }
        else if (ActingType == 1)
        {// WakeUp->1�϶�,
            image.color = new Color(255f / 255f, 10f / 255f, 10f / 255f, 255f / 255f); // RGB�� 0~1���� �����Ƿ�, 255�� �����־����.
        }
        // CheerUp->2�϶�,
        else if (ActingType == 2)
        {
            image.color = new Color(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f); // RGB�� 0~1���� �����Ƿ�, 255�� �����־����.
        }
        else Debug.Log("Type����");

    }
    public void offHighLight()
    {
            Debug.Log("offHighLight �׽�Ʈ");
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

