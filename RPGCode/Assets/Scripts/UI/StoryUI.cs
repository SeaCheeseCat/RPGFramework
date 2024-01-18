using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ����UI
/// </summary>
public class StoryUI : UIBase
{
    //Tip: ���±����ı�
    public Text storyText;
    //Tip: ��һ����ť
    public Button nextButton;
    //Tip: ��һ���±�
    private int nextIndex = 0;
    //Tip: ��ǰ���ı�����
    private string[] contents;
    //Tip���Ի��ٶ�
    private const float talkSpeed = 0.4f;

    /// <summary>
    /// Base: init
    /// ��ʼ��
    /// </summary>
    /// <param name="dialogArgs"></param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        InitData((int)dialogArgs[0]);
    }   
    
    /// <summary>
    /// Base:init
    /// ��ʼ������
    /// </summary>
    public void InitData(int id) 
    {
        /*var cfg = ConfigManager.GetConfigByID<StoryCfg>(id);
        contents = cfg.Content;
        nextIndex = 0;
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(() => { NextOnclick(); });
        nextButton.gameObject.SetActive(false);
        storyText.text = "";
        StartCoroutine(OnStart());*/
    }

    /// <summary>
    /// Callback��
    /// ����
    /// </summary>
    IEnumerator OnStart()
    {
        yield return new WaitForSeconds(0.3f);
        storyText.text = contents[nextIndex];
        storyText.DOFade(1, talkSpeed);
        yield return new WaitForSeconds(talkSpeed - 0.1f);
        nextButton.gameObject.SetActive(true);

    }

    /// <summary>
    /// Callback:
    /// ��һ����ť��������¼�
    /// </summary>
    public void NextOnclick()
    {
        nextButton.gameObject.SetActive(false);
        nextIndex++;
        if (nextIndex >= contents.Length)
        {
            OnFinish();
            return;
        }
        StartCoroutine(DoNextTextIenumerator());
    }

    /// <summary>
    /// Callback:
    /// �Ի��ı�����ʱ�䴥��
    /// </summary>
    public void OnFinish() 
    {
        DebugEX.Log("��Ļ����");
        if (GameBaseData.Chapter == 1001 && GameBaseData.Level == 1)
        {
            EventManager.Instance.TriggerEvent<InitSubtitlesEvent>();
        }
        Close();
    }


    /// <summary>
    /// Do:
    /// ִ����һ���ı�
    /// </summary>
    public void DoNextText()
    {
        var index = nextIndex;
        var text = contents[index];
        storyText.text = text;
        storyText.DOFade(1, talkSpeed).OnComplete(()=> {
            nextButton.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// Do:
    /// ִ����һ���ı�����ִ�л��������0.1����ʾ���Ե����һ���İ�ť������ϻ�ȽϺ�һЩ
    /// </summary>
    /// <returns></returns>
    IEnumerator DoNextTextIenumerator()
    {
        storyText.DOFade(0, talkSpeed);
        yield return new WaitForSeconds(talkSpeed);
        var index = nextIndex;
        var text = contents[index];
        storyText.text = text;
        storyText.DOFade(1, talkSpeed);
        yield return new WaitForSeconds(talkSpeed - 0.1f);
        nextButton.gameObject.SetActive(true);
    }

}
