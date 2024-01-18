using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ai״̬������
/// </summary>
public class AIStateBase
{
    protected UnitAI AI;

    public virtual void SetUp(UnitAI ai)
    {
        AI = ai;
    }

    /// <summary>
    /// ����
    /// </summary>
    public virtual void OnEnter()
    {

    }

    /// <summary>
    /// ��ʱ����
    /// </summary>
    public virtual void Update()
    {

    }

    /// <summary>
    /// �˳�
    /// </summary>
    public virtual void OnExit()
    {

    }
}
