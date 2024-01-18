using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogItem : InteractableObject
{
    //Tip: 对话框距离人物的偏移量
    public float offset = 1.5f; 
    //Tip: 对话的内容
    public Text contentText;
    //Tip: 包含的任务
    public TaskCfg taskcfg;
    //Tip: 与目标任务的距离
    public float taskDistance;
    //Tip： 任务目标篇角度
    public Vector2 targetAngle;
    //Tip: Dialog对话框的背景图片
    public Image image;
    //Tip: 材质
    public Material imgmaterial;
    //Tip: 文本材质
    public Material textMaterial;
   

    /// <summary>
    /// Base: oncreate
    /// 创造时触发
    /// </summary>
    public override void OnCreate()
    {
        base.OnCreate();
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = Camera.main;
        contentText.material = new Material(textMaterial);
        image.material = new Material(imgmaterial);
    }

    /// <summary>
    /// Init:
    /// 初始化任务
    /// </summary>
    public void InitTask() 
    {
        var tasks = TaskManager.Instance.GetCurrentLevelTask();
        foreach (var item in tasks)
        {
            if (item.Event.Equals("DialogEvent") && int.Parse(item.Args[0]) == followTarget.ID)
            {
                taskcfg = item;
                targetAngle = new Vector2(item.Angles[0], item.Angles[1]);
            }
        }
    }

    /// <summary>
    /// Base: 
    /// 设置跟随目标
    /// </summary>
    /// <param name="npc"></param>
    public override void SetFollow(NpcBase npc)
    {
        base.SetFollow(npc);
        InitTask();
    }

    /// <summary>
    /// Set:
    /// 设置Dialog对话框的内容
    /// </summary>
    /// <param name="val"></param>
    public void SetDialogText(string val) 
    {
        if (GameBaseData.language == Language.Chinese)
        {
            contentText.text = val;
            return;
        }
        contentText.text = GetLanguageValue(GameBaseData.language);
    }

    /// <summary>
    /// Event:
    /// 切换语言事件执行了
    /// </summary>
    /// <param name="msgData"></param>
    public override void OnSwitchLanguageEvent(MsgData msgData)
    {
        base.OnSwitchLanguageEvent(msgData);
        var language = (Language)msgData.arg1;
        if (languagearrayDic == null)
            return;
        foreach (var item in languageDic)
        {
            if (item.Key == language)
            {
                SetDialogText(item.Value);
            }
        }
    }

    /// <summary>
    /// Callback:
    /// 拖动时执行
    /// </summary>
    public override void OnDray()
    {
        base.OnDray();
        if (complete)
        {
            image.material.SetFloat("_GlitchAmount", 0);
            contentText.material.SetFloat("_GlitchAmount", 0);
        }
        if (taskcfg == null )
            return;
        taskDistance = Vector2.Distance(targetAngle, new Vector2(GameManager.Instance.cameraRotateX, GameManager.Instance.cameraRotateY));
        if (taskDistance <= 20)
        {
            image.material.SetFloat("_GlitchAmount", taskDistance);
            contentText.material.SetFloat("_GlitchAmount", taskDistance);
        }
        else
        {
            image.material.SetFloat("_GlitchAmount", 20);
            contentText.material.SetFloat("_GlitchAmount", 20);
        }       
    }



}
