using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitGameSetUI : SetItemUI
{
    public override void OnEnter()
    {
        base.OnEnter();
        Application.Quit();
    }
}

