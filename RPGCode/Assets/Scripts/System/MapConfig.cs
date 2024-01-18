using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapConfig : Manager<MapConfig>
{
    // Tip: NPC��Transform
    public Transform npcsTrans;

    // Tip: ģ�͵�Transform
    public Transform modelsTrans;

    // Tip: ����Ч����Transform
    public Transform particleTrans;

    //Tip: ��������
    public Transform landTran;

    // Tip: NPC���ַ�����ʶ
    private const string npcTr = "Npc";

    // Tip: ����ģ�͵��ַ�����ʶ
    private const string sceneTr = "SceneModel";

    // Tip: ����Ч�����ַ�����ʶ
    private const string particleTr = "Particle";

    //Tip: �����λ��
    private const string landTrName = "Land";

    // Tip: ��ͼ��������
    private MapConfigData configData;
    
    /// <summary>
    /// Init:
    /// ��ʼ������
    /// </summary>
    public void InitData()
    {
        npcsTrans = GameObject.Find(npcTr)?.transform;
        modelsTrans = GameObject.Find(sceneTr)?.transform;
        particleTrans = GameObject.Find(particleTr)?.transform;
        landTran = GameObject.Find(landTrName)?.transform;
    }

    /// <summary>
    /// Load:
    /// ����ؿ�����
    /// </summary>
    /// <param name="Chapter">�½�</param>
    /// <param name="Level">�ؿ�</param>
    /// <param name="isedit">�Ƿ��Ǳ༭��ģʽ</param>
    public void LoadMapConfig(int Chapter, int Level, bool isedit) {
        Instance.InitData();
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("��ȡ�ؿ���������ʧ��");
            return;
        }
        LoadMapConfig(data, isedit);
    }

    /// <summary>
    /// Save:
    /// ����ؿ���������
    /// </summary>
    public MapConfigData SaveMapConfig()
    {
        configData = new MapConfigData();
        configData.mapnpcdata = new List<MapNpcData>();
        configData.mapmodeldatas = new List<MapModelData>();
        configData.mapparticledatas = new List<MapParticleData>();
        configData.maplanddata = new MapLandData();
        SaveNpcData();
        SaveSenceModelData();
        SavePartitleData();
        SaveLandData();
        return configData;
    }
    
    /// <summary>
    /// Save:
    /// ���� Npc ����
    /// </summary>
    private void SaveNpcData()
    {
        var data = Getmapnpcdatas();
        if (data == null)
            return;
        configData.mapnpcdata = data;
    }

    /// <summary>
    /// Get:
    /// ��ȡ��ͼNpc����
    /// </summary>
    /// <returns></returns>
    public List<MapNpcData> Getmapnpcdatas() 
    {
        var datas = new List<MapNpcData>();
        if (npcsTrans == null)
        {
            Debug.LogError("Npc Transform not found.");
            return null;
        }
        for (int i = 0; i < npcsTrans.childCount; i++)
        {
            var item = npcsTrans.GetChild(i);
            var npc = item.GetComponent<NpcBase>();
            var data = new MapNpcData();
            data.x = item.localPosition.x;
            data.y = item.localPosition.y;
            data.z = item.localPosition.z;
            data.scalex = item.localScale.x;
            data.scaley = item.localScale.y;
            data.scalez = item.localScale.z;
            data.rotatex = item.localEulerAngles.x;
            data.rotatey = item.localEulerAngles.y;
            data.rotatez = item.localEulerAngles.z;
            data.ID = npc?.ID ?? 0; 
            datas.Add(data);
        }
        return datas;
    }

    /// <summary>
    /// Save:
    /// ���泡������
    /// </summary>
    private void SaveSenceModelData() 
    {
        var data = Getmapmodelsdatas();
        if (data == null)
            return;
        configData.mapmodeldatas = data;
    }
    
    /// <summary>
    /// Get:
    /// ��ȡ��ͼNpc����
    /// </summary>
    /// <returns>ȫ���ĵ�ͼ����</returns>
    public List<MapModelData> Getmapmodelsdatas()
    {
        var datas = new List<MapModelData>();
        if (modelsTrans == null)
        {
            Debug.LogError("Npc Transform not found.");
            return null;
        }
        for (int i = 0; i < modelsTrans.childCount; i++)
        {
            var item = modelsTrans.GetChild(i);
            var npc = item.GetComponent<ModelBase>();
            var data = new MapModelData();
            data.x = item.localPosition.x;
            data.y = item.localPosition.y;
            data.z = item.localPosition.z;
            data.scalex = item.localScale.x;
            data.scaley = item.localScale.y;
            data.scalez = item.localScale.z;
            data.rotatex = item.localEulerAngles.x;
            data.rotatey = item.localEulerAngles.y;
            data.rotatez = item.localEulerAngles.z;
            data.ID = npc?.ID ?? 0; // Handle the case where Npc component is missing
            datas.Add(data);
        }
        return datas;
    }


    /// <summary>
    /// Save:
    /// ���泡����������
    /// </summary>
    private void SavePartitleData()
    {
        var data = Getmapparticledatas();
        if (data == null)
            return;
        configData.mapparticledatas = data;
    }

    /// <summary>
    /// Save:
    /// ���泡����������
    /// </summary>
    private void SaveLandData()
    {
        var data = Getmaplanddata();
        if (data == null)
            return;
        configData.maplanddata = data;
    }

    /// <summary>
    /// Get:
    /// ��ȡ������������
    /// </summary>
    /// <returns></returns>
    public List<MapParticleData> Getmapparticledatas()
    {
        var datas = new List<MapParticleData>();
        if (particleTrans == null)
        {
            Debug.LogError("Npc Transform not found.");
            return null;
        }
        for (int i = 0; i < particleTrans.childCount; i++)
        {
            var item = particleTrans.GetChild(i);
            var npc = item.GetComponent<ParticleBase>();
            var data = new MapParticleData();
            data.x = item.localPosition.x;
            data.y = item.localPosition.y;
            data.z = item.localPosition.z;
            data.scalex = item.localScale.x;
            data.scaley = item.localScale.y;
            data.scalez = item.localScale.z;
            data.rotatex = item.localEulerAngles.x;
            data.rotatey = item.localEulerAngles.y;
            data.rotatez = item.localEulerAngles.z;
            data.ID = npc?.ID ?? 0; 
            datas.Add(data);
        }
        return datas;
    }


    /// <summary>
    /// Get:
    /// ��ȡ������������
    /// </summary>
    /// <returns></returns>
    public MapLandData Getmaplanddata()
    {
        var data = new MapLandData();
        if (landTran == null)
        {
            Debug.LogError("Land Transform not found.");
            return null;
        }
        data.x = landTran.localPosition.x;
        data.y = landTran.localPosition.y;
        data.z = landTran.localPosition.z;
        data.scalex = landTran.localScale.x;
        data.scaley = landTran.localScale.y;
        data.scalez = landTran.localScale.z;
        data.rotatex = landTran.localEulerAngles.x;
        data.rotatey = landTran.localEulerAngles.y;
        data.rotatez = landTran.localEulerAngles.z;
        return data;
    }

    /// <summary>
    /// Load:
    /// �����ͼ��������
    /// </summary>
    /// <param name="data">����</param>
    /// <param name="isedit">�Ƿ��Ǳ༭��ģʽ</param>
    public void LoadMapConfig(MapConfigData data, bool isedit)
    {
        if (data.mapnpcdata != null)
        {
            foreach (var item in data.mapnpcdata)
            {
                var npc = CreateNpc(item.ID, new Vector3((float)item.x, (float)item.y, (float)item.z), new Vector3((float)item.scalex, (float)item.scaley, (float)item.scalez), new Vector3((float)item.rotatex, (float)item.rotatey, (float)item.rotatez), isedit);
                if(!isedit)
                    npc.OnCreate();
                npc.ID = item.ID;
            }
        }
        if (data.mapmodeldatas != null)
        {
            foreach (var item in data.mapmodeldatas)
            {
                var model = CreateSceneModel(item.ID, new Vector3((float)item.x, (float)item.y, (float)item.z), new Vector3((float)item.scalex, (float)item.scaley, (float)item.scalez), new Vector3((float)item.rotatex, (float)item.rotatey, (float)item.rotatez), isedit);
                model.ID = item.ID;
            }
        }
        if (data.mapparticledatas != null)
        {
            foreach (var item in data.mapparticledatas)
            {
                var particle = CreateParticle(item.ID, new Vector3((float)item.x, (float)item.y, (float)item.z), new Vector3((float)item.scalex, (float)item.scaley, (float)item.scalez), new Vector3((float)item.rotatex, (float)item.rotatey, (float)item.rotatez), isedit);
                particle.ID = item.ID;
            }
        }
        if (data.maplanddata != null)
        {
            var landdata = data.maplanddata;
            landTran.localPosition = new Vector3((float)landdata.x, (float)landdata.y, (float)landdata.z);
            landTran.localScale = new Vector3((float)landdata.scalex, (float)landdata.scaley, (float)landdata.scalez);
            landTran.localRotation = Quaternion.Euler((float)landdata.rotatex, (float)landdata.rotatey, (float)landdata.rotatez);
        }
        GameBaseData.Chapter = data.Charpter;
        GameBaseData.Level = data.Level;
        if (!isedit)
            AudioManager.Instance.PlayMusic(data.aduioPath, true);
    }

    /// <summary>
    /// Create:
    /// ����Npc
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">λ��</param>
    /// <param name="isedit">�Ƿ��Ǳ༭��ģʽ</param>
    /// <returns></returns>
    public NpcBase CreateNpc(int id, Vector3 pos, Vector3 scale, Vector3 rotate, bool isedit)
    {
        var config = ConfigManager.GetConfigByID<NpcCfg>(id);
        GameObject model = null;
        if (isedit)
             model = EditCreateModel("Prefabs/" + config.Path, pos, scale, rotate,  npcsTrans);
        else
            model = CreateModel("Prefabs/" + config.Path, pos, scale, rotate, npcsTrans);
        return model.GetComponent<NpcBase>();
    }

    /// <summary>
    /// Create:
    /// ��������ģ��
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">λ��</param>
    /// <param name="isedit">�Ƿ��Ǳ༭��ģʽ</param>
    /// <returns></returns>
    public ModelBase CreateSceneModel(int id, Vector3 pos, Vector3 scale, Vector3 rotate, bool isedit)
    {
        var config = ConfigManager.GetConfigByID<ModelCfg>(id);
        GameObject model = null;
        if (isedit)
            model = EditCreateModel("Prefabs/" + config.Path, pos, scale, rotate,modelsTrans);
        else
            model = CreateModel("Prefabs/" + config.Path, pos, scale, rotate,modelsTrans);
        return model.GetComponent<ModelBase>();
    }

    /// <summary>
    /// Create:
    /// ��������
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">λ��</param>
    /// <param name="isedit">�Ƿ��Ǳ༭��ģʽ</param>
    /// <returns></returns>
    public ParticleBase CreateParticle(int id, Vector3 pos, Vector3 scale, Vector3 rotate, bool isedit)
    {
        var config = ConfigManager.GetConfigByID<ParticleCfg>(id);
        GameObject model = null;
        if (isedit)
            model = EditCreateModel("Prefabs/" + config.Path, pos, scale, rotate, particleTrans);
        else
            model = CreateModel("Prefabs/" + config.Path, pos, scale, rotate, particleTrans);
        return model.GetComponent<ParticleBase>();
    }
    
    /// <summary>
    /// Create: Edit
    /// �༭��״̬�´���ģ��
    /// </summary>
    /// <param name="path">·��</param>
    /// <param name="pos">λ��</param>
    /// <param name="parent">���ڵ�</param>
    /// <returns></returns>
    public GameObject EditCreateModel(string path, Vector3 pos, Vector3 scanle, Vector3 rotate, Transform parent)
    {
        var npc = EditCreateModel(path, parent);
        if (npc == null)
        {
            Debug.LogError("Npc creation failed. Path not found: " + path);
            return null;
        }
        npc.transform.localPosition = pos;
        npc.transform.localScale = scanle;
        npc.transform.localRotation = Quaternion.Euler(rotate.x, rotate.y, rotate.z);
        return npc;
    }

    /// <summary>
    /// Create:
    /// ����һ��ģ��
    /// </summary>
    /// <param name="path">·��</param>
    /// <param name="pos">λ��</param>
    /// <param name="parent">���ڵ�</param>
    /// <returns></returns>
    public GameObject CreateModel(string path, Vector3 pos, Vector3 scanle, Vector3 rotate,Transform parent)
    {
        var npc = CreateModel(path, parent);
        if (npc == null)
        {
            Debug.LogError("Npc creation failed. Path not found: " + path);
            return null;
        }
        npc.transform.localPosition = pos;
        npc.transform.localScale = scanle;
        npc.transform.localRotation = Quaternion.Euler(rotate.x, rotate.y, rotate.z);
        return npc;
    }

    /// <summary>
    /// Create:Edit
    /// </summary>
    /// <param name="path">·��</param>
    /// <param name="parent">���ڵ�</param>
    /// <returns></returns>
    public GameObject EditCreateModel(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab != null)
        {
            GameObject instantiatedPrefab = Object.Instantiate(prefab, parent.transform);
            UnityEditor.EditorUtility.SetDirty(instantiatedPrefab);
        }
        return prefab;
    }

    /// <summary>
    /// Create:
    /// ����һ��ģ��
    /// </summary>
    /// <param name="path">·��</param>
    /// <param name="parent">���ڵ�</param>
    /// <returns></returns>
    public GameObject CreateModel(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);
        if (prefab != null)
        {
            GameObject instantiatedPrefab = GameObject.Instantiate(prefab, parent.transform);
            UnityEditor.EditorUtility.SetDirty(instantiatedPrefab);
            return instantiatedPrefab;
        }
        return null;
    }
}
