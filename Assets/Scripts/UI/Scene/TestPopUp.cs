using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestPopUp : UI_Popup
{
    enum Buttons
    {
        ScoreIncreaseButton,
        HighScoreClearButton,
        HighScoreSetButton,
    }

    enum Texts
    {
        CurrentScoreText,
        HighScoreText1,
    }
    void Start()
    {
        Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        GetButton((int)Buttons.ScoreIncreaseButton).gameObject.AddUIEvent(ScoreIncrease);
        GetButton((int)Buttons.HighScoreClearButton).gameObject.AddUIEvent(Clear);
        GetButton((int)Buttons.HighScoreSetButton).gameObject.AddUIEvent(SetHighScore);

        Get<TMP_Text>((int)Texts.CurrentScoreText).text = Managers.Data.scoreData.currentScore.ToString();
        Get<TMP_Text>((int)Texts.HighScoreText1).text = Managers.Data.scoreData.HighScore.ToString();
    }
    public override void Init()
    {
        base.Init();
        Managers.Data.scoreData.HighScore = PlayerPrefs.GetInt("HighScore");
    }

    void ScoreIncrease(PointerEventData eventData)
    {
        Managers.Data.scoreData.currentScore += 100;
        Get<TMP_Text>((int)Texts.CurrentScoreText).text = Managers.Data.scoreData.currentScore.ToString();
    }

    void Clear(PointerEventData eventData)
    {
        Managers.Data.scoreData.HighScore = 0;
        Get<TMP_Text>((int)Texts.HighScoreText1).text = Managers.Data.scoreData.HighScore.ToString();
        PlayerPrefs.SetInt("HighScore", Managers.Data.scoreData.HighScore);
    }

    void SetHighScore(PointerEventData eventData)
    {
        Managers.Data.scoreData.SetHighScore();
        Get<TMP_Text>((int)Texts.HighScoreText1).text = Managers.Data.scoreData.HighScore.ToString();
        PlayerPrefs.SetInt("HighScore", Managers.Data.scoreData.HighScore);
    }
}
