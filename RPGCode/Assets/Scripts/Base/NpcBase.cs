using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Npc���� ��ͨ��NPC����
/// </summary>
public class NpcBase : MonoBehaviour
{
    public int ID;
    public SpriteRenderer[] ModelSprite;
    public DialogItem Dialog;
    public NpcCfg Cfg;

    private void Start()
    {
        //OnCreate();
    }

    /// <summary>
    /// ģ��ͳһ�ı���ɫ  ����ʵ��2DֽƬ��
    /// </summary>
    public void ModelDoColor(Color color) 
    {
        foreach (var item in ModelSprite)
        {
            item.color = color;
        }
    }

    /// <summary>
    /// ģ��ͳһ�ı�͸����
    /// </summary>
    public void ModelDoFade(float val, float time = 0.2f) 
    {
        foreach (var item in ModelSprite)
        {
            item.DOFade(val, time);
        }
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
        if (Cfg.Dialog != "")
            ItemManager.Instance.CreateDialogItem(this,Cfg.Dialog, Cfg.DialogLanguage);

        if (Cfg.Islens)
            GameManager.Instance.AddFacingCamera(this.transform);
    }

    /// <summary>
    /// Set:
    /// ����һ��Dialog
    /// </summary>
    /// <param name="dialog"></param>
    public void SetDialog(DialogItem dialog)
    {
        Dialog = dialog;
    }

    /// <summary>
    /// Delete:
    /// ɾ��һ��Dialog
    /// </summary>
    public void DeleteDialog() 
    {
        if (Dialog != null)
        {
            Destroy(Dialog.gameObject);
        }
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


