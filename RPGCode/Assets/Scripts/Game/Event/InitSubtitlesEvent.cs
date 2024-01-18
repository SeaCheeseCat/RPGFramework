using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ʼ�����¼�
/// ��һ�������
/// </summary>
public class InitSubtitlesEvent :  BaseGameEvent
{
    /// <summary>
    /// Trigger:
    /// �����¼�
    /// </summary>
    /// <param name="data">�¼�����</param>
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        GameBase.Instance.StartCoroutine(DoInitSubtitlesEvent());
    }

    IEnumerator DoInitSubtitlesEvent() 
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.OpenUI<MonologueUI>(2);
    }

}
