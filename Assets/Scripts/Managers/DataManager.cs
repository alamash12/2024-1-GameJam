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
 

    public void Init()
    {
       
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // text ������ textAsset�� ����.
                                                                                 // TextAsset Ÿ���� �ؽ�Ʈ���� �����̶�� �����ϸ� ��!
        return JsonUtility.FromJson<Loader>(textAsset.text); //JSON �����͸� �ҷ��ͼ� ����
    }

}
