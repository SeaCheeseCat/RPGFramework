using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����UI
/// </summary>
public class TestSaveUI : UIBase
{
    //Tip: ����Ϊ����ʹ��    ��������Դ�����滻
    public Button closeBtn;
    //Tip: �ؿ����� 
    public LevelItem[] levelItems;
    //Tip: ��ǰѡ�йؿ�
    public LevelItem currentItem;
    //Tip: �ϴ�ѡ�йؿ�
    public LevelItem lastItem;
    //Tip: E����ǰ���Բ�ν��ȿ�
    public Image eFillImage;
    //Tip: R����ǰ���Բ�ν��ȿ�
    public Image rFillImage;
    //Tip: ��ǰѡ�йؿ��±�
    private int currentlevel;
    //Tip: E�������
    private bool isEPressed = false;
    //Tip: R�������
    private bool isRPressed = false;
    //Tip: ����ʱ�� ��λ����
    private float pressDuration = 1f; 
    //Tip: E�Ѿ�������ʱ��
    private float pressTimerE = 0f;
    //Tip: R�Ѿ�������ʱ��
    private float pressTimerR = 0f;
    

    /// <summary>
    /// Base: init
    /// ��ʼ��
    /// </summary>
    /// <param name="dialogArgs"></param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        closeBtn.onClick.AddListener(() => { Close(); });
        var unLockLevels = LevelManager.Instance.CheckLevelCompleteState(GameBase.Instance.saveData.levelData);
        for (int i = 0; i < levelItems.Length; i++)
        {
            var index = i;
            var item = levelItems[i];
            item.Btn.onClick.AddListener(() => { LevelOnclick(index); });
            foreach (var unlock in unLockLevels)
            {
                if (unlock.Level == item.level && unlock.Chapter == item.chatper)
                    item.unlock = true;
                
            }
        }
        LoadData(1);
    }

    /// <summary>
    /// Update:
    /// ˢ�¹ؿ���Ϣ
    /// </summary>
    public void UpdateLevelInfo() 
    {
        var unLockLevels = LevelManager.Instance.CheckLevelCompleteState(GameBase.Instance.saveData.levelData);
        for (int i = 0; i < levelItems.Length; i++)
        {
            var item = levelItems[i];
            foreach (var unlock in unLockLevels)
            {
                if (unlock.Level == item.level && unlock.Chapter == item.chatper)
                    item.unlock = true;
            }
        }
    }

    /// <summary>
    /// Load:
    /// ����ؿ�����
    /// </summary>
    /// <param name="chapter"></param>
    public void LoadData(int chapter) 
    {
        var datas = GameBase.Instance.GetLevelData(chapter);
        for (int i = 0; i < levelItems.Length; i++)
        {
            var item = levelItems[i];
            item.valText.text = "0"+"/" + TaskManager.Instance.GetTaskCount(item.chatper, item.level);

            foreach (var data in datas)
            {
                if (item.chatper == data.chapter && item.level == data.level)
                {
                    item.valText.text = data.completedTask.Count + "/" + TaskManager.Instance.GetTaskCount(item.chatper, item.level);
                }
            }
        }
    }

    /// <summary>
    /// Base: update
    /// </summary>
    private void Update()
    {
        OnKeyInput();
    }

    /// <summary>
    /// Callback:
    /// ��������
    /// </summary>
    public void OnKeyInput()
    {
        CheckLongPress(KeyCode.E, ref isEPressed, ref pressTimerE,()=> { ContinueLevel(currentlevel);});
        CheckLongPress(KeyCode.R, ref isRPressed, ref pressTimerR,()=> { ResetLevel(currentlevel);});
    }

    /// <summary>
    /// Check:
    /// ��ⰴ������
    /// </summary>
    /// <param name="key"></param>
    /// <param name="isPressed"></param>
    /// <param name="pressTimer"></param>
    /// <param name="callback"></param>
    void CheckLongPress(KeyCode key, ref bool isPressed, ref float pressTimer, Action callback)
    {
        if (Input.GetKey(key))
        {
            if (key == KeyCode.E && Input.GetKey(KeyCode.R))
                return;
            if (key == KeyCode.R && Input.GetKey(KeyCode.E))
                return;
            if (!isPressed)
            {
                isPressed = true;
                pressTimer = 0f;
            }
            else
            {
                pressTimer += Time.deltaTime;
                if (pressTimer >= pressDuration)
                {
                    if (!isEPressed || !isRPressed)
                    {
                        // ����״̬
                        isPressed = false;
                        pressTimer = 0f;
                        callback.Invoke();
                        DebugEX.Log("ִ����");
                    }
                }
                if (key == KeyCode.E)
                    ChangeFillAmount(eFillImage, pressTimer);
                else
                    ChangeFillAmount(rFillImage, pressTimer);
            }
        }
        else
        {
            isPressed = false;
            pressTimer = 0f;
            if (key == KeyCode.E)
                ChangeFillAmount(eFillImage, pressTimer);
            else
                ChangeFillAmount(rFillImage, pressTimer);
        }
    }

    /// <summary>
    /// Change:
    /// ����FillͼƬ�Ľ���ֵ
    /// </summary>
    void ChangeFillAmount(Image targetImage, float pressTimer)
    {
        if (targetImage == null)
            return;

        if (pressTimer == 0)
        {
            targetImage.fillAmount = 0;
        }
        else
        {
            float fillPercentage = pressTimer / pressDuration;
            targetImage.fillAmount = Mathf.Clamp01(fillPercentage);
        }
    }

    /// <summary>
    /// Callback:
    /// ѡ��ؿ���ť�����
    /// </summary>
    /// <param name="index">�ؿ��±�</param>
    public void LevelOnclick(int index) 
    {
        currentItem = levelItems[index];
        if (!currentItem.unlock)
        {
            DebugEX.Log("�ùؿ���û�н���", currentItem.chatper, currentItem.level);
            return;
        }
        if (lastItem != null)
        {
            lastItem.chooseObj.SetActive(false);
        }
        currentItem.chooseObj.gameObject.SetActive(true);
        lastItem = currentItem;
        currentlevel = index;
      /*  UIManager.Instance.OpenTipUI()
            .AddTitle("����ؿ�")
                .FontSize(TipBaseUI.Title, 20)
            .AddContent("�Ƿ������Ϸ")
                .FontBold(TipBaseUI.Content)
            .AddYesText("����")
            .AddNoText("���¿�ʼ")
            .AddYesClickEvent(() => {
                ContinueLevel(index); })
            .AddNoClickEvent(() => { 
                ResetLevel(index); })
            .EnableClose(true);*/
    }

    /// <summary>
    /// Continue:
    /// �����ؿ�
    /// </summary>
    /// <param name="index"></param>
    public void ContinueLevel(int index) 
    {
        var data = levelItems[index];
        GameBaseData.Chapter = data.chatper;
        GameBaseData.Level = data.level;
        GameBaseData.levelType = LevelType.COMMON;
        GameBase.Instance.LoadLevelData(GameBaseData.Chapter, GameBaseData.Level);
        UIManager.Instance.CloseAllUI();
        LoadManager.Instance.LoadSceneAsync("Demo", (progress) =>
        {
          
        },
        null);
        //LoadManager.Instance.LoadScene("Demo");
        GameBase.Instance.gameScene = GameScene.GameLevel;
    }

    /// <summary>
    /// Reset:
    /// ���ùؿ�
    /// </summary>
    public void ResetLevel(int index) 
    {
        var data = levelItems[index];
        GameBaseData.Chapter = data.chatper;
        GameBaseData.Level = data.level;
        GameBaseData.levelType = LevelType.COMMON;
        ArchiveManager.Instance.ClearLevelData(data.chatper, data.level);
        GameBase.Instance.LoadLevelData(GameBaseData.Chapter, GameBaseData.Level);
        UIManager.Instance.CloseAllUI();
        LoadManager.Instance.LoadScene("Demo");
        GameBase.Instance.gameScene = GameScene.GameLevel;
    }



}
