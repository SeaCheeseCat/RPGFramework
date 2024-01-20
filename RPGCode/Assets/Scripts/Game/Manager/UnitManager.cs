using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ�����
/// </summary>
public class UnitManager : Manager<UnitManager>
{
    /// <summary>
    /// Create:
    /// ����һ��NPC
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    public T CreateNpc<T>(int id) where T : NpcBase
    {
        var npc = MapConfig.Instance.CreateNpc(id, default, default, default, false);
        npc.OnCreate();
        return npc as T;
    }

    /// <summary>
    /// Create:
    /// ����һ��NPC
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">ID</param>
    /// <param name="pos">λ��</param>
    /// <param name="scale">��С</param>
    /// <param name="rotate">��ת</param>
    public T CreateNpc<T>(int id, Vector3 pos, Vector3 scale, Vector3 rotate) where T: NpcBase
    {
        var npc = MapConfig.Instance.CreateNpc(id, pos, scale, rotate, false);
        npc.OnCreate();
        return npc as T;
    }   
}
