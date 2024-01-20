using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体管理
/// </summary>
public class UnitManager : Manager<UnitManager>
{
    /// <summary>
    /// Create:
    /// 创建一个NPC
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
    /// 创建一个NPC
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id">ID</param>
    /// <param name="pos">位置</param>
    /// <param name="scale">大小</param>
    /// <param name="rotate">旋转</param>
    public T CreateNpc<T>(int id, Vector3 pos, Vector3 scale, Vector3 rotate) where T: NpcBase
    {
        var npc = MapConfig.Instance.CreateNpc(id, pos, scale, rotate, false);
        npc.OnCreate();
        return npc as T;
    }   
}
