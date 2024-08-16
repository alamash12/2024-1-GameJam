using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : BaseScene
{
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
