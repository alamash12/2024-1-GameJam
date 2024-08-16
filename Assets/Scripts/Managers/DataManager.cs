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
    public Dictionary<int, Stat> StatDict { get; private set; } = new Dictionary<int, Stat>(); // ���Ȱ��� �����͵��� ���� Dictionary (key, value= ���� ��ü)

  /*  public Dictionary<int, int> currentLevel = new Dictionary<int, int>();
        public Dictionary<int, float> currentStat = new Dictionary<int, float>();*/
        public Define.WholeGameData gameData;
        public Define.Items Items;
        public Define.VolumeData volumeData = new Define.VolumeData();
        public Define.Blesses Blesses;

    public void Init()
    {
        StatDict = LoadJson<StatData, int, Stat>("StatData").MakeDict(); // LoadJson�Լ��� StatData.json �����Ͱ� �����
                                                                         // Stat ��ü���� ��� Dictionary�� ����� ������Ƽ�� ����
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}"); // text ������ textAsset�� ����.
                                                                                 // TextAsset Ÿ���� �ؽ�Ʈ���� �����̶�� �����ϸ� ��!
        return JsonUtility.FromJson<Loader>(textAsset.text); //JSON �����͸� �ҷ��ͼ� ����
    }

}


#region Stat

[Serializable]
public class Stat // // MonoBehavior �� ������� �ʾұ� ������ ����ȭ�ؼ� insperctorâ���� ����
{
    public int LEVEL; // ID
    public int ATK;
    public int DEF;
    public int GOLD;
    public int CRIT1;
    public int CRIT2;
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();  // json ���Ͽ��� ����� ���

    public Dictionary<int, Stat> MakeDict() // �������̵�
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats) // ����Ʈ���� Dictionary�� �ű�� �۾�
            dict.Add(stat.LEVEL, stat); // level�� ID(Key)�� 
        return dict;
    }
}

#endregion