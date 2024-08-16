using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
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

    private void Awake()
    {
        Managers.UI.ShowPopUpUI<UI_Test>();
        Debug.Log("Start");
        Init();
    }
}
