using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Manager<EventManager>
{
    // Tip: �Ѿ���ɵ��¼�
    public List<int> completedEvents = new List<int>();

    /// <summary>
    /// Trigger:
    /// ������ͨ�¼�
    /// </summary>
    /// <param name="id">�¼�ID</param>
    public void TriggerEvent(string eventName)
    {
        DebugEX.Log("ִ���¼�", eventName);
        var curEvent = InstanceGameEvent(eventName);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = null;
            eventData.id = 0;
            curEvent.TriggerEvent(eventData);
        }
    }

    /// <summary>
    /// Trigger:
    /// ������ͨ�¼�
    /// </summary>
    /// <param name="id">�¼�ID</param>
    public void TriggerEvent<T>()
    {
        var curEvent = InstanceGameEvent(typeof(T).Name);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = null;
            eventData.id = 0;
            curEvent.TriggerEvent(eventData);
        }
    }


    /// <summary>
    /// Trigger:
    /// ������ͨ�¼�
    /// </summary>
    /// <param name="id">�¼�ID</param>
    /// <param name="eventName">�¼�����</param>
    /// <param name="args">�¼�����</param>
    public void TriggerEvent(int id, string eventName, string[] args)
    {
        if (GetCompleteEvent(id))
        {
            DebugEX.LogError("�¼�ִ��ʧЧ  �¼��Ѿ���������һ��", id);
            return;
        }
        var curEvent = InstanceGameEvent(eventName);
        if (curEvent != null)
        {
            var eventData = new EventData();
            eventData.args = args;
            eventData.id = id;
            curEvent.TriggerEvent(eventData);
        }
        completedEvents.Add(id);
    }

    /// <summary>
    /// Get:
    /// �¼��Ƿ��Ѿ����
    /// </summary>
    /// <param name="id">�¼�id</param>
    /// <returns>���״̬</returns>
    public bool GetCompleteEvent(int id)
    {
        foreach (var item in completedEvents)
        {
            if (item == id)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Instance:
    /// ͨ������ʵ����һ���¼���
    /// </summary>
    /// <param name="eventName">�¼�����</param>
    /// <returns>�¼���</returns>
    private BaseGameEvent InstanceGameEvent(string eventName)
    {
        var typeName = eventName;
        Type type = Type.GetType(typeName);
        if (type != null)
        {
            var ev = Activator.CreateInstance(type) as BaseGameEvent;
            return ev;
        }
        return null;
    }

    public void Recycle()
    { 
        completedEvents.Clear();
    }

}
