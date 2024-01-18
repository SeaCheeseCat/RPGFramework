using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetItemUI : MonoBehaviour
{
    //Tip: 描述文本
    public Text descText;
    //Tip: 选择的描述文本
    public Text switchText;
    //Tip: 按钮
    public Button button;
    //Tip: 左侧按钮
    public Button leftButton;
    //Tip: 右侧按钮
    public Button rightButton;
    //Tip: 选择描述文本
    private string[] switchDescs;
    //Tip: 切换设置的类型
    private SetSwitchType setSwitchType;
    //Tip: 选择下标
    private int switchIndex = 0;
    //Tip: 最大值
    private int maxValue = 0;
    //Tip: 最小值
    private int minValue = 0;
    //Tip: 增量
    private int increment = 0;
    //Tip: 切换状态的物体
    public GameObject switchObj;
    //Tip: 选择的物体
    public GameObject selectObj;
    //Tip: 是否已经被选中
    public bool isSelect;
    //Tip: 是否在调整选项状态
    public bool isAdjust;
    //Tip: 是否包含可切换的选择
    private bool haveSwitch;

    /// <summary>
    /// Callback:
    /// 物体创建时执行
    /// </summary>
    public void OnCreate() 
    {
        descText = transform.Find("Desc").GetComponent<Text>();
        switchText = RecursivelyFindChildren(transform,"SwitchText").GetComponent<Text>();
        button = transform.Find("Button").GetComponent<Button>();
        leftButton = RecursivelyFindChildren(transform, "LeftButton").GetComponent<Button>();
        rightButton = RecursivelyFindChildren(transform, "RightButton").GetComponent<Button>();
        switchObj = transform.Find("Switch").gameObject;
        selectObj = transform.Find("Select").gameObject;
    }

    /// <summary>
    /// Recursively:
    /// 查找子物体
    /// </summary>
    /// <param name="parentTransform"></param>
    /// <param name="targetName"></param>
    /// <returns></returns>
    Transform RecursivelyFindChildren(Transform parentTransform, string targetName)
    {
        Transform child = parentTransform.Find(targetName);
        if (child != null)
        {
            return child;
        }
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform currentChild = parentTransform.GetChild(i);
            Transform result = RecursivelyFindChildren(currentChild, targetName);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    /// <summary>
    /// Init:
    /// 初始化选择
    /// </summary>
    /// <param name="desc">描述</param>
    /// <param name="type">类型</param>
    /// <param name="switchargs">参数：若为Select类型则传入string[] 若为Value则传入int[]{初始值，最小值,最大值,增减效益}</param>
    public void Init(string desc, SetSwitchType type, string[] switchargs)
    {
        descText.text = desc;
        if (type == global::SetSwitchType.SELECT)
        {
            switchDescs = (string[])switchargs;
            switchText.text = switchDescs[0];
            switchObj.gameObject.SetActive(true);
            haveSwitch = true;
        }
        else if (type == global::SetSwitchType.VALUE)
        {
            switchText.text = switchargs[0];
            switchIndex = int.Parse(switchargs[0]);
            minValue = int.Parse(switchargs[1]);
            maxValue = int.Parse(switchargs[2]);
            increment = int.Parse(switchargs[3]);
            switchText.text = switchDescs[0];
            switchObj.gameObject.SetActive(true);
            haveSwitch = true;
        }
        else if (type == global::SetSwitchType.NULL)
        {
            switchObj.gameObject.SetActive(false);
            haveSwitch = false;
        }
        this.setSwitchType = type;
        button.onClick.RemoveAllListeners();
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { ButtonOnclick(); });
        leftButton.onClick.AddListener(() => { LeftOnclick(); });
        rightButton.onClick.AddListener(() => { RightOnclick(); });
        switchIndex = 0;
    }

    /// <summary>
    /// Callback:
    /// 被选中
    /// </summary>
    public void OnSelected()
    {
        selectObj.gameObject.SetActive(true);
        isSelect = true;
        if (haveSwitch)
        {
            leftButton.gameObject.transform.parent.gameObject.SetActive(true);
            rightButton.transform.parent.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Callback:
    /// 取消选中
    /// </summary>
    public void OnCancelSelect()
    {
        selectObj.gameObject.SetActive(false);
        isSelect = false;
        if (haveSwitch)
        {
            leftButton.gameObject.transform.parent.gameObject.SetActive(false);
            rightButton.transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Callback:
    /// 当物体开始调节状态
    /// </summary>
    public void OnAdjust()
    {
        isAdjust = true;
        OnEnter();
    }

    /// <summary>
    /// Callback:
    /// 当Enter按键按下
    /// </summary>
    public virtual void OnEnter() 
    { 
    
    
    }

    /// <summary>
    /// Callback:
    /// 当物体取消调节状态
    /// </summary>
    public void OnCancelAdjust() 
    {
        isAdjust = false;
    }

    /// <summary>
    /// Create:
    /// 创造一个SetItem物体
    /// </summary>
    /// <returns></returns>
    public SetItemUI Create() 
    {
        button.onClick.RemoveAllListeners();
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => { ButtonOnclick(); });
        leftButton.onClick.AddListener(() => { LeftOnclick(); });
        rightButton.onClick.AddListener(() => { RightOnclick(); });
        switchIndex = 0;
        return this;
    }



    /// <summary>
    /// Callback：
    /// 按钮点击事件
    /// </summary>
    public void ButtonOnclick() 
    { 
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isAdjust)
                return;
            LeftOnclick();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (!isAdjust)
                return;
            RightOnclick();
        }

    }


    /// <summary>
    /// Callback:
    /// 左侧点击事件
    /// </summary>
    public void LeftOnclick() 
    {
        var desc = "";
        if (setSwitchType == global::SetSwitchType.SELECT)
        {
            if (switchIndex <= 0 || !isSelect)
                return;
            switchIndex--;
            desc = switchDescs[switchIndex];
            switchText.text = desc;
        }
        else if (setSwitchType == global::SetSwitchType.VALUE)
        {
            if (switchIndex > minValue || !isSelect)
            {
                switchIndex--;
                return;
            }
            desc = switchIndex+"";
            switchText.text = desc;
        }

        OnVlueChange(desc);
    }

    /// <summary>
    /// Callback:
    /// 右侧点击事件
    /// </summary>
    public void RightOnclick() 
    {
        var desc = "";
        if (setSwitchType == global::SetSwitchType.SELECT)
        {
            if (switchIndex >= switchDescs.Length-1 || !isSelect)
                return;
            switchIndex++;
            desc = switchDescs[switchIndex];
            switchText.text = desc;
        }
        else if (setSwitchType == global::SetSwitchType.VALUE)
        {
            if (switchIndex < maxValue || !isSelect)
            {
                switchIndex++;
                return;
            }
            desc = switchIndex + "";
            switchText.text = desc;
        }
        OnVlueChange(desc);
    }

    /// <summary>
    /// Callback:
    /// 当数值改变触发
    /// </summary>
    public virtual void OnVlueChange(string value)
    { 

    }

    /// <summary>
    /// Set:
    /// 设置描述文本
    /// </summary>
    /// <param name="desc"></param>
    /// <returns></returns>
    public SetItemUI SetDescText(string desc) 
    {
        descText.text = desc;
        return this;
    }

    /// <summary>
    /// Set:
    /// 设置切换描述文本
    /// </summary>
    /// <param name="desc">描述文本</param>
    /// <returns></returns>
    public SetItemUI SetSwitchDesc(SetSwitchType type, string[] switchargs)
    {
        if (type == global::SetSwitchType.SELECT)
        {
            switchDescs = (string[])switchargs;
            switchText.text = switchDescs[0];
        }
        else if (type == global::SetSwitchType.VALUE)
        {
            switchText.text = switchargs[0];
            minValue = int.Parse(switchargs[1]);
            maxValue = int.Parse(switchargs[2]);
            increment = int.Parse(switchargs[3]);
        }
        return this;
    }



    /// <summary>
    /// Set:
    /// 设置切换描述文本
    /// </summary>
    /// <param name="desc">描述文本</param>
    /// <returns></returns>
    public SetItemUI SetSwitchType(SetSwitchType type)
    {
        setSwitchType = type;
        return this;
    }

}

public enum SetSwitchType
{
    NULL,
    SELECT,
    VALUE
}
