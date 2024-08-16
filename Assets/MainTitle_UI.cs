using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainTitle_UI : UI_Popup
{
    enum Buttons
    {
        GameStart,
        HowToPlay,
        MaxScore
    }
    private void Start()
    {
        Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(GameStartButtonClicked);
        GetButton((int)Buttons.HowToPlay).gameObject.AddUIEvent(HowToPlayButtonClicked);
        GetButton((int)Buttons.MaxScore).gameObject.AddUIEvent(MaxScoreButtonClicked);
    }
    public override void Init()
    {
        base.Init();
    }
    private void GameStartButtonClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
    }
    private void HowToPlayButtonClicked(PointerEventData eventData)
    {
        Managers.UI.ShowPopUpUI<UI_HowToPlay>();
    }
    private void MaxScoreButtonClicked(PointerEventData eventData)
    {

    }
}
