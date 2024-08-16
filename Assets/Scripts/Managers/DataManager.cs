using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict(); //MakeDict �Լ� ���� ���� , Data ��ü���� Dictionary�� ����.
}



public class DataManager
 {
        public Define.WholeGameData gameData;
        public Define.VolumeData volumeData = new Define.VolumeData();
    public ScoreData scoreData;

    public void Init()
    {
        scoreData = new ScoreData();
    }

    public void ScoreIncrease()
    {
        scoreData.currentScore += 100;
        scoreData.comboCount++;
        scoreData.touchStudent++;
        if(scoreData.comboCount % 5 == 0 && scoreData.comboCount != 0)
        {
            scoreData.currentScore += 300;
        }
        scoreData.GamePercent = Mathf.Clamp(scoreData.currentScore / 100f, 0f, 100f);
    }   

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // text ������ textAsset�� ����.
                                                                                 // TextAsset Ÿ���� �ؽ�Ʈ���� �����̶�� �����ϸ� ��!
        return JsonUtility.FromJson<Loader>(textAsset.text); //JSON �����͸� �ҷ��ͼ� ����
    }

}

[System.Serializable]
public class ScoreData
{
    public int currentScore;
    public int touchStudent;
    public int HighScore;
    public float GamePercent;
    public int comboCount;

    public ScoreData()
    {
        currentScore = 0;
        touchStudent = 0;
        HighScore = PlayerPrefs.GetInt("HighScore");
        GamePercent = 0f;
        comboCount = 0;
    }

    public void SetHighScore()
    {
        if(currentScore > HighScore)
        {
            HighScore = currentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
}
