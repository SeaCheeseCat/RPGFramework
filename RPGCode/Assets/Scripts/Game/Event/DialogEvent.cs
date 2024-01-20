using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogEvent : BaseGameEvent
{
    //Tip: Dialog事件：将其中一个人物的Dialog转移到另一个人物身上
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        var npc1 = NPCManager.Instance.GetNpc(int.Parse(data.args[0]));
        var npc2 = NPCManager.Instance.GetNpc(int.Parse(data.args[1]));
       /* npc1.Dialog.transform.parent = npc2.transform;
        npc1.Dialog.OnTaskComplete(data.id);*/
    }

}
