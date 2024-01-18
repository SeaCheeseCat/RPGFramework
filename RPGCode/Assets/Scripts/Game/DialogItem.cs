using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogItem : InteractableObject
{
    //Tip: �Ի�����������ƫ����
    public float offset = 1.5f; 
    //Tip: �Ի�������
    public Text contentText;
    //Tip: ����������
    public TaskCfg taskcfg;
    //Tip: ��Ŀ������ľ���
    public float taskDistance;
    //Tip�� ����Ŀ��ƪ�Ƕ�
    public Vector2 targetAngle;
    //Tip: Dialog�Ի���ı���ͼƬ
    public Image image;
    //Tip: ����
    public Material imgmaterial;
    //Tip: �ı�����
    public Material textMaterial;
   

    /// <summary>
    /// Base: oncreate
    /// ����ʱ����
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
    /// ��ʼ������
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
    /// ���ø���Ŀ��
    /// </summary>
    /// <param name="npc"></param>
    public override void SetFollow(NpcBase npc)
    {
        base.SetFollow(npc);
        InitTask();
    }

    /// <summary>
    /// Set:
    /// ����Dialog�Ի��������
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
    /// �л������¼�ִ����
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
    /// �϶�ʱִ��
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
