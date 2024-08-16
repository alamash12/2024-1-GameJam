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
        Option
    }
    private void Start()
    {
        Init();
        Bind<Button>(typeof(Buttons));

        GetButton((int)Buttons.GameStart).gameObject.AddUIEvent(GameStartButtonClicked);
        GetButton((int)Buttons.HowToPlay).gameObject.AddUIEvent(HowToPlayButtonClicked);
        GetButton((int)Buttons.Option).gameObject.AddUIEvent(OptionButtonClicked);
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
    private void OptionButtonClicked(PointerEventData eventData)
    {
      
    }

}
