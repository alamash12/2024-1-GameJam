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
        base.Init();
        SetResolution();
        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        GetButton((int)Buttons.ScoreIncreaseButton).gameObject.AddUIEvent(ScoreIncrease);
        GetButton((int)Buttons.HighScoreClearButton).gameObject.AddUIEvent(Clear);
        GetButton((int)Buttons.HighScoreSetButton).gameObject.AddUIEvent(SetHighScore);


        StartCoroutine(dataloading());
    }
    IEnumerator dataloading()
    {
        yield return new WaitForSeconds(0.1f);
        GetText((int)Texts.CurrentScoreText).text = Managers.Data.scoreData.currentScore.ToString();
        GetText((int)Texts.HighScoreText1).text = Managers.Data.scoreData.HighScore.ToString();
        yield return null;
    }

    void ScoreIncrease(PointerEventData eventData)
    {
        Managers.Data.scoreData.currentScore += 100;
    }

    void Clear(PointerEventData eventData)
    {
        Managers.Data.scoreData.HighScore = 0;
    }

    void SetHighScore(PointerEventData eventData)
    {
        Managers.Data.scoreData.SetHighScore();
    }
}
