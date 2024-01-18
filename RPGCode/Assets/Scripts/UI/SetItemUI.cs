using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetItemUI : MonoBehaviour
{
    //Tip: �����ı�
    public Text descText;
    //Tip: ѡ��������ı�
    public Text switchText;
    //Tip: ��ť
    public Button button;
    //Tip: ��ఴť
    public Button leftButton;
    //Tip: �Ҳఴť
    public Button rightButton;
    //Tip: ѡ�������ı�
    private string[] switchDescs;
    //Tip: �л����õ�����
    private SetSwitchType setSwitchType;
    //Tip: ѡ���±�
    private int switchIndex = 0;
    //Tip: ���ֵ
    private int maxValue = 0;
    //Tip: ��Сֵ
    private int minValue = 0;
    //Tip: ����
    private int increment = 0;
    //Tip: �л�״̬������
    public GameObject switchObj;
    //Tip: ѡ�������
    public GameObject selectObj;
    //Tip: �Ƿ��Ѿ���ѡ��
    public bool isSelect;
    //Tip: �Ƿ��ڵ���ѡ��״̬
    public bool isAdjust;
    //Tip: �Ƿ�������л���ѡ��
    private bool haveSwitch;

    /// <summary>
    /// Callback:
    /// ���崴��ʱִ��
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
    /// ����������
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
    /// ��ʼ��ѡ��
    /// </summary>
    /// <param name="desc">����</param>
    /// <param name="type">����</param>
    /// <param name="switchargs">��������ΪSelect��������string[] ��ΪValue����int[]{��ʼֵ����Сֵ,���ֵ,����Ч��}</param>
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
    /// ��ѡ��
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
    /// ȡ��ѡ��
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
    /// �����忪ʼ����״̬
    /// </summary>
    public void OnAdjust()
    {
        isAdjust = true;
        OnEnter();
    }

    /// <summary>
    /// Callback:
    /// ��Enter��������
    /// </summary>
    public virtual void OnEnter() 
    { 
    
    
    }

    /// <summary>
    /// Callback:
    /// ������ȡ������״̬
    /// </summary>
    public void OnCancelAdjust() 
    {
        isAdjust = false;
    }

    /// <summary>
    /// Create:
    /// ����һ��SetItem����
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
    /// Callback��
    /// ��ť����¼�
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
    /// ������¼�
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
    /// �Ҳ����¼�
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
    /// ����ֵ�ı䴥��
    /// </summary>
    public virtual void OnVlueChange(string value)
    { 

    }

    /// <summary>
    /// Set:
    /// ���������ı�
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
    /// �����л������ı�
    /// </summary>
    /// <param name="desc">�����ı�</param>
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
    /// �����л������ı�
    /// </summary>
    /// <param name="desc">�����ı�</param>
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
