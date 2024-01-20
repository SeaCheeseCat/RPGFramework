using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleMono<GameManager>
{
    //Tip:焦点值
    public float fovValue;
    //Tip:焦点值 上次指向的Npc
    [HideInInspector]
    public NpcBase lastNpc;
    //Tip:焦点值 当前指向的Npc
    [HideInInspector]
    public NpcBase currentNpc;
    //Tip:当前状态是否已经打开取景器
    [HideInInspector]
    public bool cameraModeling = false;
    //Tip:当前状态是否在旋转镜头
    [HideInInspector]
    public bool rotaing = false;  
    //Tip:焦点状态颜色组
    public Color[] lockColors;  
    //Tip:当前的镜头旋转X值
    public float cameraRotateX;
    //Tip:当前的镜头旋转Y值
    public float cameraRotateY;
    //Tip:跟随镜头旋转的物体
    public List<Transform> faceFollowTrs = new List<Transform>();
    //Tip: 玩家
    public Player player;

    public NpcBase FocusNpc
    {
        get { return currentNpc; }
    }
    
    //Tip: 打开照片状态  -禁用旋转   -禁用选择  -鼠标恢复
    public bool OpenPhoto 
    {
        get;set;
    }

    /// <summary>
    /// Base:Awake
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    /// Load：
    /// 载入游戏数据
    /// </summary>
    public void LoadLevelData() 
    {
        var data = GameBase.Instance.currentLevelData;
        LoadWithTask(data);

    }

    /// <summary>
    /// Load:
    /// 载入游戏任务数据
    /// </summary>
    public void LoadWithTask(LevelData data) 
    {
        if (data == null)
            return;
        foreach (var item in data.completedTask)
        {
            var task = TaskManager.Instance.GetTask(item);
            var taskName = task.Event;
            if (task.Event == "DialogEvent")
            {
                taskName = "ReplaceDialogEvent";
            }
            EventManager.Instance.TriggerEvent(item, taskName, task.Args);
        }
        TaskManager.Instance.TaskCompletes = data.completedTask;
        MsgManager.Instance.SendMessage(MSGInfo.REFRESHINFO, TaskManager.Instance.GetTaskCompleteCount(), TaskManager.Instance.GetTaskCount());
    }

    /// <summary>
    /// Add:
    /// 添加镜头跟随旋转的物体、Npc
    /// </summary>
    /// <param name="tr"></param>
    public void AddFacingCamera(Transform tr) 
    {
        faceFollowTrs.Add(tr);
    }

    /// <summary>
    /// Clear:
    /// 清除跟随物体
    /// </summary>
    public void ClearFaceCameras() 
    {
        faceFollowTrs.Clear();
    }

    /// <summary>
    /// Callback：
    /// 相机开始旋转
    /// </summary>
    public void OnCameraRotateStart()
    { 

    }

    /// <summary>
    /// Callback:
    /// 相机停止旋转
    /// </summary>
    public void OnCameraRotateStop() 
    {
        OnCameraRotateStop(cameraRotateX,cameraRotateY);
    }

    /// <summary>
    /// Callback:
    /// 相机停止旋转
    /// </summary>
    public void OnCameraRotateStop(float x, float y) 
    {
        var focusid = 0;
        if (FocusNpc != null)
            focusid = FocusNpc.ID;
        var task = TaskManager.Instance.CheckTask(focusid, x,y,20);   //获取任务完成
        if (task != null)
        {
            EventManager.Instance.TriggerEvent(task.ID, task.Event, task.Args);
            TaskManager.Instance.OnCompleteTask(task.TaskID);
            var complete = TaskManager.Instance.CheckLevelComplete();   //检查关卡是否完成了
            if (complete)
            {
                GameBase.Instance.currentLevelData.completed = true;
                DebugEX.Log("关卡已经完成了");
            }
        }
        else
        {
            DebugEX.Log("此处没有寻找到任务");
        }
    }

    /// <summary>
    /// Base:Update
    /// </summary>
    private void Update()
    {
        OnFaceRotate();
        if(GameBase.Instance.gameScene == GameScene.GameLevel)
            OnKeyInput();
        if (cameraModeling)
            OnMouseTrigger();
    }
    
    /// <summary>
    /// Callback:
    /// 进入关卡
    /// </summary>
    public void OnEntryLevel()
    {

    }

    /// <summary>
    /// Callback:
    /// 按键按下
    /// </summary>
    public void OnKeyInput() 
    {
      /*  if (Input.GetKeyDown(KeyCode.E))
           CameraUIControls.Instance.OpenCameraModle();
        else if (Input.GetKeyDown(KeyCode.R))
           CameraUIControls.Instance.CloseCameraModle();*/
    }

    /// <summary>
    /// Callback:
    /// 更新人物跟随镜头
    /// </summary>
    public void OnFaceRotate() 
    {
        for (int i = 0; i < faceFollowTrs.Count; i++)
        {
            faceFollowTrs[i].rotation = Camera.main.transform.rotation;
        }
    }

    /// <summary>
    /// Callback:
    /// 相机模式
    /// </summary>
    public void OnMouseTrigger() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag != "NPC")
            {
              /*  if (currentNpc != null)
                    currentNpc.ModelDoColor(Color.white);*/
                lastNpc = null;
                currentNpc = null;
                return;
            }
            currentNpc = hitObject.GetComponent<NpcBase>();
            if (lastNpc != null && lastNpc == currentNpc)
                return;
            TriggerNPC(hitObject);
        }
        else
        {
            if (currentNpc != null)
            {
               /* currentNpc.ModelDoColor(Color.white);
                currentNpc = null;*/
            }
            if (lastNpc != null)
            {
               /* lastNpc.ModelDoColor(Color.white);
                lastNpc = null;*/
            }
            ResetNpc();
        }
    }


    /// <summary>
    /// Trigger:
    /// 触发标记一个Npc
    /// </summary>
    /// <param name="obj">标记物</param>
    public void TriggerNPC(GameObject obj) 
    {
        if (lastNpc != null)
        {
            //lastNpc.ModelDoColor(Color.white);
        }
        var npc = obj.GetComponent<NpcBase>();
        NPCManager.Instance.SignNpc(npc);
        lastNpc = currentNpc;
    }
    
    /// <summary>
    /// Reset:
    /// 重置所有Npc
    /// </summary>
    public void ResetNpc() 
    {
        if (currentNpc != null)
        {
           /* currentNpc.ModelDoColor(Color.white);
            currentNpc = null;*/
        }
        if (lastNpc != null)
        {
            /*lastNpc.ModelDoColor(Color.white);
            lastNpc = null;*/
        }
    }

    /// <summary>
    /// Recycle：
    /// 回收数据
    /// </summary>
    public void Recycle() 
    {
        ClearFaceCameras();
        EventManager.Instance.Recycle();
        NPCManager.Instance.Recycle();
        GameBase.Instance.currentLevelData = new LevelData();
        TaskManager.Instance.Recycle();
        if(ItemManager.Instance != null)
            ItemManager.Instance.Recycle();
    }
}
