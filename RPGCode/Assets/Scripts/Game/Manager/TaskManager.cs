using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Manager<TaskManager>
{
    //Tip: ��������
    TaskCfg[] AllTasks;
    //Tip���½�/�ؿ����񣨼��ٱ���������
    List<TaskCfg> ChapterTasks = new List<TaskCfg>(); 
    //Tip: �Ѿ���ɵ�����
    public List<int> TaskCompletes = new List<int>();
    //Tip: �Ѿ���ɵ���������
    public int TaskCompleteCount
    {
        get { return TaskCompletes.Count; }
    }
    //Tip: �ܹ�����������
    public int TaskTotal 
    {
        get { return ChapterTasks.Count; }
    }

    /// <summary>
    /// ��ȡ������ܹ�����
    /// </summary>
    /// <returns></returns>
    public int GetTaskCount()
    {
        return TaskTotal;
    }

    /// <summary>
    /// Get:
    /// ��ȡ���������
    /// </summary>
    /// <param name="chatper"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetTaskCount(int chatper,int level)
    {
        var count = 0;
        foreach (var item in AllTasks)
        {
            if (item.Chapter[0] == chatper && item.Chapter[1] == level)
                count++;
        }

        return count;
    }

    /// <summary>
    /// Get:
    /// ����������ɵ�����
    /// </summary>
    /// <returns></returns>
    public int GetTaskCompleteCount()
    {
        return TaskCompletes.Count;
    }
    

    /// <summary>
    /// Base: init
    /// ��ʼ��
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        Init();
        return base.Init(obj);
    }

    /// <summary>
    /// Init:
    /// ��ʼ��
    /// </summary>
    private void Init()
    {
        AllTasks = ConfigManager.GetConfigList<TaskCfg>();
    }

    /// <summary>
    /// Init:
    /// ��ʼ����������
    /// ������ʼ��
    /// </summary>
    /// <param name="chapter">�½�</param>
    /// <param name="level">�ؿ�</param>
    public void InitData(int chapter,int level) 
    {
        foreach (var item in AllTasks)
        {
            if (item.Chapter[0] == chapter && item.Chapter[1] == level)
                ChapterTasks.Add(item);
        }
    }

    /// <summary>
    /// Get:
    /// ��ȡ��ǰ�½ڵ���������
    /// </summary>
    /// <returns></returns>
    public List<TaskCfg> GetCurrentLevelTask() 
    {
        return ChapterTasks;
    }
    
    /// <summary>
    /// Get:
    /// ��ȡһ������
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TaskCfg GetTask(int id)
    {
        return ConfigManager.GetConfigByID<TaskCfg>(id);
    }
    
    /// <summary>
    /// Check:
    /// ���һ�������Ƿ����
    /// </summary>
    /// <param name="id">����NPC ID</param>
    /// <param name="anglex">��ת�ĽǶ�x</param>
    /// <param name="angley">��ת�ĽǶ�y</param>
    /// <param name="arg">������жϽǶ�</param>
    /// <returns></returns>
    public TaskCfg CheckTask(int id,float anglex, float angley, float arg) 
    {
        foreach (var item in ChapterTasks)
        {
            if (int.Parse(item.Args[1]) == id &&(item.Angles[0] >= anglex-arg && item.Angles[0] <= anglex + arg) && (item.Angles[1] >= angley-arg && item.Angles[1] <= angley + arg))
            {
                return item;
            }
        }
        return null;
    }


    /// <summary>
    /// Check:
    /// ���ؿ��Ƿ����
    /// </summary>
    /// <returns></returns>
    public bool CheckLevelComplete() 
    {
        return TaskCompleteCount >= TaskTotal;
    }

    /// <summary>
    /// Callback:
    /// ������һ���¼���ɴ���
    /// </summary>
    public void OnCompleteTask(int id) 
    {
        if (TaskCompletes.Contains(id))
            return;
        TaskCompletes.Add(id);
        MsgManager.Instance.SendMessage(MSGInfo.REFRESHINFO, GetTaskCompleteCount(), GetTaskCount());
    }

    /// <summary>
    /// Recycle:
    /// ��������
    /// </summary>
    public void Recycle() 
    {
        ChapterTasks.Clear();
    }

}
