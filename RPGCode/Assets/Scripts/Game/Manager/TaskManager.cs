using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Manager<TaskManager>
{
    //Tip: 所有任务
    TaskCfg[] AllTasks;
    //Tip：章节/关卡任务（减少遍历次数）
    List<TaskCfg> ChapterTasks = new List<TaskCfg>(); 
    //Tip: 已经完成的任务
    public List<int> TaskCompletes = new List<int>();
    //Tip: 已经完成的任务数量
    public int TaskCompleteCount
    {
        get { return TaskCompletes.Count; }
    }
    //Tip: 总共的任务数量
    public int TaskTotal 
    {
        get { return ChapterTasks.Count; }
    }

    /// <summary>
    /// 获取任务的总共数量
    /// </summary>
    /// <returns></returns>
    public int GetTaskCount()
    {
        return TaskTotal;
    }

    /// <summary>
    /// Get:
    /// 获取任务的数量
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
    /// 返回任务完成的数量
    /// </summary>
    /// <returns></returns>
    public int GetTaskCompleteCount()
    {
        return TaskCompletes.Count;
    }
    

    /// <summary>
    /// Base: init
    /// 初始化
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
    /// 初始化
    /// </summary>
    private void Init()
    {
        AllTasks = ConfigManager.GetConfigList<TaskCfg>();
    }

    /// <summary>
    /// Init:
    /// 初始化任务数据
    /// 单独初始化
    /// </summary>
    /// <param name="chapter">章节</param>
    /// <param name="level">关卡</param>
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
    /// 获取当前章节的所有任务
    /// </summary>
    /// <returns></returns>
    public List<TaskCfg> GetCurrentLevelTask() 
    {
        return ChapterTasks;
    }
    
    /// <summary>
    /// Get:
    /// 获取一个任务
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TaskCfg GetTask(int id)
    {
        return ConfigManager.GetConfigByID<TaskCfg>(id);
    }
    
    /// <summary>
    /// Check:
    /// 检测一个任务是否完成
    /// </summary>
    /// <param name="id">焦点NPC ID</param>
    /// <param name="anglex">旋转的角度x</param>
    /// <param name="angley">旋转的角度y</param>
    /// <param name="arg">增大的判断角度</param>
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
    /// 检查关卡是否完成
    /// </summary>
    /// <returns></returns>
    public bool CheckLevelComplete() 
    {
        return TaskCompleteCount >= TaskTotal;
    }

    /// <summary>
    /// Callback:
    /// 当其中一个事件完成触发
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
    /// 回收数据
    /// </summary>
    public void Recycle() 
    {
        ChapterTasks.Clear();
    }

}
