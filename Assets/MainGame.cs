using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MainGame : BaseScene
{
    private CanvasGroup canvasGroup;

    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    protected override void Init()
    {
        base.Init();
    }

    private void Start()
    {
        Managers.UI.ShowPopUpUI<UI_Test>();
        Debug.Log("Start");
        Init();
    }

 
}
