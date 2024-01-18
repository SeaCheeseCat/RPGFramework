using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonologueUI : UIBase
{
    //Tip: 对话Text文本
    public Text talkText;
    //Tip: 角色UI
    public Image characterIamge;
    //Tip: 下一步按钮
    public Button nextBtn;
    //Tip: 下一步下标
    private int nextIndex = 0;
    //Tip: 当前的文本内容
    private string[] contents;
    //Tip：对话速度
    private const float talkSpeed = 0.4f;
    //Tip: 打开的Y值
    private const float openUiY = 0f;
    //Tip: 关闭的Y值
    private const float closeUiY = -350f;
    //Tip: 动画滑动速度
    private const float slideSpeed = 0.5f;

    /// <summary>
    /// Base: init
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
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => { NextOnclick(); });
        nextBtn.gameObject.SetActive(false);
        talkText.text = "";
        StartCoroutine(OnStart());*/
    }

    /// <summary>
    /// Callback：
    /// 开启
    /// </summary>
    IEnumerator OnStart()
    {
        yield return new WaitForSeconds(0.3f);
        talkText.text = "";
        talkText.DOText(contents[nextIndex], talkSpeed);
        yield return new WaitForSeconds(talkSpeed - 0.1f);
        nextBtn.gameObject.SetActive(true);
    }

    /// <summary>
    /// Callback:
    /// 下一步按钮点击返回事件
    /// </summary>
    public void NextOnclick()
    {
        nextBtn.gameObject.SetActive(false);
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
        talkText.text = text;
        talkText.DOFade(1, talkSpeed).OnComplete(() => {
            nextBtn.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// Do:
    /// 执行下一段文本这里执行会比上面早0.1秒显示可以点击下一步的按钮，体感上会比较好一些
    /// </summary>
    /// <returns></returns>
    IEnumerator DoNextTextIenumerator()
    {
        talkText.text = "";
        yield return new WaitForSeconds(0.5f);
        var index = nextIndex;
        var text = contents[index];
        talkText.DOText(text,talkSpeed);
        yield return new WaitForSeconds(talkSpeed - 0.1f);
        nextBtn.gameObject.SetActive(true);
    }

    /// <summary>
    /// Open:
    /// 打开时的动画
    /// </summary>
    public override void OpenWithAnimation()
    {
        transform.DOLocalMoveY(openUiY, slideSpeed);
        canvasGroup.DOFade(1, 0.5f);
    }

    /// <summary>
    /// Close:
    /// 关闭时的动画
    /// </summary>
    public override void CloseWithAnimation()
    {
        transform.DOLocalMoveY(closeUiY, slideSpeed);
        canvasGroup.DOFade(0, 0.5f);
    }

}
