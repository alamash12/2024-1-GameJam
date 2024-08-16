using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Score : UI_Popup
{
    enum Buttons
    {
        ToMain,
        RePlay
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.ToMain).gameObject.AddUIEvent(ToMainClicekd);
        GetButton((int)Buttons.RePlay).gameObject.AddUIEvent(RePlayClicked);
    }
    void ToMainClicekd(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainTitle);
    }
    void RePlayClicked(PointerEventData eventData)
    {
        
    }
}
