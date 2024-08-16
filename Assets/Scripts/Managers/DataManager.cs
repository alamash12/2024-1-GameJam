using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict(); //MakeDict 함수 구현 강제 , Data 객체들은 Dictionary에 모음.
}



public class DataManager
 {
        public Define.WholeGameData gameData;
        public Define.VolumeData volumeData = new Define.VolumeData();
    public ScoreData scoreData = new ScoreData();

    public void Init()
    {
       
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // text 파일이 textAsset에 담긴다.
                                                                                 // TextAsset 타입은 텍스트파일 에셋이라고 생각하면 됨!
        return JsonUtility.FromJson<Loader>(textAsset.text); //JSON 데이터를 불러와서 리턴
    }

}

[System.Serializable]
public class ScoreData
{
    public int currentScore;
    public int touchStudent;
    public int HighScore;
    public float GamePercent;

    public ScoreData()
    {
        currentScore = 0;
        touchStudent = 0;
        HighScore = 0;
    }

    public void SetHighScore()
    {
        if(currentScore > HighScore)
            HighScore = currentScore;
    }
}
