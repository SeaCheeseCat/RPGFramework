using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ѱ�н׶�
/// </summary>
public class AIStateSearch : AIStateBase
{
    float cache;    //Ѱ�м��

    public override void OnEnter()
    {
        base.OnEnter();
        cache = 0.1f;
    }

    public override void Update()
    {
        base.Update();
        cache -= Time.deltaTime;
        if (cache <= 0f)
        {
            cache += 0.1f;

            //������ѯ���ü��ܲ�����Ѱ��
            /*foreach(var v in AI.owner.Skills)
            {
                if (v.CanCast())
                {
                    //Debug.Log(AI.owner.ID+ "���ڿ��ͷż���" + v.ID);
                    var state = AI.ChangeState(AIState.chase) as AIStateChase;
                    state.SetSkill(v);
                    break;
                }
            }*/

            //Debug.Log("δ�ҵ����ͷż���" + AI.owner.Skills.Count);
        }
    }
}
