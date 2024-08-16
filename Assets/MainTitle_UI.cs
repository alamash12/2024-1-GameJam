using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class MainTitle_UI : UI_Popup
{
    enum Buttons
    {
        GameStart,
        HowToPlay,
        Option
    }
    enum Texts
    {
        HighScore,
    }
    private void Start()
    {
        Init();
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(GameStartButtonClicked);
        GetButton((int)Buttons.HowToPlay).gameObject.AddUIEvent(HowToPlayButtonClicked);
        GetButton((int)Buttons.Option).gameObject.AddUIEvent(OptionButtonClicked);

        Get<TMP_Text>((int)Texts.HighScore).text = Managers.Data.scoreData.HighScore.ToString();
    }
    public override void Init()
    {
        base.Init();
    }
    private void GameStartButtonClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
        Managers.Sound.Play(Define.SFX.Button);

    }
    private void HowToPlayButtonClicked(PointerEventData eventData)
    {
        
        Managers.UI.ShowPopUpUI<UI_HowToPlay>();
        Managers.Sound.Play(Define.SFX.Button);

    }
    private void OptionButtonClicked(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_Option>();
        Managers.Sound.Play(Define.SFX.Button);

    }

}
