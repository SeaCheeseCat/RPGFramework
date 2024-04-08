using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArchiveManager : Manager<ArchiveManager>
{
    //-------------- �浵ϵͳ -------------- 
    //һ������MapConfigҪ��Ĵ浵��ֻҪ��һЩ������������ʹ��
    //��һ��������Ϸ�浵����Ƶ����Ϊ�˷���ֱ�Ӵ浽��ע�����PlayPefer��

    //Tip:��������Ӧ�ñ��浽��Ϸ����  �������ⲿ����  (ʧ���ˣ���Resource��ֻ����  ����д��  �����ظĻ�ȥ)
    private const string mapconfigPath = "Assets/Resources/Config/Map/";  //����·��    
    //Tip: ���������ô���·��
    private const string languageconfigPath = "Assets/Resources/Config/Language/";
    //Tip: ��Ϸ�����ļ�Ŀ¼
    private const string gameconfigPath = "Assets/Resources/Config/GameConfig/";
    //Tip: �ؿ���Դ���ļ�����
    private const string mapConfigName = "mapconfig";
    //Tip: �������ļ�����
    private const string languageFileName = "language.json";
    //Tip: ��Ϸ�����ļ�����
    private const string gameconfigFileName = "gameconfig.json";
    //Tip: ��Ϸ�б��������
    private const string saveFileName = "data.json";

    /// <summary>
    /// Save:
    /// �����ͼ����
    /// ��ͼ���ݱ�����Resource�ļ�����
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
    /// �������������
    /// ������Resource�ļ�����
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
    /// �������������
    /// ������Resource�ļ�����
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
    /// �Զ�������Ϸ��json�ļ�
    /// </summary>
    public void SaveDataToJsonFile()
    {
        SaveDataToJsonFile<SaveData>(GameBase.Instance.saveData);
    }

    /// <summary>
    /// Save:
    /// ������Ϸ����
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
    /// ɾ����ͼ���ñ��ļ�
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
    /// ɾ���浵�ļ�
    /// </summary>
    public void DeleteData()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        DeleteFile(path);
    }

    /// <summary>
    /// Load:
    /// ���ص�ͼ����  
    /// ��ͼ������Resouce�ļ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadMapConfigFromJson<T>(int Chapter, int Level)
    {
        return LoadFromJsonFile<T>(mapconfigPath + mapConfigName + "_" + Chapter + "_" + Level + ".json");
    }

    /// <summary>
    /// Load:
    /// ���ض���������
    /// ����������Resouce�ļ���
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
    /// ���ض���������
    /// ����������Resouce�ļ���
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
    /// ����浵���ݴ�Json�ļ���
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
    /// �Ƿ����һ�������ļ�
    /// </summary>
    /// <returns></returns>
    public bool ExistDataFile()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        return File.Exists(path);
    }

    /// <summary>
    /// Load:
    /// ���عؿ��б�
    /// </summary>
    /// <returns></returns>
    public string[] LaodMapConfigDic()
    {
        string directoryPath = Path.Combine(mapconfigPath);
        if (Directory.Exists(directoryPath))
        {
            // ��ȡĿ¼�µ������ļ�·��
            string[] files = Directory.GetFiles(directoryPath);
            return files;
        }
        return null;
    }


    /// <summary>
    /// Save:
    /// �������ݳ�Ϊjson��ʽ
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
    /// ɾ���ļ�
    /// </summary>
    /// <param name="path"></param>
    public void DeleteFile(string path)
    {
        //var filePath = Path.Combine(Application.persistentDataPath, path);
        // ����ļ��в����ڣ��򴴽��ļ���
        if (File.Exists(path))
            File.Delete(path);
    }

    /// <summary>
    /// Load:
    /// �������ݲ��ҷ����л�Ϊʵ��
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path"></param>
    /// <returns></returns>
    private T LoadFromJsonFile<T>(string path)
    {
        // ���ļ���ȡ JSON �ַ���
        string jsonString = File.ReadAllText(path);
        // �� JSON �ַ��������л�Ϊ����
        return JsonMapper.ToObject<T>(jsonString);
    }

    /// <summary>
    /// Get��
    /// ��ȡ�½ڴ浵
    /// </summary>
    /// <returns></returns>
    public List<LevelData> GetDataFile()
    {
        return GameBase.Instance.saveData.levelData;
    }

    /// <summary>
    /// ��ȡ��ǰ�ؿ��浵
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
    /// ����ؿ��浵��������ɾ���浵
    /// </summary>
    /// <param name="chapter">�½�</param>
    /// <param name="level">�ؿ�</param>
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