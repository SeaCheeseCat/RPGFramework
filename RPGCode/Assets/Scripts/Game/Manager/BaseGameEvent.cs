using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEvent 
{
    /// <summary>
    /// ����һ���¼�
    /// </summary>
    /// <param name="data">�¼�����</param>
    public virtual void TriggerEvent(EventData data)
    {  
    }

    /// <summary>
    /// ͨ������ֱ��ʹ���¼�
    /// </summary>
    /// <param name="args">�����ľ�����ֵ</param>
    public virtual void DirectTriggerEvent(int[] args) 
    {
    }
}

//Tip: �������¼����ݽṹ��
public class EventData 
{
    public int id;
    public string[] args;
}
