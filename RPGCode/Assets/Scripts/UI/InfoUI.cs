using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InfoUI : MonoBehaviour
{
    public Image IconImg;
    public TextMeshProUGUI ContentText;

    public void Start()
    {
        MsgManager.Instance.AddObserver(MSGInfo.REFRESHINFO,RefreshInfo);    
    }


    /// <summary>
    /// 刷新关卡数据
    /// </summary>
    /// <param name="data"></param>
    public void RefreshInfo(MsgData data) 
    {
        ContentText.text = data.arg1 + "/" + data.arg2;
    }



}
