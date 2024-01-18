using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ʼ�����¼�
/// ��һ�������
/// </summary>
public class InitialStoryEvent :  BaseGameEvent
{
    /// <summary>
    /// Trigger:
    /// �����¼�
    /// </summary>
    /// <param name="data">�¼�����</param>
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        UIManager.Instance.OpenUI<StoryUI>(1);   
    }

}
