using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Npc基类 普通的NPC！！
/// </summary>
public class NpcBase : MonoBehaviour
{
    //Tip: ID
    public int ID;
    //Tip: 配置表数据
    public NpcCfg Cfg;
    //Tip: 主物体
    public GameObject owner;
    //Tip: 物体身上的Rigidbody
    [HideInInspector]
    public Rigidbody2D m_Rigidbody;
    //Tip: 主角的SpriteReader
    [HideInInspector]
    public SpriteRenderer m_spriteRenderer;
    //Tip: 主角的Box
    [HideInInspector]
    public BoxCollider2D m_boxCollider2D;

    /// <summary>
    /// Base:awake
    /// </summary>
    public virtual void Awake()
    {
        InitData();
    }

    /// <summary>
    /// Init:
    /// 初始化数据
    /// </summary>
    public virtual void InitData()
    {
        m_Rigidbody = owner.GetComponent<Rigidbody2D>();
        m_spriteRenderer = owner.GetComponent<SpriteRenderer>();
        m_boxCollider2D = owner.GetComponent<BoxCollider2D>();
    }

    /// <summary>
    /// Base:start
    /// </summary>
    public virtual void Start()
    {
        OnCreate();
    }


    /// <summary>
    /// Callback:
    /// 创造时
    /// </summary>
    public virtual void OnCreate() 
    {
        Cfg = NPCManager.Instance.InitNpcData(this);
        DebugEX.Log("Npc初始化成功", ID,gameObject.name);
        InitConfigData();   
    }

    /// <summary>
    /// Init:
    /// 初始化配置表数据
    /// </summary>
    public void InitConfigData() {
        if (Cfg == null)
            return;
    }
    
    /// <summary>
    /// Get:
    /// 获取该物体子物体下的一个可交互物体
    /// </summary>
    /// <param name="targetName"></param>
    /// <returns></returns>
    public Transform GetInteractableObject(string targetName)
    {
        var item = FindObjectByName(transform, targetName);
        if (item != null && item.GetComponent<InteractableObject>() != null)
            return item;
        return null;
    }

    /// <summary>
    /// Find:
    /// 获取到一个物体
    /// </summary>
    /// <returns></returns>
    Transform FindObjectByName(Transform parent, string targetName)
    {
        Transform result = null;
        // 遍历子物体
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
            {
                result = child;
                break;
            }
            // 递归调用，在子物体中查找
            Transform foundObject = FindObjectByName(child, targetName);
            if (foundObject != null)
            {
                result = foundObject;
                break;
            }
        }
        return result;
    }





}


