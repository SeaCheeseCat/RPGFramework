using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameBaseData
{
    //Tip: �½�
    public static int Chapter
    {
        get;set;
    }

    //Tip: �ؿ�
    public static int Level {
        get;set;
    }

    //Tip: �ؿ�����
    public static LevelType levelType
    {
        get;set;
    }   

    // Tip: ��Ҫ���ݵ��¼�
    public static Action Event
    {
        get;set;
    }

    // Tip: ��Ҫ���ݵķ���
    public static string EventName
    {
        get; set;
    }

    /// <summary>
    /// Tip:��ǰ������
    /// </summary>
    public static Language language
    {
        get;set;
    }
}

/// <summary>
/// �ؿ�����
/// </summary>
public enum LevelType
{ 
    //Tip: ���õ�
    COMMON,
    //Tip: ������Ļ��
    HAVESUBTITLE
}