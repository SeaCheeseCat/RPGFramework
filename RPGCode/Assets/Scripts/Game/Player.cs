using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayerBase
{
    /// <summary>
    /// Callback:
    /// ���������ɽ����������ϵ�ʱ�򴥷�
    /// </summary>
    /// <param name="gameObject"></param>
    public override void OnIneract(GameObject ineractObj)
    {
        base.OnIneract(ineractObj);
        DebugEX.Log("Ѱ�ҵ��ɽ�����������", ineractObj.name);
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
