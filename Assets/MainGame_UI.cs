using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MainGame_UI : UI_Popup
{
    enum Buttons
    {
        Water,
        WakeUp,
        CheerUp,
        Option
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)(Buttons.Water)).gameObject.AddUIEvent(WaterButtonClicked);
        GetButton((int)(Buttons.WakeUp)).gameObject.AddUIEvent(WakeUpButtonClicked);
        GetButton((int)(Buttons.CheerUp)).gameObject.AddUIEvent(CheerUpButtonClicked);
        GetButton((int)(Buttons.Option)).gameObject.AddUIEvent(OptionClicked);
    }
    private void WaterButtonClicked(PointerEventData eventData)
    {
        if(SlotManager._slot.tmpSlot ==null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType==0)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
        }
       
    }
    private void WakeUpButtonClicked(PointerEventData eventData)
    {
        if (SlotManager._slot.tmpSlot == null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType == 1)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
        }
    }
    private void CheerUpButtonClicked(PointerEventData eventData)
    {
        if (SlotManager._slot.tmpSlot == null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(SlotManager._slot.tmpSlot.ActingType == 2)
        {
            SlotManager._slot.tmpSlot.offHighLight();
            SlotManager._slot.tmpSlot = null;
        }
    }

    private void OptionClicked(PointerEventData eventData)
    {

    }
}
