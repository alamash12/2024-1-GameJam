using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class MainGame_UI : UI_Popup
{
    enum Buttons
    {
        Water,
        WakeUp,
        CheerUp,
    }

    enum Texts
    {
        Score,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        GetButton((int)(Buttons.Water)).gameObject.AddUIEvent(WaterButtonClicked);
        GetButton((int)(Buttons.WakeUp)).gameObject.AddUIEvent(WakeUpButtonClicked);
        GetButton((int)(Buttons.CheerUp)).gameObject.AddUIEvent(CheerUpButtonClicked);

        
    }
    private void Update()
    {
        Get<TMP_Text>((int)Texts.Score).text = Managers.Data.scoreData.currentScore.ToString();
    }
    private void WaterButtonClicked(PointerEventData eventData)
    {
        if(SlotManager._slot.tmpSlot ==null)
        {
            Managers.Sound.Play(Define.SFX.Button);
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType==0)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
            Managers.Sound.Play(Define.SFX.Watering);
        }
        else
        {
            Managers.Sound.Play(Define.SFX.Button);
        }

    }
    private void WakeUpButtonClicked(PointerEventData eventData)
    {
        if (SlotManager._slot.tmpSlot == null)
        {
            Managers.Sound.Play(Define.SFX.Button);
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType == 1)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
            Managers.Sound.Play(Define.SFX.Ringing);
        }
        else
        {
            Managers.Sound.Play(Define.SFX.Button);
        }

    }
    private void CheerUpButtonClicked(PointerEventData eventData)
    {
        if (SlotManager._slot.tmpSlot == null)
        {
            Managers.Sound.Play(Define.SFX.Button);
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType == 2)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
            Managers.Sound.Play(Define.SFX.Encourage);
        }
        else
        {
            Managers.Sound.Play(Define.SFX.Button);
        }


    }

}
