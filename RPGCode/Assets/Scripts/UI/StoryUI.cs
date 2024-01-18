using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 测试UI
/// </summary>
public class StoryUI : UIBase
{
    //Tip: 故事表现文本
    public Text storyText;
    //Tip: 下一步按钮
    public Button nextButton;
    //Tip: 下一步下标
    private int nextIndex = 0;
    //Tip: 当前的文本内容
    private string[] contents;
    //Tip：对话速度
    private const float talkSpeed = 0.4f;

    /// <summary>
    /// Base: init
    /// 初始化
    /// </summary>
    /// <param name="dialogArgs"></param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        InitData((int)dialogArgs[0]);
    }   
    
    /// <summary>
    /// Base:init
    /// 初始化数据
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
    /// Callback：
    /// 开启
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
    /// 下一步按钮点击返回事件
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
    /// 对话文本结束时间触发
    /// </summary>
    public void OnFinish() 
    {
        DebugEX.Log("字幕结束");
        if (GameBaseData.Chapter == 1001 && GameBaseData.Level == 1)
        {
            EventManager.Instance.TriggerEvent<InitSubtitlesEvent>();
        }
        Close();
    }


    /// <summary>
    /// Do:
    /// 执行下一段文本
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
    /// 执行下一段文本这里执行会比上面早0.1秒显示可以点击下一步的按钮，体感上会比较好一些
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
