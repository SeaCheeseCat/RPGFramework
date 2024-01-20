using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceDialogEvent :  BaseGameEvent
{
    //Tip: ReplaceDialogEvent：将其中一个人物的Dialog替换到
    public override void TriggerEvent(EventData data)
    {
        base.TriggerEvent(data);
        DebugEX.Log("触发事件:"+GetType().Name);
        var npc1 = NPCManager.Instance.GetNpc(int.Parse(data.args[0]));
        var npc2 = NPCManager.Instance.GetNpc(int.Parse(data.args[1]));
      /*  var dialogVal = npc1.Dialog.contentText.text;
        ItemManager.Instance.CreateDialogItem(npc2, dialogVal, npc1.Dialog.languageDic);
        ItemManager.Instance.DeleteDialogItem(npc1);
        npc1.Dialog.OnTaskComplete(data.id);*/
        //npc1.Dialog.transform.parent = npc2.transform;
    }
}
