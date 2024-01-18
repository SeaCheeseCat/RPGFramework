using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ���н׶�
/// </summary>
public class AIStateChase : AIStateBase
{
    //SkillMono skill;    //׼���ͷŵļ���
    Vector3 direct;
    NavMeshPath navMeshPath;
    List<Vector3> path; //·��
    float findPathCache;    //����Ѱ·�ӳ�
    NavMeshAgent agent;

    public override void SetUp(UnitAI ai)
    {
        base.SetUp(ai);
        agent = ai.GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();
        path = new List<Vector3>();
    }

    public void SetSkill(/*SkillMono skill*/)
    {
       // this.skill = skill;
    }

    public override void OnExit()
    {
        base.OnEnter();
        //skill = null;
    }

    public override void Update()
    {
        base.Update();
        //if (Battle.Instance.Pause) return;
        if (findPathCache > 0f)
            findPathCache -= Time.deltaTime;

        //skill.CheckTarget();
        //if (skill.Target == null)
        //{
        //    skill.FindTarget();
        //}
        /*if (skill.Target != null)
        {
            direct = skill.Target.transform.position - AI.transform.position;
            if (direct.magnitude<= AI.owner.GetProperty(RpcData.PropertyType.Range) / 100f||skill.Config.InitCast==1)
            {
                //Debug.Log("���빻��");
                //ʩ�������ڣ�ֱ��ʩ��
                var skill = this.skill;
                var state = AI.ChangeState(AIState.action) as AIStateAction;
                state.SetSkill(skill);
            }
            else
            {
                //�ж��Ƿ��ܹ��ƶ�
                if (AI.owner.UnitCfg.Speed <= 0f)
                {
                    skill.ClearTarget();
                    AI.ChangeState(AIState.search); //����Ѱ��
                }
                else
                {
                    //�����ƶ�
                    //���Ƚ���Ѱ·
                    if (findPathCache <= 0||path.Count<=0)
                    {
                        findPathCache = 0.5f;
                        path.Clear();
                        navMeshPath.ClearCorners();
                        if (agent.CalculatePath(skill.Target.transform.position, navMeshPath))
                        {
                            //д��Ѱ·���
                            //Debug.Log("·������:" + navMeshPath.corners.Length);
                            if(navMeshPath.status == NavMeshPathStatus.PathComplete)
                            {
                                foreach (var v in navMeshPath.corners)
                                {
                                    path.Add(v);
                                }
                            }
                        }
                    }

                    //����Ѱ··��
                    if (path.Count > 0)
                    {
                        direct = path[0] - AI.transform.position;
                        if (direct.magnitude <= 0.1f)
                        {
                            path.RemoveAt(0);
                        }
                        else
                        {
                            AI.owner.Move(direct);
                        }   
                    }
                }
            }
        }
        else
        {
            AI.ChangeState(AIState.search); //����Ѱ��
        }*/
    }
}
