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
    }
}
