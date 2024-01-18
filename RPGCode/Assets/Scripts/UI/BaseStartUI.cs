using System;
using UnityEngine.UI;
public class BaseStartUI : UIBase
{
    //Tip: ���ð�ť
    public Button SetBtn;
    //Tip: ����Ϸ��ť
    public Button NewGameBtn;
    //Tip: ������Ϸ��ť
    public Button ContinueBtn;


    public override void Awake()
    {
        base.Awake();
        Init();
    }

    /// <summary>
    /// Init:
    /// ��ʼ������
    /// </summary>
    private void Init() 
    {
        //Tip: ���ð�ť����¼�
        SetBtn.onClick.AddListener(() =>
        {
            SetOnclick();
        });
        //Tip: ����Ϸ��ť����¼�
        NewGameBtn.onClick.AddListener(() =>
        {
            NewGameOnclick();
        });

        //Tip: ������Ϸ��ť����¼�
        ContinueBtn.onClick.AddListener(() =>
        {
            ContinueOnclick();
        });
    }

    /// <summary>
    /// Callback:
    /// ���ð�ť���
    /// </summary>
    public void SetOnclick() 
    {
        UIManager.Instance.OpenUI<SetUI>();
    }

    /// <summary>
    /// Callback:
    /// ����Ϸ��ť���
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
    /// ������Ϸ��ť���
    /// </summary>
    public void ContinueOnclick()
    {
        UIManager.Instance.OpenUI<TestSaveUI>();
    }
     
}
