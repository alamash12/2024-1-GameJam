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
    public int PersonType; // graphic->0, pd->1, programmer->2, sound -> 3
    public bool isActing = false;
    public int ActingType; // Water->0 , WakeUp->1, CheerUp->2

    public void setHighLight()
    {
        image = GetComponent<Image>();
        // Water->0�϶�
        string _isType="";
        if (PersonType == 0)
        {
            _isType = "graphic";
        }
        else if (PersonType == 1)
        {
            _isType = "pd";
        }
        else if (PersonType == 2)
        {
            _isType = "programer";
        }
        else if (PersonType == 3)
        {
            _isType = "sound";
        }
        else Debug.Log("PersonType����");

        if (ActingType == 0)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_drunk");
        }
        else if (ActingType == 1)
        {// WakeUp->1�϶�,
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_sleep");
        }
        // CheerUp->2�϶�,
        else if (ActingType == 2)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_mental");
        }
        else Debug.Log("Type����");
    }
    public void offHighLight()
    {
            Debug.Log("offHighLight �׽�Ʈ");
            image = GetComponent<Image>();

        // Water->0�϶�
        string _isType = "";
        if (PersonType == 0)
        {
            _isType = "graphic";
        }
        else if (PersonType == 1)
        {
            _isType = "pd";
        }
        else if (PersonType == 2)
        {
            _isType = "programer";
        }
        else if (PersonType == 3)
        {
            _isType = "sound";
        }
        else Debug.Log("PersonType����");

            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_basic");

       
        isActing = false;
        SlotManager._slot.DeleteAvailable(slotPositionX, slotPositionY);
        



    }

    public void SetActingtype(int type)
    {
        ActingType = type;
        isActing = true;
        setHighLight();
    }

    public void NowTmpStudent()
    {
        SlotManager._slot.tmpSlot = this;
    }

}

