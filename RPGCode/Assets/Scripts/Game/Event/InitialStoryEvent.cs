using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 初始故事事件
/// 是一个特殊的
/// </summary>
public class InitialStoryEvent :  BaseGameEvent
{
    /// <summary>
    /// Trigger:
    /// 触发事件
    /// </summary>
    /// <param name="data">事件参数</param>
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        UIManager.Instance.OpenUI<StoryUI>(1);   
    }

}
