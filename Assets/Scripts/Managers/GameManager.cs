using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public int gameStateIndex;
    //게임 상태를 나눠서 상태에 따라 스크립트들이 돌아가게 함
    public enum GameState
    {
        Swimming,
        Fail,
        Succeess
    }
    public GameState currentState;
    //플레이어 죽을 때 실행시킬 함수
    public void PlayerDied(int failing) // 잠수, 그냥 실패, 성공
    {                       //잠수조건 -> 전부 가득차면, 그냥 실패는 일정점수 이하로 시간초과, 성공은 일정 점수 이상으로 시간초과 
        Managers.UI.ShowPopUpUI<UI_Score>();
        Time.timeScale = 0;

        gameStateIndex = failing;
        
    }
    //인게임 데이터 초기화 
    public void GameStart()
    {
       
    }

}
