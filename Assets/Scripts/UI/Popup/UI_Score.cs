using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Score : UI_Popup
{
    public Image image;
    public Image DiveImage;
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
        Bind<TMP_Text>(typeof(Texts));

        GetButton((int)Buttons.ToMain).gameObject.AddUIEvent(ToMainClicekd);
        GetButton((int)Buttons.RePlay).gameObject.AddUIEvent(RePlayClicked);


        Get<TMP_Text>((int)Texts.GetStudentScore).text = Managers.Data.scoreData.touchStudent.ToString();
        Get<TMP_Text>((int)Texts.Score).text = Managers.Data.scoreData.currentScore.ToString();
        Get<TMP_Text>((int)Texts.CompleteGamePercent).text = Managers.Data.scoreData.GamePercent.ToString();

        if(Managers.Game.gameStateIndex==0)//잠수
        {
            Managers.UI.ClosePopUpUI();
            Managers.UI.ShowPopUpUI<UI_FailPopUp>();
        }
        else if (Managers.Game.gameStateIndex == 1)// 실패    
        {
            image.sprite = Managers.Resource.Load<Sprite>("Sprites/Result_fail");
        }
        else // 성공
        {
            image.sprite = Managers.Resource.Load<Sprite>("Sprites/Result_success");
        }
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
