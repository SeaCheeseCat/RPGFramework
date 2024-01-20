using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class NPCManager : Manager<NPCManager>
{
    //Tip: ��ǰ������Npc
    public List<NpcBase> npcs = new List<NpcBase>();

    //Tip: ���ñ������е�npc����
    public NpcCfg[] allnpcDatas;

    /// <summary>
    /// Init:
    /// ��ʼ��Npc����
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        allnpcDatas = ConfigManager.GetConfigList<NpcCfg>();
        DebugEX.Log("NpcManager��ʼ���ɹ�");
        return base.Init(obj);
    }

    /// <summary>
    /// Init
    /// ��ʼ��Npc����
    /// </summary>
    /// <param name="npc">Npc����</param>
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
    /// ���һ��Npc
    /// </summary>
    /// <param name="obj">ָ���Npc</param>
    public void AddNpc(NpcBase npc) 
    {
        npcs.Add(npc);
    }

    /// <summary>
    /// Get:
    /// ���һ��NPC
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
        DebugEX.LogError("Npc Findʧ��", id);
        return null;
    }

    /// <summary>
    /// Remove:
    /// �Ƴ�һ��Npc
    /// </summary>
    /// <param name="obj"></param>
    public void Remove(NpcBase npc)
    {
        npcs.Remove(npc);
    }

    /// <summary>
    /// Sign:
    /// ���һ��Npc
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
    /// ��������
    /// </summary>
    public void Recycle()
    {
        npcs.Clear();
    }
}
