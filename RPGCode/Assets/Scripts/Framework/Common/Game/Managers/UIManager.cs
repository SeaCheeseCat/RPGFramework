using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : Singleton<UIManager>
{
    //Tip: �����Canvas
    private Transform parentCanvas;
    //Tip: UI����
    public Dictionary<string, UIBase> uis = new Dictionary<string, UIBase>();
    //Tip: Canvas
    public Transform Canvas;

    /// <summary>
    /// Init:
    /// ��ʼ��UI���:Auto�Զ�����
    /// </summary>
    public void Init() 
    {
        this.parentCanvas = GameObject.Find("Canvas").transform;
    }
    
    /// <summary>
    /// Init:
    /// ��ʼ��
    /// </summary>
    /// <param name="Canvas"></param>
    /// <param name="parentCanvas"></param>
    public void Init(Transform Canvas, Transform parentCanvas)
    {
        this.Canvas = Canvas;
        this.parentCanvas = parentCanvas;
    }
    
    /// <summary>
    /// Open:
    /// ��UI
    /// </summary>
    /// <param name="UiName">UI����</param>
    /// <param name="dialogArgs">����</param>
    public UIBase OpenUI(string UiName, params object[] dialogArgs)
    {
        if (!uis.ContainsKey(UiName))
        {
            GameObject go = LoadUiWithRes(UiName);
            UIBase uibase = go.GetComponent<UIBase>();
            go.transform.SetParent(parentCanvas);
            uis.Add(UiName, uibase);
            uibase.Init(dialogArgs);
            uibase.OpenWithAnimation();
            go.transform.localPosition = new Vector3(0, 0, 0);
            return uibase;
        }
        else 
        {
            var ui = uis[UiName];
            if (ui.type == UiType.INITEXIST)
            {
                GameObject go = LoadUiWithUis(UiName);
                UIBase uibase = go.GetComponent<UIBase>();
                go.transform.SetParent(parentCanvas);
                uis.Add(UiName, uibase);
                uibase.Init(dialogArgs);
                uibase.OpenWithAnimation();
                go.transform.localPosition = new Vector3(0, 0, 0);
                return uibase;
            }
        }

        return null;
    }
    
    /// <summary>
    /// Open:
    /// ��UI
    /// </summary>
    /// <typeparam name="T">UI����</typeparam>
    /// <param name="dialogArgs">UI�Ĳ���</param>
    public T OpenUI<T>(params object[] dialogArgs) where T : UIBase
    {
        string typename = typeof(T).Name;
        var uiBase = OpenUI(typename, dialogArgs);
        if (uiBase != null)
            return uiBase as T;
        return default(T);
    }

    /// <summary>
    /// Register:
    /// ע��UI��ʹ���ڱ�����ڳ����ϴ��ڵ�UI)
    /// </summary>
    public void RegisterUI(UIBase Uibase, params object[] dialogArgs)
    {
        if (!uis.ContainsKey(Uibase.name))
        {
            uis.Add(Uibase.name, Uibase);
            Uibase.Init(dialogArgs);
        }
    }

    /// <summary>
    /// Close:
    /// �ر�UI
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void CloseUI<T>()
    {
        string typename = typeof(T).Name;
        CloseUI(typename);
    }

    /// <summary>
    /// Get:
    /// ��ȡһ��UI
    /// </summary>
    /// <param name="UiName">ui����</param>
    /// <returns>UI</returns>
    public UIBase GetUI(string UiName)
    {
        if (!uis.ContainsKey(UiName))
            return null;
        return uis[UiName];
    }

    /// <summary>
    /// Get:
    /// ��ȡһ��UI
    /// </summary>
    /// <typeparam name="T">UI����</typeparam>
    /// <returns>UI</returns>
    public UIBase GetUI<T>()
    {
        string typename = typeof(T).Name;
        if (!uis.ContainsKey(typename))
            return null;
        return uis[typename];
    }

    /// <summary>
    /// Close:
    /// �ر�һ��UI
    /// </summary>
    /// <param name="UiName"></param>
    public void CloseUI(string UiName)
    {
        if (uis.ContainsKey(UiName))
        {
            var ui = uis[UiName];
            ui.OnDeath();
            ui.Close();
            if (ui.type == UiType.COMMON)
                uis.Remove(UiName);
        }
    }

    /// <summary>
    /// Close:
    /// �ر��Լ����UI
    /// </summary>
    internal virtual void CloseMySelf()
    {
        UIManager.Instance.CloseUI(GetType().Name);
    }


    /// <summary>
    /// Close:
    /// �ر�����UI
    /// </summary>
    public void CloseAllUI()
    {
        foreach (var ui in uis.Values)
        {
            ui.OnDeath();
            PoolManager.Instance.UiUnitPool.Recycle(ui.gameObject);
        }
        uis.Clear();
    }

    /// <summary>
    /// Destroy:
    /// ����ȫ��UI
    /// </summary>
    public void DestroyAllUI()
    {
        foreach (var ui in uis.Keys)
        {
            DestroyUI(ui);
        }
        uis.Clear();
    }

    /// <summary>
    /// Destroy:
    /// ����һ��UI
    /// </summary>
    /// <param name="UiName"></param>
    public void DestroyUI(string UiName)
    {
        if (uis.ContainsKey(UiName))
        {
            var ui = uis[UiName];
            ui.OnDeath();
            ui.gameObject.SetActive(false);
            uis.Remove(UiName);
        }
    }

    /// <summary>
    /// Load:
    /// ����һ��UI��Դ
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private GameObject LoadUiWithRes(string name)
    {
        GameObject obj = PoolManager.Instance.UiUnitPool.Spawn("UI/" + name);
        if (obj == null)
        {
            obj = ResourceManager.LoadPrefabSync("UI/" + name) as GameObject;
            obj.name = name;
        }
        return obj;
    }
    
    /// <summary>
    /// Load:
    /// ����һ��UI��Դ
    /// </summary>
    /// <returns></returns>
    private GameObject LoadUiWithUis(string name)
    {
        uis[name].gameObject.SetActive(true);
        return uis[name].gameObject;
    }

    /// <summary>
    /// Open:
    /// ��һ��ͨ����Ϸ������
    /// </summary>
    /// <param name="title">����</param>
    /// <param name="content">����</param>
    /// <param name="yesAction">ȷ���¼�</param>
    /// <param name="cancelAction">ȡ���¼�</param>
    public void OpenTipUI(string title, string content, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false) 
    {
        var ui = OpenUI<TipBoxUI>();
        ui.Init(title, content, yesAction, cancelAction, hasCloseBtn);
    }

    /// <summary>
    /// Open��
    /// ����Ϸͨ�õ�����
    /// </summary>
    /// <param name="title">����</param>
    /// <param name="content">����</param>
    /// <param name="yesContent">��ť1�ı�</param>
    /// <param name="cancelContent">��ť2�ı�</param>
    /// <param name="yesAction">��ť1�¼�</param>
    /// <param name="cancelAction">��ť2�¼�</param>
    public void OpenTipUI(string title, string content, string yesContent, string cancelContent, Action yesAction, Action cancelAction = null, bool hasCloseBtn = false)
    {
        var ui = OpenUI<TipBoxUI>();
        ui.Init(title, content, yesContent, cancelContent, yesAction, cancelAction, hasCloseBtn);
    }

    /// <summary>
    /// Open:
    /// ����Ϸͨ�õ�����
    /// </summary>
    /// <returns></returns>
    public TipBase OpenTipUI() 
    {
        var ui = OpenUI<TipBoxUI>();
        return ui;
    }

}


