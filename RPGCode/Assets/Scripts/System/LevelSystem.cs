using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ؿ�������
/// </summary>
public class LevelSystem : Manager<LevelSystem>
{
    /// <summary>
    /// Init:
    /// ��ʼ��
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        return base.Init(obj);
    }

    /// <summary>
    /// Load:
    /// ����
    /// </summary>
    /// <param name="Chapter"></param>
    /// <param name="Level"></param>
    public void Load(int Chapter, int Level)
    {
        DebugEX.Log("��Ϸ��������ؿ�System", Chapter, Level);
        MapConfig.Instance.InitData();
        DeleteChild(MapConfig.Instance.npcsTrans);
        DeleteChild(MapConfig.Instance.modelsTrans);
        DeleteChild(MapConfig.Instance.particleTrans);
        TaskManager.Instance.InitData(Chapter, Level);
        MapConfig.Instance.LoadMapConfig(Chapter, Level, false);
    }

    /// <summary>
    /// Delete��
    /// ��Eidtģʽɾ��һ�������������е�������
    /// </summary>
    /// <param name="item"></param>
    private void DeleteChild(Transform item)
    {
        GameObject[] items = new GameObject[item.childCount];
        for (int i = 0; i < item.childCount; i++)
        {
            items[i] = item.GetChild(i).gameObject;
        }

        foreach (var obj in items)
        {
            GameObject.Destroy(obj);
        }
    }

    /// <summary>
    /// Get:
    /// ��ȡ���ñ�����
    /// </summary>
    /// <param name="Chapter"></param>
    /// <param name="Level"></param>
    /// <returns></returns>
    public MapConfigData GetConfig(int Chapter, int Level) 
    {
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("��ȡ�ؿ���������ʧ��");
            return null;
        }
        return data;
    }
}
