using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetUI : UIBase
{
    //Tip：上面的Tab
    public SetTabItemUI[] tabs;
    //Tip: 当前的Tab
    public SetTabItemUI currentTabItem;
    //Tip: 上次触发的Tab
    public SetTabItemUI lastTabItem;
    //Tip: 当前的List
    public SetItemUI currentSetItem;
    //Tip: 上次触发的Tab
    public SetItemUI lastSetItem;
    //Tip: 内容
    public Transform contentParents;
    //Tip: Tab的标记
    private int tabIndex = 0;
    //Tip: 列表标记
    private int listIndex = 0;
    //Tip: 基础设置
    private List<SetItem> baseItems = new List<SetItem>();
    //Tip: 参数设置
    private List<SetItem> argItems = new List<SetItem>();
    //Tip: 控制设置
    private List<SetItem> contorlItems = new List<SetItem>();
    //Tip: 离开游戏
    private List<SetItem> leaveItems = new List<SetItem>();
    //Tip: 当前的设置状态
    private SetState setState;
    //Tip: 当前创建出的SetItem
    private List<SetItemUI> currentSetItems = new List<SetItemUI>();
    //Tip: 已经选中Item状态
    private bool isAdjustItem;
    //Tip：ScrollRect物体
    public ScrollRect scrollRect;
    //Tip: 滑动速度 或者说 增量
    private const float scrollSpeed = 1f;
    //Tip: 一页Scroll最多容纳的数量
    private const int maxScrollNum = 5;

    /// <summary>
    /// Base:init
    /// </summary>
    /// <param name="dialogArgs"></param>
    internal override void Init(params object[] dialogArgs)
    {
        base.Init(dialogArgs);
        InitData();
    }



    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    public void InitData() 
    {
        var base1 = new SetItem("全屏显示", new string[] {"开","关" }, SetSwitchType.SELECT);
        base1.Create<SetItemUI>();
        var base2 = new SetItem("画面亮度等级", new string[] { "10", "20","30","40" }, SetSwitchType.SELECT);
        base2.Create<SetItemUI>();
        var base3 = new SetItem("垂直同步", new string[] { "开", "关" }, SetSwitchType.SELECT);
        base3.Create<SetItemUI>();
        var base4 = new SetItem("音乐音量", new string[] { "10", "20","30","40","50","60" }, SetSwitchType.SELECT);
        base4.Create<SetItemUI>();
        var base5 = new SetItem("音效音量", new string[] { "10", "20", "30", "40", "50", "60" }, SetSwitchType.SELECT);
        base5.Create<SetItemUI>();
        var base6 = new SetItem("保存照片", new string[] { "开","关" }, SetSwitchType.SELECT);
        base6.Create<SetItemUI>();
        baseItems.Add(base1);
        baseItems.Add(base2);
        baseItems.Add(base3);
        baseItems.Add(base4);
        baseItems.Add(base5);
        baseItems.Add(base6);

        var arg1 = new SetItem("Arg1", new string[] { "开", "关" }, SetSwitchType.SELECT);
        arg1.Create<SetItemUI>();
        var arg2 = new SetItem("Arg2", new string[] { "开", "关" }, SetSwitchType.SELECT);
        arg2.Create<SetItemUI>();
        var arg3 = new SetItem("Arg3", new string[] { "开", "关" }, SetSwitchType.SELECT);
        arg3.Create<SetItemUI>();
        argItems.Add(arg1);
        argItems.Add(arg2);
        argItems.Add(arg3);

        var contorl1 = new SetItem("Contorl1", new string[] { "开", "关" }, SetSwitchType.SELECT);
        contorl1.Create<SetItemUI>();
        var contorl2 = new SetItem("Contorl2", new string[] { "开", "关" }, SetSwitchType.SELECT);
        contorl2.Create<SetItemUI>();
        var contorl3 = new SetItem("Contorl3", new string[] { "开", "关" }, SetSwitchType.SELECT);
        contorl3.Create<SetItemUI>();
        contorlItems.Add(contorl1);
        contorlItems.Add(contorl2);
        contorlItems.Add(contorl3);

        var level1 = new SetItem("返回标题", new string[] { }, SetSwitchType.NULL);
        level1.Create<BackInitSetUI>();
        var level2 = new SetItem("离开游戏", new string[] { }, SetSwitchType.NULL);
        level2.Create<ExitGameSetUI>();
        leaveItems.Add(level1);
        leaveItems.Add(level2);

        SelectTab(0);
        listIndex = 0;
        SelectList(0);
    }


    /// <summary>
    /// Clear:
    /// 清除全部的Item
    /// </summary>
    public void ClearItem() 
    {
        var clears = new GameObject[currentSetItems.Count];
        for (int i = 0; i < currentSetItems.Count; i++)
        {
            clears[i] = currentSetItems[i].gameObject;
        }

        foreach (var item in clears)
        {
            PoolManager.Instance.UiWithSetItemPool.Recycle(item.gameObject);
        }
        currentSetItems.Clear();
    }

    /// <summary>
    /// Create:
    /// 创建一个列表
    /// </summary>
    /// <param name="state"></param>
    public void CreateList(SetState state)
    {
        List<SetItem> list = new List<SetItem>();
        if (state == SetState.Base)
            list = baseItems;
        else if (state == SetState.Arg)
            list = argItems;
        else if (state == SetState.Control)
            list = contorlItems;
        else if (state == SetState.Leave)
            list = leaveItems;
        for (int i = 0; i < list.Count; i++)
        {
            var data = list[i];
            var item = CreateItem(data.instantType);
            item.OnCreate();
            item.Init(data.name, data.type, data.selects);
            item.transform.SetParent(contentParents);
            currentSetItems.Add(item);
        }
    }

    /// <summary>
    /// Create:
    /// 创建一个物体
    /// </summary>
    public SetItemUI CreateItem(string type) 
    {
        GameObject prefab = PoolManager.Instance.UiUnitPool.Spawn("UI/SetItem");
        if (prefab == null)
        {
            prefab = ResourceManager.LoadPrefabSync("UI/" + name) as GameObject;
            prefab.name = name;
        }
        if (prefab != null)
        {
            // 使用 Type.GetType 获取类型的 Type 对象
            Type componentType = Type.GetType(type);
            // 获取或添加组件
            SetItemUI itemComponent = prefab.GetComponent(componentType) as SetItemUI;
            if (itemComponent == null)
            {
                itemComponent = prefab.AddComponent(componentType) as SetItemUI;
            }
            return itemComponent;
        }
        return null;
    }

    /// <summary>
    /// Base:update
    /// </summary>
    private void Update()
    {
        OnKeyboardInput();
    }

    /// <summary>
    /// Callback:
    /// 监控键盘按下
    /// </summary>
    public void OnKeyboardInput() 
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (currentSetItem != null)
            {
                isAdjustItem = true;
                currentSetItem.OnAdjust();
            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentSetItem != null && isAdjustItem)
            {
                isAdjustItem = false;
                currentSetItem.OnCancelAdjust();
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (listIndex <= 0 || isAdjustItem)
                return;
            if (listIndex >= maxScrollNum)
                DoScrollUp();
            listIndex--;
            SelectList(listIndex);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (listIndex >= currentSetItems.Count - 1 || isAdjustItem)
                return;
            listIndex++;
            if (listIndex >= maxScrollNum)
                DoScrollDown();
            SelectList(listIndex);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (tabIndex <= 0 || isAdjustItem)
                return;
            tabIndex--;                
            SelectTab(tabIndex);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (tabIndex >= tabs.Length-1 || isAdjustItem)
                return;
            tabIndex++;
            SelectTab(tabIndex);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    
    /// <summary>
    /// Select:
    /// 选择上部的Tab
    /// </summary>
    /// <param name="index">下标</param>
    public void SelectTab(int index)
    {
        var tab = tabs[index];
        setState = (SetState)index;
        currentTabItem = tab;
        if (lastTabItem != null)
        {
            lastTabItem.selectImage.gameObject.SetActive(false);
        }
        currentTabItem.selectImage.gameObject.SetActive(true);
        lastTabItem = currentTabItem;
        ClearItem();
        CreateList((SetState)index);
        listIndex = 0;
        SelectList(0);
        if (currentSetItems.Count < maxScrollNum)
            scrollRect.enabled = false;
        else
            scrollRect.enabled = true;

    }

    /// <summary>
    /// Select:
    /// 选择中间部分的列表
    /// </summary>
    /// <param name="index">下标</param>
    public void SelectList(int index)
    {
        if (index == -1)
        {
            lastSetItem.OnCancelSelect();
            lastSetItem = null;
            return;
        }
        var list = currentSetItems[index];
        currentSetItem = list;
        if (lastSetItem != null)
        {
            lastSetItem.OnCancelSelect();
        }
        currentSetItem.OnSelected();
        lastSetItem = currentSetItem;
    }

    /// <summary>
    /// CallBack:
    /// 关闭状态下触发
    /// </summary>
    public override void OnClose()
    {
        base.OnClose();
        
    }

    /// <summary>
    /// Callback:
    /// UImanager里通用触发
    /// </summary>
    internal override void OnDeath()
    {
        base.OnDeath();
        Recycle();
    }

    /// <summary>
    /// Recycle:
    /// 回收数据
    /// </summary>
    public void Recycle() 
    {
        ClearItem();
        SelectTab(0);
        isAdjustItem = false;
        tabIndex = 0;
        listIndex = 0;
        currentSetItem = null;
        lastSetItem = null;
        currentTabItem = null;
        lastTabItem = null;
        baseItems.Clear();
        argItems.Clear();
        contorlItems.Clear();
        leaveItems.Clear();
    }

    /// <summary>
    /// Do:
    /// Scorll向上滚动
    /// </summary>
    void DoScrollUp()
    {
        // 向上滚动一定量
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition + scrollSpeed);
    }

    /// <summary>
    /// Do:
    /// Scorll向下滚动
    /// </summary>
    void DoScrollDown()
    {
        // 向下滚动一定量
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition - scrollSpeed);
    }

}

public class SetItem
{
    public string name;
    public string[] selects;
    public SetSwitchType type;
    public string instantType;

    public SetItem(string name, string[] selects, SetSwitchType type) 
    {
        this.name = name;
        this.selects = selects;
        this.type = type;
    }

    /// <summary>
    /// 创造一个都属于其的物体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="t"></param>
    public void Create<T>() where T: SetItemUI
    {
        instantType = typeof(T).FullName;
    }

}

public enum SetState
{
    Base = 0,
    Arg = 1,
    Control = 2,
    Leave = 3
}