using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipBase : UIBase
{
    //Tip: �����ı���
    public Text contentText;
    //Tip: �����ı���
    public Text titleText;
    //Tip: ȷ����ť
    public Button yesBtn;
    //Tip: ȡ����ť
    public Button cancelBtn;
    //Tip: ȷ����ť�ı�
    public Text yesText;
    //Tip: ȡ����ť�ı�
    public Text cancelText;
    //Tip: �رհ�ť
    public Button closeBtn;
    //Tip: �رհ�ť������
    public GameObject closeObj;

    /// <summary>
    /// Init:
    /// ��ʼ��
    /// </summary>
    /// <param name="dialogArgs">�Զ������</param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        closeBtn.onClick.AddListener(() => {
            Close();
        });
    }

    /// <summary>
    /// Init:
    /// ��ʼ������
    /// </summary>
    /// <param name="title">����</param>
    /// <param name="content">����</param>
    /// <param name="yesAction">ȷ���¼�</param>
    /// <param name="cancelAction">ȡ���¼�</param>
    public void Init(string title, string content, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        titleText.text = title;
        contentText.text = content;
        yesBtn.onClick.RemoveAllListeners();
        cancelBtn.onClick.RemoveAllListeners();
        closeBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() => { yesAction(); });
        closeObj.SetActive(hasCloseBtn);
        cancelBtn.onClick.AddListener(() => {
            if (cancelAction == null)
                Close();
            else
                cancelAction?.Invoke();
        });
        closeBtn.onClick.AddListener(() => {
            Close();
        });
    }

    /// <summary>
    /// Init��ʼ������ ���Ӱ棨���Զ���ȷ����ť��ȡ����ť�ı��أ�
    /// </summary>
    /// <param name="title"></param>
    /// <param name="content"></param>
    /// <param name="yesContent"></param>
    /// <param name="cancelContent"></param>
    /// <param name="yesAction"></param>
    /// <param name="cancelAction"></param>
    public void Init(string title, string content, string yesContent, string cancelContent, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        Init(title, content, yesAction, cancelAction, hasCloseBtn);
        yesText.text = yesContent;
        cancelText.text = cancelContent;
    }

    
    /// <summary>
    /// Add:
    /// ��ӱ���
    /// </summary>
    /// <returns></returns>
    public TipBase AddTitle(string title) 
    {
        titleText.text = title;
        return this;
    }


    /// <summary>
    /// Add:
    /// �������
    /// </summary>
    /// <returns></returns>
    public TipBase AddContent(string content)
    {
        contentText.text = content;
        return this;
    }

    /// <summary>
    /// Add:
    /// ���ȷ������¼�
    /// </summary>
    /// <param name="action">�¼�</param>
    /// <returns></returns>
    public TipBase AddYesClickEvent(Action action)
    {
        yesBtn.onClick.AddListener(() => { action(); });
        return this;
    }

    /// <summary>
    /// Add:
    /// ���No����¼�
    /// </summary>
    /// <param name="action">�¼�</param>
    /// <returns></returns>
    public TipBase AddNoClickEvent(Action action)
    {
        cancelBtn.onClick.AddListener(() => { action(); });
        return this;
    }

    /// <summary>
    /// Add:
    /// ���ȷ���ı�
    /// </summary>
    /// <param name="text">�ı�����</param>
    /// <returns></returns>
    public TipBase AddYesText(string text) 
    {
        yesText.text = text;
        return this;
    }

    /// <summary>
    /// Add:
    /// ���No�ı�
    /// </summary>
    /// <param name="text">�ı�����</param>
    /// <returns></returns>
    public TipBase AddNoText(string text) 
    {
        cancelText.text = text;
        return this;
    }

    /// <summary>
    /// Enable:
    /// �Ƿ�����Close��ť
    /// </summary>
    /// <param name="enable"></param>
    /// <returns></returns>
    public TipBase EnableClose(bool enable)
    {
        closeObj.SetActive(enable);
        return this;
    }

    /// <summary>
    /// Set:
    /// ���������С
    /// </summary>
    /// <param name="tipUi">UI����</param>
    /// <param name="size">�����С</param>
    public TipBase FontSize(TipBaseUI tipUi, int size) 
    {
        if (tipUi == TipBaseUI.Title)
            titleText.fontSize = size;
        else if (tipUi == TipBaseUI.Content)
            contentText.fontSize = size;
        else if (tipUi == TipBaseUI.Yes)
            yesText.fontSize = size;
        else if(tipUi == TipBaseUI.No)
            cancelText.fontSize = size;
        return this;
    }


    /// <summary>
    /// Set:
    /// ��������Ӵ�
    /// </summary>
    /// <param name="tipUi">UI����</param>
    /// <param name="size">�����С</param>
    public TipBase FontBold(TipBaseUI tipUi)
    {
        if (tipUi == TipBaseUI.Title)
            titleText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.Content)
            contentText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.Yes)
            yesText.fontStyle = FontStyle.Bold;
        else if (tipUi == TipBaseUI.No)
            cancelText.fontStyle = FontStyle.Bold;
        return this;
    }

}

public enum TipBaseUI
{ 
    Title,
    Content,
    Yes,
    No
}
