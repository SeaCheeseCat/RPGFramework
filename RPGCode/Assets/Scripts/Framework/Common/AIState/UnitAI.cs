using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ɫ���ϵ�AI������
/// </summary>
public class UnitAI : MonoBehaviour
{
    //public UnitMono owner { get; private set; }  //���ж���
    Dictionary<AIState, AIStateBase> states = new Dictionary<AIState, AIStateBase>();    //״̬��
    AIStateBase state;  //��ǰ״̬
    public AIState AIState;

   /* public void SetUp(UnitMono owner,AIState state)
    {
        this.owner = owner;
        ChangeState(state);

        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null)
        {
            agent.Warp(transform.position);
        }
    }*/

/*    protected virtual void Update()
    {
        if (Battle.Instance.Pause) return;
        if (owner == null || !owner.Alive) return;
        state?.Update();
    }*/

    /// <summary>
    /// �ı�ai״̬
    /// </summary>
    public virtual AIStateBase ChangeState(AIState type)
    {
        if (state != null)
        {
            state.OnExit();
        }
        if (!states.ContainsKey(type))
        {
            AIStateBase state = null;
            switch (type)
            {
                case AIState.search:
                    state = new AIStateSearch();
                    break;
                case AIState.action:
                    state = new AIStateAction();
                    break;
                case AIState.chase:
                    state = new AIStateChase();
                    break;
                case AIState.idle:
                    state = new AIStateIdle();
                    break;
            }
            state.SetUp(this);
            states.Add(type, state);
        }
        state = states[type];
        state.OnEnter();
        AIState = type;
        //Debug.Log("ai״̬�л�==>>" + type);
        return state;
    }
}

public enum AIState
{
    idle = 0,   //ʲô������
    search = 1, //Ѱ�н׶�
    chase = 2,  //���н׶�
    action = 3,   //��Ϊ�׶�
}