using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_FailPopUp : UI_Popup
{
    enum Buttons
    {
        ToMain,
        RePlay
    }

    enum Texts
    {
        Score,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));
        GetButton((int)Buttons.ToMain).gameObject.AddUIEvent(ToMainClicekd);
        GetButton((int)Buttons.RePlay).gameObject.AddUIEvent(RePlayClicked);

        Managers.Data.scoreData.SetHighScore();
        Get<TMP_Text>((int)Texts.Score).text = $"{(Managers.Data.scoreData.currentScore / 100)}%";
    }
    void ToMainClicekd(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainTitle);
        Managers.Data.ScoreInit();
        Managers.Sound.Play(Define.SFX.Button);
    }
    void RePlayClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
        Managers.Data.ScoreInit();
        Managers.Sound.Play(Define.SFX.Button);
    }
}
