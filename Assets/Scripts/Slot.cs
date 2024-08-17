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
    public Animator padeAnim;
    public RectTransform position;
    private Image image;
    public int slotPositionX, slotPositionY;
    public Slot CancleSlot;
    public int PersonType; // graphic->0, pd->1, programmer->2, sound -> 3
    public bool isActing = false;
    public int ActingType; // Water->0 , WakeUp->1, CheerUp->2, Basic->3

    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void setHighLight()
    {
        image = GetComponent<Image>();
        // Water->0일때
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
        else Debug.Log("PersonType오류");

        if (ActingType == 0)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_drunk");
        }
        else if (ActingType == 1)
        {// WakeUp->1일때,
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_sleep");
        }
        // CheerUp->2일때,
        else if (ActingType == 2)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_mental");
        }
        else Debug.Log("Type오류");
    }
    public void offHighLight()
    {
        Managers.Data.ScoreIncrease();
        Debug.Log("offHighLight 테스트");
            image = GetComponent<Image>();

        // Water->0일때
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
        else Debug.Log("PersonType오류");

        StartCoroutine(ActToBasic(_isType));


    }

    IEnumerator ActToBasic(string _isType)
    {
        
        if (ActingType == 0)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_drunk_effect");
        }
        else if (ActingType == 1)
        {// WakeUp->1일때,
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_sleep_effect");
        }
        // CheerUp->2일때,
        else if (ActingType == 2)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_mental_effect");
        }

        yield return new WaitForSeconds(0.4f);
        image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_isType}_basic");
        isActing = false;
        SlotManager._slot.DeleteAvailable(slotPositionX, slotPositionY);
        yield return null;
    }
    public void SetActingtype(int type)
    {
        ActingType = type;
        isActing = true;
        setHighLight();
    }

    public void NowTmpStudent()
    {
        string _isType = "";
        string _wasType = "";
        if (SlotManager._slot.tmpSlot!=null)
        {
            CancleSlot = SlotManager._slot.tmpSlot;
            image = CancleSlot.GetComponent<Image>();
            if (CancleSlot.PersonType == 0)
            {
                _wasType = "graphic";
            }
            else if (CancleSlot.PersonType == 1)
            {
                _wasType = "pd";
            }
            else if (CancleSlot.PersonType == 2)
            {
                _wasType = "programer";
            }
            else if (CancleSlot.PersonType == 3)
            {
                _wasType = "sound";
            }


            if (CancleSlot.isActing == false)
            {
                image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_wasType}_basic");
            }
            else
            {
                if (CancleSlot.ActingType == 0)
                {
                    image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_wasType}_drunk");
                }
                else if (CancleSlot.ActingType == 1)
                {// WakeUp->1일때,
                    image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_wasType}_sleep");
                }
                // CheerUp->2일때,
                else if (CancleSlot.ActingType == 2)
                {
                    image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Character_{_wasType}_mental");
                }
            }
        }


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
        else Debug.Log("PersonType오류");

        SlotManager._slot.tmpSlot = this;
        image = GetComponent<Image>();


        if (isActing==false)
        {
            image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Selected/Character_{_isType}_basic_select");
        }
        else
        {
            if (ActingType == 0)
            {
                image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Selected/Character_{_isType}_drunk_select");
            }
            else if (ActingType == 1)
            {// WakeUp->1일때,
                image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Selected/Character_{_isType}_sleep_select");
            }
            // CheerUp->2일때,
            else if (ActingType == 2)
            {
                image.sprite = Managers.Resource.Load<Sprite>($"Assets/Character/Selected/Character_{_isType}_mental_select");
            }
        }
       

    }

}

