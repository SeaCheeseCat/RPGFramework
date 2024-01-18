using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveEvent : BaseGameEvent
{
    //Tip: Interactive�¼��� ������һ������ת�Ƶ���Ӧ����
    //Arg��1.Npc��ID  2.��NPC�������������� 3.ת�Ƶ���NpcID
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        DebugEX.Log("�����¼�:InteractiveEvent");
        var npc1 = NPCManager.Instance.GetNpc(int.Parse(data.args[0]));
        var npc2 = NPCManager.Instance.GetNpc(int.Parse(data.args[1]));
        var item = npc1.GetInteractableObject(data.args[2]);
        item.parent = npc2.transform;
        //npc1.Dialog.transform.parent = npc2.transform;

    }

}
