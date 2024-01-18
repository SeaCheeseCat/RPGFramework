using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetUI : UIBase
{
    //Tip�������Tab
    public SetTabItemUI[] tabs;
    //Tip: ��ǰ��Tab
    public SetTabItemUI currentTabItem;
    //Tip: �ϴδ�����Tab
    public SetTabItemUI lastTabItem;
    //Tip: ��ǰ��List
    public SetItemUI currentSetItem;
    //Tip: �ϴδ�����Tab
    public SetItemUI lastSetItem;
    //Tip: ����
    public Transform contentParents;
    //Tip: Tab�ı��
    private int tabIndex = 0;
    //Tip: �б���
    private int listIndex = 0;
    //Tip: ��������
    private List<SetItem> baseItems = new List<SetItem>();
    //Tip: ��������
    private List<SetItem> argItems = new List<SetItem>();
    //Tip: ��������
    private List<SetItem> contorlItems = new List<SetItem>();
    //Tip: �뿪��Ϸ
    private List<SetItem> leaveItems = new List<SetItem>();
    //Tip: ��ǰ������״̬
    private SetState setState;
    //Tip: ��ǰ��������SetItem
    private List<SetItemUI> currentSetItems = new List<SetItemUI>();
    //Tip: �Ѿ�ѡ��Item״̬
    private bool isAdjustItem;
    //Tip��ScrollRect����
    public ScrollRect scrollRect;
    //Tip: �����ٶ� ����˵ ����
    private const float scrollSpeed = 1f;
    //Tip: һҳScroll������ɵ�����
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
    /// ��ʼ��
    /// </summary>
    public void InitData() 
    {
        var base1 = new SetItem("ȫ����ʾ", new string[] {"��","��" }, SetSwitchType.SELECT);
        base1.Create<SetItemUI>();
        var base2 = new SetItem("�������ȵȼ�", new string[] { "10", "20","30","40" }, SetSwitchType.SELECT);
        base2.Create<SetItemUI>();
        var base3 = new SetItem("��ֱͬ��", new string[] { "��", "��" }, SetSwitchType.SELECT);
        base3.Create<SetItemUI>();
        var base4 = new SetItem("��������", new string[] { "10", "20","30","40","50","60" }, SetSwitchType.SELECT);
        base4.Create<SetItemUI>();
        var base5 = new SetItem("��Ч����", new string[] { "10", "20", "30", "40", "50", "60" }, SetSwitchType.SELECT);
        base5.Create<SetItemUI>();
        var base6 = new SetItem("������Ƭ", new string[] { "��","��" }, SetSwitchType.SELECT);
        base6.Create<SetItemUI>();
        baseItems.Add(base1);
        baseItems.Add(base2);
        baseItems.Add(base3);
        baseItems.Add(base4);
        baseItems.Add(base5);
        baseItems.Add(base6);

        var arg1 = new SetItem("Arg1", new string[] { "��", "��" }, SetSwitchType.SELECT);
        arg1.Create<SetItemUI>();
        var arg2 = new SetItem("Arg2", new string[] { "��", "��" }, SetSwitchType.SELECT);
        arg2.Create<SetItemUI>();
        var arg3 = new SetItem("Arg3", new string[] { "��", "��" }, SetSwitchType.SELECT);
        arg3.Create<SetItemUI>();
        argItems.Add(arg1);
        argItems.Add(arg2);
        argItems.Add(arg3);

        var contorl1 = new SetItem("Contorl1", new string[] { "��", "��" }, SetSwitchType.SELECT);
        contorl1.Create<SetItemUI>();
        var contorl2 = new SetItem("Contorl2", new string[] { "��", "��" }, SetSwitchType.SELECT);
        contorl2.Create<SetItemUI>();
        var contorl3 = new SetItem("Contorl3", new string[] { "��", "��" }, SetSwitchType.SELECT);
        contorl3.Create<SetItemUI>();
        contorlItems.Add(contorl1);
        contorlItems.Add(contorl2);
        contorlItems.Add(contorl3);

        var level1 = new SetItem("���ر���", new string[] { }, SetSwitchType.NULL);
        level1.Create<BackInitSetUI>();
        var level2 = new SetItem("�뿪��Ϸ", new string[] { }, SetSwitchType.NULL);
        level2.Create<ExitGameSetUI>();
        leaveItems.Add(level1);
        leaveItems.Add(level2);

        SelectTab(0);
        listIndex = 0;
        SelectList(0);
    }


    /// <summary>
    /// Clear:
    /// ���ȫ����Item
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
    /// ����һ���б�
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
    /// ����һ������
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
            // ʹ�� Type.GetType ��ȡ���͵� Type ����
            Type componentType = Type.GetType(type);
            // ��ȡ��������
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
    /// ��ؼ��̰���
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
    /// ѡ���ϲ���Tab
    /// </summary>
    /// <param name="index">�±�</param>
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
    /// ѡ���м䲿�ֵ��б�
    /// </summary>
    /// <param name="index">�±�</param>
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
    /// �ر�״̬�´���
    /// </summary>
    public override void OnClose()
    {
        base.OnClose();
        
    }

    /// <summary>
    /// Callback:
    /// UImanager��ͨ�ô���
    /// </summary>
    internal override void OnDeath()
    {
        base.OnDeath();
        Recycle();
    }

    /// <summary>
    /// Recycle:
    /// ��������
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
    /// Scorll���Ϲ���
    /// </summary>
    void DoScrollUp()
    {
        // ���Ϲ���һ����
        scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition + scrollSpeed);
    }

    /// <summary>
    /// Do:
    /// Scorll���¹���
    /// </summary>
    void DoScrollDown()
    {
        // ���¹���һ����
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
    /// ����һ���������������
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