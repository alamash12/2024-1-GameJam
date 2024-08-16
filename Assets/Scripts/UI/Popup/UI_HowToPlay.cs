using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
public class UI_HowToPlay : UI_Popup
{
    enum Buttons
    {
        Close,
        Behind,
        Next,
        GameStart
    }
    private void Start()
    {
        Init();

        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.Close).gameObject.AddUIEvent(CloseButtonClicked);
        GetButton((int)Buttons.Behind).gameObject.AddUIEvent(BehindButtonClicekd);
        GetButton((int)Buttons.Next).gameObject.AddUIEvent(NextButtonClicked);
        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(GameStartButtonClcked);
    }
    public override void Init()
    {
        base.Init();
    }
    void CloseButtonClicked(PointerEventData eventData)
    {
        Debug.Log("f��������������������������");
        Managers.UI.ClosePopUpUI();
    }
    void BehindButtonClicekd(PointerEventData eventData)
    {
        Debug.Log("f��������������������������");
    }
    void NextButtonClicked(PointerEventData eventData)
    {
        Debug.Log("f��������������������������");
    }
    void GameStartButtonClcked(PointerEventData eventData)
    {
        Debug.Log("f��������������������������");
        Managers.Scene.LoadScene(Define.Scene.MainGame);
    }
}
