using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArchiveManager : Manager<ArchiveManager>
{
    //-------------- 存档系统 -------------- 
    //一部分是MapConfig要求的存档，只要是一些储存配置数据使用
    //另一部分是游戏存档（音频数据为了方便直接存到了注册表中PlayPefer）

    //Tip:特殊数据应该保存到游戏里面  而不是外部储存  (失败了！！Resource是只读的  不能写入  阴暗地改回去)
    private const string mapconfigPath = "Assets/Resources/Config/Map/";  //基础路径    
    //Tip: 多语言配置储存路径
    private const string languageconfigPath = "Assets/Resources/Config/Language/";
    //Tip: 游戏配置文件目录
    private const string gameconfigPath = "Assets/Resources/Config/GameConfig/";
    //Tip: 关卡资源的文件名字
    private const string mapConfigName = "mapconfig";
    //Tip: 多语言文件名字
    private const string languageFileName = "language.json";
    //Tip: 游戏配置文件名字
    private const string gameconfigFileName = "gameconfig.json";
    //Tip: 游戏中保存的数据
    private const string saveFileName = "data.json";

    /// <summary>
    /// Save:
    /// 保存地图数据
    /// 地图数据保存在Resource文件夹下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void SaveMapConfigToJsonFile<T>(T data, int Chapter, int Level)
    {
        var path = mapconfigPath + mapConfigName + "_" + Chapter + "_" + Level + ".json";
        SaveToJsonFile<T>(path, data);
    }

    /// <summary>
    /// Save:
    /// 保存多语言数据
    /// 保存在Resource文件夹下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void SaveLanguageConfigToJsonFile<T>(T data)
    {
        var path = languageconfigPath + languageFileName;
        SaveToJsonFile<T>(path, data);
    }

    /// <summary>
    /// Save:
    /// 保存多语言数据
    /// 保存在Resource文件夹下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void SaveGameConfigToJsonFile<T>(T data)
    {
        var path = gameconfigPath + gameconfigFileName;
        SaveToJsonFile<T>(path, data);
    }


    /// <summary>
    /// Save:
    /// 自动保存游戏成json文件
    /// </summary>
    public void SaveDataToJsonFile()
    {
        SaveDataToJsonFile<SaveData>(GameBase.Instance.saveData);
    }

    /// <summary>
    /// Save:
    /// 保存游戏数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    public void SaveDataToJsonFile<T>(T data)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        SaveToJsonFile<T>(path, data);
    }

    /// <summary>
    /// Delete:
    /// 删除地图配置表文件
    /// </summary>
    /// <param name="Chapter"></param>
    /// <param name="Level"></param>
    public void DeleteMapConfig(int Chapter, int Level)
    {
        var path = mapconfigPath + mapConfigName + "_" + Chapter + "_" + Level + ".json";
        DeleteFile(path);
    }

    /// <summary>
    /// Delete:
    /// 删除存档文件
    /// </summary>
    public void DeleteData()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        DeleteFile(path);
    }

    /// <summary>
    /// Load:
    /// 加载地图数据  
    /// 地图数据在Resouce文件下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadMapConfigFromJson<T>(int Chapter, int Level)
    {
        return LoadFromJsonFile<T>(mapconfigPath + mapConfigName + "_" + Chapter + "_" + Level + ".json");
    }

    /// <summary>
    /// Load:
    /// 加载多语言数据
    /// 语言数据在Resouce文件下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadLanguageConfigFromJson<T>()
    {
        var path = languageconfigPath + languageFileName;
        return LoadFromJsonFile<T>(path);
    }

    /// <summary>
    /// Load:
    /// 加载多语言数据
    /// 语言数据在Resouce文件下
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadGameConfigFromJson<T>()
    {
        var path = gameconfigPath + gameconfigFileName;
        return LoadFromJsonFile<T>(path);
    }

    /// <summary>
    /// Load:
    /// 载入存档数据从Json文件中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadDataFromJson<T>()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return LoadFromJsonFile<T>(path);
    }

    /// <summary>
    /// Exist:
    /// 是否存在一个数据文件
    /// </summary>
    /// <returns></returns>
    public bool ExistDataFile()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return File.Exists(path);
    }

    /// <summary>
    /// Load:
    /// 加载关卡列表
    /// </summary>
    /// <returns></returns>
    public string[] LaodMapConfigDic()
    {
        string directoryPath = Path.Combine(mapconfigPath);
        if (Directory.Exists(directoryPath))
        {
            // 获取目录下的所有文件路径
            string[] files = Directory.GetFiles(directoryPath);
            return files;
        }
        return null;
    }


    /// <summary>
    /// Save:
    /// 保存数据成为json格式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath"></param>
    /// <param name="data"></param>
    private void SaveToJsonFile<T>(string filePath, T data)
    {
        var directoryPath = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        string jsonString = JsonMapper.ToJson(data);
        File.WriteAllText(filePath, jsonString);
    }

    /// <summary>
    /// Delete:
    /// 删除文件
    /// </summary>
    /// <param name="path"></param>
    public void DeleteFile(string path)
    {
        //var filePath = Path.Combine(Application.persistentDataPath, path);
        // 如果文件夹不存在，则创建文件夹
        if (File.Exists(path))
            File.Delete(path);
    }

    /// <summary>
    /// Load:
    /// 载入数据并且反序列化为实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    private T LoadFromJsonFile<T>(string path)
    {
        // 从文件读取 JSON 字符串
        string jsonString = File.ReadAllText(path);
        // 将 JSON 字符串反序列化为对象
        return JsonMapper.ToObject<T>(jsonString);
    }

    /// <summary>
    /// Get：
    /// 获取章节存档
    /// </summary>
    /// <returns></returns>
    public List<LevelData> GetDataFile()
    {
        return GameBase.Instance.saveData.levelData;
    }

    /// <summary>
    /// 获取当前关卡存档
    /// </summary>
    /// <returns></returns>
    public LevelData GetLevelData(int chapter, int level)
    {
        foreach (var item in GameBase.Instance.saveData.levelData)
        {
            if (item.chapter == chapter && item.level == level)
            {
                return item;
            }
        }
        return new LevelData();
    }

    /// <summary>
    /// Clear:
    /// 清理关卡存档，并不是删除存档
    /// </summary>
    /// <param name="chapter">章节</param>
    /// <param name="level">关卡</param>
    public void ClearLevelData(int chapter, int level) 
    {
        LevelData data = null;
        foreach (var item in GameBase.Instance.saveData.levelData)
        {
            if (item.chapter == chapter && item.level == level)
                data = item;
        }
        data.completedTask.Clear();
    }



}