using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBase
{
    /// <summary>
    /// Callback:
    /// 当触发到可交互的物体上的时候触发
    /// </summary>
    /// <param name="gameObject"></param>
    public override void OnIneract(GameObject ineractObj)
    {
        base.OnIneract(ineractObj);
        DebugEX.Log("寻找到可交互的物体了", ineractObj.name);
        UIManager.Instance.OpenUI<KeyTipUI>();
    }

    public override void OnExiatIneract()
    {
        base.OnExiatIneract();
        if (UIManager.Instance.GetUI<KeyTipUI>() != null)
        {
            UIManager.Instance.CloseUI<KeyTipUI>();
        }
    }

    public override void Update()
    {
        base.Update();
        OnInputKey();
    }


    public void OnInputKey()
    { 
        
    }

}
