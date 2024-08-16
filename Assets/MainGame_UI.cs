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
        if(Managers.Slot.tmpSlot ==null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(Managers.Slot.tmpSlot.ActingType==0)
        {
            Managers.Slot.tmpSlot.offHighLight();
            Managers.Slot.tmpSlot = null;
        }
       
    }
    private void WakeUpButtonClicked(PointerEventData eventData)
    {
        if (Managers.Slot.tmpSlot == null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(Managers.Slot.tmpSlot.ActingType == 1)
        {
            Managers.Slot.tmpSlot.offHighLight();
            Managers.Slot.tmpSlot = null;
        }
    }
    private void CheerUpButtonClicked(PointerEventData eventData)
    {
        if (Managers.Slot.tmpSlot == null)
        {
            Debug.Log("tmpSlot Null");
        }
        else if(Managers.Slot.tmpSlot.ActingType == 2)
        {
            Managers.Slot.tmpSlot.offHighLight();
            Managers.Slot.tmpSlot = null;
        }
    }

    private void OptionClicked(PointerEventData eventData)
    {

    }
}
