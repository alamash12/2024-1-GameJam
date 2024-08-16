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

        GetButton((int)Buttons.Behind).gameObject.SetActive(false);
        GetButton((int)Buttons.GameStart).gameObject.SetActive(false);
    }
    public override void Init()
    {
        base.Init();
    }
    void CloseButtonClicked(PointerEventData eventData)
    {
        Managers.UI.ClosePopUpUI();
        index = 0;
        GetButton((int)Buttons.Behind).gameObject.SetActive(false);
        Managers.Sound.Play(Define.SFX.Button);
    }
    void BehindButtonClicekd(PointerEventData eventData)
    {
        GetButton((int)Buttons.Next).gameObject.SetActive(true);
        GetButton((int)Buttons.GameStart).gameObject.SetActive(false);
        if (index == 1)
        {
            GetButton((int)Buttons.Behind).gameObject.SetActive(false);
        }
        howToPlayPanelImage.sprite = howToPlaySprite[--index];
        Managers.Sound.Play(Define.SFX.Button);
    }
    void NextButtonClicked(PointerEventData eventData)
    {
        GetButton((int)Buttons.Behind).gameObject.SetActive(true);
        if(index == 2)
        {
            GetButton((int)Buttons.Next).gameObject.SetActive(false);
            GetButton((int)Buttons.GameStart).gameObject.SetActive(true);
        }
        howToPlayPanelImage.sprite = howToPlaySprite[++index];
        Managers.Sound.Play(Define.SFX.Button);
    }
    void GameStartButtonClcked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
        Managers.Sound.Play(Define.SFX.Button);
    }
}
