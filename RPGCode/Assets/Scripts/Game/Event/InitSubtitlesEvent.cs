using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 初始故事事件
/// 是一个特殊的
/// </summary>
public class InitSubtitlesEvent :  BaseGameEvent
{
    /// <summary>
    /// Trigger:
    /// 触发事件
    /// </summary>
    /// <param name="data">事件参数</param>
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        GameBase.Instance.StartCoroutine(DoInitSubtitlesEvent());
    }

    IEnumerator DoInitSubtitlesEvent() 
    {
        yield return new WaitForSeconds(1);
        UIManager.Instance.OpenUI<MonologueUI>(2);
    }

}
