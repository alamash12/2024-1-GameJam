using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{

    //게임 상태를 나눠서 상태에 따라 스크립트들이 돌아가게 함
    public enum GameState
    {
    }
    public GameState currentState;
    //플레이어 죽을 때 실행시킬 함수
    public void PlayerDied()
    {
        Managers.UI.ShowPopUpUI<UI_Score>();
        Time.timeScale = 0;
    }
    //인게임 데이터 초기화 
    public void GameStart()
    {
       
    }

}
