using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : UI_Popup
{
    
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        SetResolution();
    }

}
