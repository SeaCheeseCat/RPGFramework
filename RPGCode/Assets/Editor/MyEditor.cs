using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public  class MyEditor : EditorWindow
{
    private const string TortoiseGitPathKey = "TortoiseGitPath";
    private const string SourceTreeGitPathKey = "SourceTreeGitPath";


    public static string tortoiseGitPath;
    public static string sourceTreeGitPath;




    [MenuItem("阴暗地爬行.jpg/资源路径", priority = 10)]
    public static void Open()
    {
        MyEditor window = GetWindow<MyEditor>("资源路径", true);
    }


 /*   [MenuItem("阴暗地爬行.jpg/笔记本x", priority = 10)]
    public static void Note()
    {
        NoteEditor window = GetWindow<NoteEditor>("笔记本x", true);
    }
*/


    [MenuItem("阴暗地爬行.jpg/启动Git/TortoiseGit", priority = 10)]
    public static void OpenTortoiseGit()
    {
        LoadPaths();
        OpenGitTool(tortoiseGitPath, "TortoiseGit");
    }

    [MenuItem("阴暗地爬行.jpg/启动Git/SourceTreeGit", priority = 10)]
    public static void OpenSourceTreeGit()
    {
        LoadPaths();
        OpenGitTool(sourceTreeGitPath, "SourceTreeGit");
    }

    [MenuItem("阴暗地爬行.jpg/启动Git/配置 Git 工具路径", priority = 20)]
    public static void OpenGitConfig()
    {
        GetWindow<DarkCrawlingConfig>("Git Config");
    }


    private static void OpenGitTool(string toolPath, string toolName)
    {
        try
        {
            if (string.IsNullOrEmpty(toolPath) || !System.IO.File.Exists(toolPath))
            {
                UnityEngine.Debug.LogError($"Failed to launch {toolName}: Path not configured or invalid.");
                return;
            }

            // 启动 Git 工具
            Process.Start(toolPath);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Failed to launch {toolName}: {e.Message}");
        }
    }

    private static void ConfigGitPaths()
    {
        tortoiseGitPath = EditorPrefs.GetString(TortoiseGitPathKey, "");
        sourceTreeGitPath = EditorPrefs.GetString(SourceTreeGitPathKey, "");
      

        tortoiseGitPath = EditorGUILayout.TextField("TortoiseGit Path:", tortoiseGitPath);
        sourceTreeGitPath = EditorGUILayout.TextField("SourceTreeGit Path:", sourceTreeGitPath);


        if (GUILayout.Button("Save Paths"))
        {
            SavePaths();
        }
    }

    public static void LoadPaths()
    {
        DebugEX.Log("载入路径", EditorPrefs.GetString(SourceTreeGitPathKey, ""));
        tortoiseGitPath = EditorPrefs.GetString(TortoiseGitPathKey, "");
        sourceTreeGitPath = EditorPrefs.GetString(SourceTreeGitPathKey, "");
        
       
    }

    public static void SavePaths()
    {
        DebugEX.Log("保存", tortoiseGitPath);
        EditorPrefs.SetString(TortoiseGitPathKey, tortoiseGitPath);
        EditorPrefs.SetString(SourceTreeGitPathKey, sourceTreeGitPath);
    }

    public static string PersistentDataPath
    {
        get
        {
            string path =
#if UNITY_ANDROID
         Application.persistentDataPath;
#elif UNITY_IPHONE && !UNITY_EDITOR
         Application.persistentDataPath;
#elif UNITY_STANDLONE_WIN || UNITY_EDITOR
         Application.persistentDataPath;
#else
        string.Empty;
#endif
            return path;
        }
    }




    public static string ConfigExcelPath
    {
        get
        {
            string path = "../../All-Doe-s-Life-Res/All-Doe-s-Life-Tool/ExcelTool/ExcelConfig";
            return path;
        }
    }

    public static string ConfigBatPath
    {
        get
        {
            string path = "../Tool/ExcelTool";
            return path;
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(position.width), GUILayout.Height(position.height));
        {
            GUILayout.Label("（各种各样的路径）（目移）");
            GUILayout.Label("\n哦... ...\n你想查阅些什么呢... ...", EditorStyles.boldLabel);
            if (GUILayout.Button("存档路径", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DirectoryInfo direction = new DirectoryInfo(PersistentDataPath);
                System.Diagnostics.Process.Start(direction.FullName);
            }
            if (GUILayout.Button("清空存档", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DeletPathFile(PersistentDataPath);
            }
            if (GUILayout.Button("配置表路径", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DirectoryInfo direction = new DirectoryInfo(ConfigExcelPath);
                System.Diagnostics.Process.Start(direction.FullName);
            }
            if (GUILayout.Button("生成配置", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                RunBat("", ConfigBatPath + "/run.bat");
            }

        }
        EditorGUILayout.EndVertical();
    }


    public static void DeletPathFile(string path)
    {
        DirectoryInfo direction = new DirectoryInfo(path);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        UnityEngine.Debug.Log(files.Length);
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta"))
            {
                continue;
            }
            string FilePath = path + "/" + files[i].Name;
            File.Delete(FilePath);
        }
    }





    public static System.Diagnostics.Process CreateShellExProcess(string cmd, string args, string workingDir = "")

    {

        var pStartInfo = new System.Diagnostics.ProcessStartInfo(cmd);

        pStartInfo.Arguments = args;

        pStartInfo.CreateNoWindow = false;

        pStartInfo.UseShellExecute = true;

        pStartInfo.RedirectStandardError = false;

        pStartInfo.RedirectStandardInput = false;

        pStartInfo.RedirectStandardOutput = false;

        if (!string.IsNullOrEmpty(workingDir))

            pStartInfo.WorkingDirectory = workingDir;

        return System.Diagnostics.Process.Start(pStartInfo);

    }




    public static void RunBat(string batfile, string args, string workingDir = "")

    {

        var p = CreateShellExProcess(batfile, args, workingDir);

        p.Close();

    }



    public static string FormatPath(string path)
    {

        path = path.Replace("/", "\\");

        if (Application.platform == RuntimePlatform.OSXEditor)

            path = path.Replace("\\", "/");
        return path;
    }

}


public class NoteEditor : EditorWindow
{
    private float inputFieldWidth = 600; // 设置输入框的宽度
    private float inputFieldHeight = 300; // 设置输入框的宽度
    private string myTextField = "";

    private bool showSaveDialog = false;
    private string saveDialogMessage = "";
    private string saveDialogFilePath = "";

    private void OnGUI()
    {
        GUILayout.Label("（一个小的笔记本，随便记记！）");
        GUILayout.Label("那么...新的故事即将开幕...", EditorStyles.boldLabel);
        //GUILayout.Label("\n\n但凡清醒之人都明白那些故事不过是一种彻底癫狂后的胡言乱语，新的舞台剧将悄然上演，写下来：");
        EditorGUIUtility.labelWidth = 100; // 设置标签宽度
        // 创建输入框
        myTextField = EditorGUILayout.TextField("", myTextField, GUILayout.Width(inputFieldWidth), GUILayout.Height(inputFieldHeight));
        // 保存按钮
        if (GUILayout.Button("Save - 保存思维"))
        {
            SaveToFile(myTextField);
        }

        // 显示自定义的提示框
        if (showSaveDialog)
        {
            DrawSaveDialog();
        }
    }

    private void SaveToFile(string textToSave)
    {
        // 获取保存文件路径，这里使用 Application.dataPath 作为示例，你可以根据需要修改路径
        string fileName = "Think" + DateTime.Now.ToString("HHmmss") + ".txt";

        string filePath = Path.Combine(Application.dataPath+ "/Resources/Note", fileName);

        // 写入文本到文件
        File.WriteAllText(filePath, textToSave);

        // 清空输入框内容
        myTextField = "";

        // 设置保存完成的提示框信息
        saveDialogFilePath = "保存成功！...\n" + filePath;

        // 显示自定义的提示框
        showSaveDialog = true;

        UnityEngine.Debug.Log("文件保存：" + filePath);
    }


    private void DrawSaveDialog()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150), "Save Complete", "Window");

        GUILayout.Label(saveDialogMessage, EditorStyles.wordWrappedLabel);
        GUILayout.Label(saveDialogFilePath, EditorStyles.wordWrappedLabel);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("确定"))
        {
            showSaveDialog = false;
            saveDialogMessage = "";
            saveDialogFilePath = "";
        }

        GUILayout.EndArea();
    }

}

public class DarkCrawlingConfig : EditorWindow
{
    private void OnGUI()
    {
       
        MyEditor.tortoiseGitPath = EditorGUILayout.TextField("Tortoise在...在哪:", MyEditor.tortoiseGitPath);
        MyEditor.sourceTreeGitPath = EditorGUILayout.TextField("SourceTree在...在哪:", MyEditor.sourceTreeGitPath);

        if (EditorGUI.EndChangeCheck())
        {
            // 如果输入框值有变化，则保存路径
            MyEditor.SavePaths();
        }


        if (GUILayout.Button("就是这样..."))
        {
            MyEditor.SavePaths();

            // 关闭当前配置窗口
            Close();

        }
    }
}

