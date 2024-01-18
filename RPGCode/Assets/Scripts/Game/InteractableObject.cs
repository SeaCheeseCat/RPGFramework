using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ɽ���������
/// ��Ϸ���ָ���ƶ�����ĵط���
/// </summary>
public class InteractableObject : Item
{
    //Tip:���������Ŀ��
    public NpcBase followTarget;
    //Tip�����״̬
    [HideInInspector]
    public bool complete;
   

    public virtual void Awake()
    {
        OnCreate();
    }

    /// <summary>
    /// Callback:
    /// ���崴���ص�
    /// </summary>
    public virtual void OnCreate() 
    { 
    }

    /// <summary>
    /// Set:
    /// ���ø�������
    /// </summary>
    /// <param name="tr"></param>
    public virtual void SetFollow(NpcBase npc)
    {
        followTarget = npc;
    }

    /// <summary>
    /// Callback:
    /// �϶�ʱִ��
    /// </summary>
    public virtual void OnDray()
    {

    }

    /// <summary>
    /// Callback:
    /// �������
    /// </summary>
    /// <param name="id"></param>
    public virtual void OnTaskComplete(int id) 
    {
        complete = true;
    }
}
