using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Npc基类 普通的NPC！！
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
    /// 模型统一改变颜色  （其实是2D纸片）
    /// </summary>
    public void ModelDoColor(Color color) 
    {
        foreach (var item in ModelSprite)
        {
            item.color = color;
        }
    }

    /// <summary>
    /// 模型统一改变透明度
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
        if (Cfg.Dialog != "")
            ItemManager.Instance.CreateDialogItem(this,Cfg.Dialog, Cfg.DialogLanguage);

        if (Cfg.Islens)
            GameManager.Instance.AddFacingCamera(this.transform);
    }

    /// <summary>
    /// Set:
    /// 设置一个Dialog
    /// </summary>
    /// <param name="dialog"></param>
    public void SetDialog(DialogItem dialog)
    {
        Dialog = dialog;
    }

    /// <summary>
    /// Delete:
    /// 删除一个Dialog
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


