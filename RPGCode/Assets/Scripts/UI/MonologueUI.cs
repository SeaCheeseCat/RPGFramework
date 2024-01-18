using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonologueUI : UIBase
{
    //Tip: �Ի�Text�ı�
    public Text talkText;
    //Tip: ��ɫUI
    public Image characterIamge;
    //Tip: ��һ����ť
    public Button nextBtn;
    //Tip: ��һ���±�
    private int nextIndex = 0;
    //Tip: ��ǰ���ı�����
    private string[] contents;
    //Tip���Ի��ٶ�
    private const float talkSpeed = 0.4f;
    //Tip: �򿪵�Yֵ
    private const float openUiY = 0f;
    //Tip: �رյ�Yֵ
    private const float closeUiY = -350f;
    //Tip: ���������ٶ�
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
    /// ��ʼ������
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
    /// Callback��
    /// ����
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
    /// ��һ����ť��������¼�
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
    /// �Ի��ı�����ʱ�䴥��
    /// </summary>
    public void OnFinish()
    {
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
        talkText.text = text;
        talkText.DOFade(1, talkSpeed).OnComplete(() => {
            nextBtn.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// Do:
    /// ִ����һ���ı�����ִ�л��������0.1����ʾ���Ե����һ���İ�ť������ϻ�ȽϺ�һЩ
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
    /// ��ʱ�Ķ���
    /// </summary>
    public override void OpenWithAnimation()
    {
        transform.DOLocalMoveY(openUiY, slideSpeed);
        canvasGroup.DOFade(1, 0.5f);
    }

    /// <summary>
    /// Close:
    /// �ر�ʱ�Ķ���
    /// </summary>
    public override void CloseWithAnimation()
    {
        transform.DOLocalMoveY(closeUiY, slideSpeed);
        canvasGroup.DOFade(0, 0.5f);
    }

}
