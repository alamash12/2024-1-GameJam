using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Score : UI_Popup
{
    enum Buttons
    {
        ToMain,
        RePlay
    }
    enum Texts
    {
        GetStudentScore,
        Score,
        CompleteGamePercent,
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

        Debug.Log(Managers.Data.scoreData.touchStudent);
        Get<TMP_Text>((int)Texts.GetStudentScore).text = Managers.Data.scoreData.touchStudent.ToString();
        Get<TMP_Text>((int)Texts.Score).text = Managers.Data.scoreData.currentScore.ToString();
        Get<TMP_Text>((int)Texts.CompleteGamePercent).text = Managers.Data.scoreData.GamePercent.ToString();

    }
    void ToMainClicekd(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainTitle);
    }
    void RePlayClicked(PointerEventData eventData)
    {
        Managers.Scene.LoadScene(Define.Scene.MainGame);
    }
}
