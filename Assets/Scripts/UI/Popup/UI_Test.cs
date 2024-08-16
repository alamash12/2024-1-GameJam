using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Test : UI_Popup
{
    // Start is called before the first frame update
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
