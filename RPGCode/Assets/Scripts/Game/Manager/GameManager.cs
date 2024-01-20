using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleMono<GameManager>
{
    //Tip:����ֵ
    public float fovValue;
    //Tip:����ֵ �ϴ�ָ���Npc
    [HideInInspector]
    public NpcBase lastNpc;
    //Tip:����ֵ ��ǰָ���Npc
    [HideInInspector]
    public NpcBase currentNpc;
    //Tip:��ǰ״̬�Ƿ��Ѿ���ȡ����
    [HideInInspector]
    public bool cameraModeling = false;
    //Tip:��ǰ״̬�Ƿ�����ת��ͷ
    [HideInInspector]
    public bool rotaing = false;  
    //Tip:����״̬��ɫ��
    public Color[] lockColors;  
    //Tip:��ǰ�ľ�ͷ��תXֵ
    public float cameraRotateX;
    //Tip:��ǰ�ľ�ͷ��תYֵ
    public float cameraRotateY;
    //Tip:���澵ͷ��ת������
    public List<Transform> faceFollowTrs = new List<Transform>();
    //Tip: ���
    public Player player;

    public NpcBase FocusNpc
    {
        get { return currentNpc; }
    }
    
    //Tip: ����Ƭ״̬  -������ת   -����ѡ��  -���ָ�
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
    /// Load��
    /// ������Ϸ����
    /// </summary>
    public void LoadLevelData() 
    {
        var data = GameBase.Instance.currentLevelData;
        LoadWithTask(data);

    }

    /// <summary>
    /// Load:
    /// ������Ϸ��������
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
    /// ��Ӿ�ͷ������ת�����塢Npc
    /// </summary>
    /// <param name="tr"></param>
    public void AddFacingCamera(Transform tr) 
    {
        faceFollowTrs.Add(tr);
    }

    /// <summary>
    /// Clear:
    /// �����������
    /// </summary>
    public void ClearFaceCameras() 
    {
        faceFollowTrs.Clear();
    }

    /// <summary>
    /// Callback��
    /// �����ʼ��ת
    /// </summary>
    public void OnCameraRotateStart()
    { 

    }

    /// <summary>
    /// Callback:
    /// ���ֹͣ��ת
    /// </summary>
    public void OnCameraRotateStop() 
    {
        OnCameraRotateStop(cameraRotateX,cameraRotateY);
    }

    /// <summary>
    /// Callback:
    /// ���ֹͣ��ת
    /// </summary>
    public void OnCameraRotateStop(float x, float y) 
    {
        var focusid = 0;
        if (FocusNpc != null)
            focusid = FocusNpc.ID;
        var task = TaskManager.Instance.CheckTask(focusid, x,y,20);   //��ȡ�������
        if (task != null)
        {
            EventManager.Instance.TriggerEvent(task.ID, task.Event, task.Args);
            TaskManager.Instance.OnCompleteTask(task.TaskID);
            var complete = TaskManager.Instance.CheckLevelComplete();   //���ؿ��Ƿ������
            if (complete)
            {
                GameBase.Instance.currentLevelData.completed = true;
                DebugEX.Log("�ؿ��Ѿ������");
            }
        }
        else
        {
            DebugEX.Log("�˴�û��Ѱ�ҵ�����");
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
    /// ����ؿ�
    /// </summary>
    public void OnEntryLevel()
    {

    }

    /// <summary>
    /// Callback:
    /// ��������
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
    /// ����������澵ͷ
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
    /// ���ģʽ
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
    /// �������һ��Npc
    /// </summary>
    /// <param name="obj">�����</param>
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
    /// ��������Npc
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
    /// Recycle��
    /// ��������
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
