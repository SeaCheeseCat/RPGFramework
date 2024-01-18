using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapConfig : Manager<MapConfig>
{
    // Tip: NPC的Transform
    public Transform npcsTrans;

    // Tip: 模型的Transform
    public Transform modelsTrans;

    // Tip: 粒子效果的Transform
    public Transform particleTrans;

    //Tip: 地面坐标
    public Transform landTran;

    // Tip: NPC的字符串标识
    private const string npcTr = "Npc";

    // Tip: 场景模型的字符串标识
    private const string sceneTr = "SceneModel";

    // Tip: 粒子效果的字符串标识
    private const string particleTr = "Particle";

    //Tip: 地面的位置
    private const string landTrName = "Land";

    // Tip: 地图配置数据
    private MapConfigData configData;
    
    /// <summary>
    /// Init:
    /// 初始化数据
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
    /// 载入关卡数据
    /// </summary>
    /// <param name="Chapter">章节</param>
    /// <param name="Level">关卡</param>
    /// <param name="isedit">是否是编辑器模式</param>
    public void LoadMapConfig(int Chapter, int Level, bool isedit) {
        Instance.InitData();
        var data = ArchiveManager.Instance.LoadMapConfigFromJson<MapConfigData>(Chapter, Level);
        if (data == null)
        {
            DebugEX.LogError("读取关卡配置数据失败");
            return;
        }
        LoadMapConfig(data, isedit);
    }

    /// <summary>
    /// Save:
    /// 保存关卡配置数据
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
    /// 保存 Npc 数据
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
    /// 获取地图Npc数据
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
    /// 保存场景数据
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
    /// 获取地图Npc数据
    /// </summary>
    /// <returns>全部的地图数据</returns>
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
    /// 保存场景粒子数据
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
    /// 保存场景地面数据
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
    /// 获取场景粒子数据
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
    /// 获取场景地面数据
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
    /// 载入地图配置数据
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="isedit">是否是编辑器模式</param>
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
    /// 创建Npc
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">位置</param>
    /// <param name="isedit">是否是编辑器模式</param>
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
    /// 创建场景模型
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">位置</param>
    /// <param name="isedit">是否是编辑器模式</param>
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
    /// 创建粒子
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="pos">位置</param>
    /// <param name="isedit">是否是编辑器模式</param>
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
    /// 编辑器状态下创建模型
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="pos">位置</param>
    /// <param name="parent">父节点</param>
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
    /// 创建一个模型
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="pos">位置</param>
    /// <param name="parent">父节点</param>
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
    /// <param name="path">路径</param>
    /// <param name="parent">父节点</param>
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
    /// 创建一个模型
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="parent">父节点</param>
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
