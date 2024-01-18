using System;
using UnityEngine.UI;
public class BaseStartUI : UIBase
{
    //Tip: 设置按钮
    public Button SetBtn;
    //Tip: 新游戏按钮
    public Button NewGameBtn;
    //Tip: 继续游戏按钮
    public Button ContinueBtn;


    public override void Awake()
    {
        base.Awake();
        Init();
    }

    /// <summary>
    /// Init:
    /// 初始化界面
    /// </summary>
    private void Init() 
    {
        //Tip: 设置按钮点击事件
        SetBtn.onClick.AddListener(() =>
        {
            SetOnclick();
        });
        //Tip: 新游戏按钮点击事件
        NewGameBtn.onClick.AddListener(() =>
        {
            NewGameOnclick();
        });

        //Tip: 继续游戏按钮点击事件
        ContinueBtn.onClick.AddListener(() =>
        {
            ContinueOnclick();
        });
    }

    /// <summary>
    /// Callback:
    /// 设置按钮点击
    /// </summary>
    public void SetOnclick() 
    {
        UIManager.Instance.OpenUI<SetUI>();
    }

    /// <summary>
    /// Callback:
    /// 新游戏按钮点击
    /// </summary>
    public void NewGameOnclick()
    {
        //GameBaseData.levelType = LevelType.HAVESUBTITLE;
        //GameBaseData.Chapter = 1001;
        //GameBaseData.Level = 1;
        LoadManager.Instance.LoadSceneAsync("Demo", null,
            () =>
            {
            });
        //GameBaseData.EventName = "InitialStoryEvent";
    }

    /// <summary>
    /// Callback:
    /// 继续游戏按钮点击
    /// </summary>
    public void ContinueOnclick()
    {
        UIManager.Instance.OpenUI<TestSaveUI>();
    }
     
}
