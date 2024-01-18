using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEvent : BaseGameEvent
{
    //Tip: Interactive事件： 将其中一个物体转移到对应区域
    //Arg：1.Npc的ID  2.该NPC下属的物体名称 3.转移到的NpcID
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        DebugEX.Log("触发事件:InteractiveEvent");
        var npc1 = NPCManager.Instance.GetNpc(int.Parse(data.args[0]));
        var npc2 = NPCManager.Instance.GetNpc(int.Parse(data.args[1]));
        var item = npc1.GetInteractableObject(data.args[2]);
        item.parent = npc2.transform;
        //npc1.Dialog.transform.parent = npc2.transform;

    }

}
