using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 测试UI
/// </summary>
public class TestSaveUI : UIBase
{
    //Tip: 仅作为测试使用    美术给资源后再替换
    public Button closeBtn;
    //Tip: 关卡数据 
    public LevelItem[] levelItems;
    //Tip: 当前选中关卡
    public LevelItem currentItem;
    //Tip: 上次选中关卡
    public LevelItem lastItem;
    //Tip: E按键前面的圆形进度框
    public Image eFillImage;
    //Tip: R按键前面的圆形进度框
    public Image rFillImage;
    //Tip: 当前选中关卡下标
    private int currentlevel;
    //Tip: E长按标记
    private bool isEPressed = false;
    //Tip: R长按标记
    private bool isRPressed = false;
    //Tip: 长按时间 单位：秒
    private float pressDuration = 1f; 
    //Tip: E已经长按的时间
    private float pressTimerE = 0f;
    //Tip: R已经长按的时间
    private float pressTimerR = 0f;
    

    /// <summary>
    /// Base: init
    /// 初始化
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
    /// 刷新关卡信息
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
    /// 载入关卡数据
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
    /// 按键按下
    /// </summary>
    public void OnKeyInput()
    {
        CheckLongPress(KeyCode.E, ref isEPressed, ref pressTimerE,()=> { ContinueLevel(currentlevel);});
        CheckLongPress(KeyCode.R, ref isRPressed, ref pressTimerR,()=> { ResetLevel(currentlevel);});
    }

    /// <summary>
    /// Check:
    /// 检测按键长按
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
                        // 重置状态
                        isPressed = false;
                        pressTimer = 0f;
                        callback.Invoke();
                        DebugEX.Log("执行了");
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
    /// 更改Fill图片的进度值
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
    /// 选择关卡按钮被点击
    /// </summary>
    /// <param name="index">关卡下标</param>
    public void LevelOnclick(int index) 
    {
        currentItem = levelItems[index];
        if (!currentItem.unlock)
        {
            DebugEX.Log("该关卡并没有解锁", currentItem.chatper, currentItem.level);
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
            .AddTitle("载入关卡")
                .FontSize(TipBaseUI.Title, 20)
            .AddContent("是否继续游戏")
                .FontBold(TipBaseUI.Content)
            .AddYesText("继续")
            .AddNoText("重新开始")
            .AddYesClickEvent(() => {
                ContinueLevel(index); })
            .AddNoClickEvent(() => { 
                ResetLevel(index); })
            .EnableClose(true);*/
    }

    /// <summary>
    /// Continue:
    /// 继续关卡
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
    /// 重置关卡
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
