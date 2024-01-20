using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class NPCManager : Manager<NPCManager>
{
    //Tip: 当前场景的Npc
    public List<NpcBase> npcs = new List<NpcBase>();

    //Tip: 配置表中所有的npc数据
    public NpcCfg[] allnpcDatas;

    /// <summary>
    /// Init:
    /// 初始化Npc数据
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        allnpcDatas = ConfigManager.GetConfigList<NpcCfg>();
        DebugEX.Log("NpcManager初始化成功");
        return base.Init(obj);
    }

    /// <summary>
    /// Init
    /// 初始化Npc数据
    /// </summary>
    /// <param name="npc">Npc本体</param>
    /// <returns></returns>
    public NpcCfg InitNpcData(NpcBase npc) 
    {
        AddNpc(npc);
        foreach (var item in allnpcDatas)
        {
            if (item.ID == npc.ID)
                return item;
        }
        return null;
    }

    /// <summary>
    /// Add:
    /// 添加一个Npc
    /// </summary>
    /// <param name="obj">指向的Npc</param>
    public void AddNpc(NpcBase npc) 
    {
        npcs.Add(npc);
    }

    /// <summary>
    /// Get:
    /// 获得一个NPC
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public NpcBase GetNpc(int id)
    {
        foreach (var item in npcs)
        {
            if (item.ID == id)
                return item;
        }
        DebugEX.LogError("Npc Find失败", id);
        return null;
    }

    /// <summary>
    /// Remove:
    /// 移除一个Npc
    /// </summary>
    /// <param name="obj"></param>
    public void Remove(NpcBase npc)
    {
        npcs.Remove(npc);
    }

    /// <summary>
    /// Sign:
    /// 标记一个Npc
    /// </summary>
    public void SignNpc(NpcBase npc) 
    {
        GameManager.Instance.StopAllCoroutines();
        //npc.ModelDoColor(GameManager.Instance.lockColors[0]);
        for (int i = 0; i < npcs.Count; i++)
        {
            var item = npcs[i];
            if (item == npc)
                continue;
            //item.ModelDoFade(0.5f, 0f);
        }
    }



    /// <summary>
    /// Recycle:
    /// 回收数据
    /// </summary>
    public void Recycle()
    {
        npcs.Clear();
    }
}
