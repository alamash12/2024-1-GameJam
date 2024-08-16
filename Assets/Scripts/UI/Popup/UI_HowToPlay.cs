using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;
public class UI_HowToPlay : UI_Popup
{
    public Sprite[] howToPlaySprite;
    public int index = 0;
    public Image howToPlayPanelImage;
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
        Managers.UI.ClosePopUpUI();
        index = 0;
    }
    void BehindButtonClicekd(PointerEventData eventData)
    {
        if(index == 0)
            return;
        howToPlayPanelImage.sprite = howToPlaySprite[--index];
    }
    void NextButtonClicked(PointerEventData eventData)
    {
        Debug.Log(index);
        if (index == 3)
            return;
        howToPlayPanelImage.sprite = howToPlaySprite[++index];
    }
    void GameStartButtonClcked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
    }
}
