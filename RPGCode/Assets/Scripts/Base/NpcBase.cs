using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Npc���� ��ͨ��NPC����
/// </summary>
public class NpcBase : MonoBehaviour
{
    //Tip: ID
    public int ID;
    //Tip: ���ñ�����
    public NpcCfg Cfg;
    //Tip: ������
    public GameObject owner;
    //Tip: �������ϵ�Rigidbody
    [HideInInspector]
    public Rigidbody2D m_Rigidbody;
    //Tip: ���ǵ�SpriteReader
    [HideInInspector]
    public SpriteRenderer m_spriteRenderer;
    //Tip: ���ǵ�Box
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
    /// ��ʼ������
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
    /// ����ʱ
    /// </summary>
    public virtual void OnCreate() 
    {
        Cfg = NPCManager.Instance.InitNpcData(this);
        DebugEX.Log("Npc��ʼ���ɹ�", ID,gameObject.name);
        InitConfigData();   
    }

    /// <summary>
    /// Init:
    /// ��ʼ�����ñ�����
    /// </summary>
    public void InitConfigData() {
        if (Cfg == null)
            return;
    }
    
    /// <summary>
    /// Get:
    /// ��ȡ�������������µ�һ���ɽ�������
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
    /// ��ȡ��һ������
    /// </summary>
    /// <returns></returns>
    Transform FindObjectByName(Transform parent, string targetName)
    {
        Transform result = null;
        // ����������
        foreach (Transform child in parent)
        {
            if (child.name == targetName)
            {
                result = child;
                break;
            }
            // �ݹ���ã����������в���
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


