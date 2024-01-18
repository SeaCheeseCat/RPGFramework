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




    [MenuItem("����������.jpg/��Դ·��", priority = 10)]
    public static void Open()
    {
        MyEditor window = GetWindow<MyEditor>("��Դ·��", true);
    }


 /*   [MenuItem("����������.jpg/�ʼǱ�x", priority = 10)]
    public static void Note()
    {
        NoteEditor window = GetWindow<NoteEditor>("�ʼǱ�x", true);
    }
*/


    [MenuItem("����������.jpg/����Git/TortoiseGit", priority = 10)]
    public static void OpenTortoiseGit()
    {
        LoadPaths();
        OpenGitTool(tortoiseGitPath, "TortoiseGit");
    }

    [MenuItem("����������.jpg/����Git/SourceTreeGit", priority = 10)]
    public static void OpenSourceTreeGit()
    {
        LoadPaths();
        OpenGitTool(sourceTreeGitPath, "SourceTreeGit");
    }

    [MenuItem("����������.jpg/����Git/���� Git ����·��", priority = 20)]
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

            // ���� Git ����
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
        DebugEX.Log("����·��", EditorPrefs.GetString(SourceTreeGitPathKey, ""));
        tortoiseGitPath = EditorPrefs.GetString(TortoiseGitPathKey, "");
        sourceTreeGitPath = EditorPrefs.GetString(SourceTreeGitPathKey, "");
        
       
    }

    public static void SavePaths()
    {
        DebugEX.Log("����", tortoiseGitPath);
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
            GUILayout.Label("�����ָ�����·������Ŀ�ƣ�");
            GUILayout.Label("\nŶ... ...\n�������Щʲô��... ...", EditorStyles.boldLabel);
            if (GUILayout.Button("�浵·��", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DirectoryInfo direction = new DirectoryInfo(PersistentDataPath);
                System.Diagnostics.Process.Start(direction.FullName);
            }
            if (GUILayout.Button("��մ浵", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DeletPathFile(PersistentDataPath);
            }
            if (GUILayout.Button("���ñ�·��", GUILayout.Height(45f), GUILayout.Width(100f)))
            {
                DirectoryInfo direction = new DirectoryInfo(ConfigExcelPath);
                System.Diagnostics.Process.Start(direction.FullName);
            }
            if (GUILayout.Button("��������", GUILayout.Height(45f), GUILayout.Width(100f)))
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
    private float inputFieldWidth = 600; // ���������Ŀ��
    private float inputFieldHeight = 300; // ���������Ŀ��
    private string myTextField = "";

    private bool showSaveDialog = false;
    private string saveDialogMessage = "";
    private string saveDialogFilePath = "";

    private void OnGUI()
    {
        GUILayout.Label("��һ��С�ıʼǱ������Ǽǣ���");
        GUILayout.Label("��ô...�µĹ��¼�����Ļ...", EditorStyles.boldLabel);
        //GUILayout.Label("\n\n��������֮�˶�������Щ���²�����һ�ֳ������ĺ�������µ���̨�罫��Ȼ���ݣ�д������");
        EditorGUIUtility.labelWidth = 100; // ���ñ�ǩ���
        // ���������
        myTextField = EditorGUILayout.TextField("", myTextField, GUILayout.Width(inputFieldWidth), GUILayout.Height(inputFieldHeight));
        // ���水ť
        if (GUILayout.Button("Save - ����˼ά"))
        {
            SaveToFile(myTextField);
        }

        // ��ʾ�Զ������ʾ��
        if (showSaveDialog)
        {
            DrawSaveDialog();
        }
    }

    private void SaveToFile(string textToSave)
    {
        // ��ȡ�����ļ�·��������ʹ�� Application.dataPath ��Ϊʾ��������Ը�����Ҫ�޸�·��
        string fileName = "Think" + DateTime.Now.ToString("HHmmss") + ".txt";

        string filePath = Path.Combine(Application.dataPath+ "/Resources/Note", fileName);

        // д���ı����ļ�
        File.WriteAllText(filePath, textToSave);

        // ������������
        myTextField = "";

        // ���ñ�����ɵ���ʾ����Ϣ
        saveDialogFilePath = "����ɹ���...\n" + filePath;

        // ��ʾ�Զ������ʾ��
        showSaveDialog = true;

        UnityEngine.Debug.Log("�ļ����棺" + filePath);
    }


    private void DrawSaveDialog()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 75, 300, 150), "Save Complete", "Window");

        GUILayout.Label(saveDialogMessage, EditorStyles.wordWrappedLabel);
        GUILayout.Label(saveDialogFilePath, EditorStyles.wordWrappedLabel);

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("ȷ��"))
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
       
        MyEditor.tortoiseGitPath = EditorGUILayout.TextField("Tortoise��...����:", MyEditor.tortoiseGitPath);
        MyEditor.sourceTreeGitPath = EditorGUILayout.TextField("SourceTree��...����:", MyEditor.sourceTreeGitPath);

        if (EditorGUI.EndChangeCheck())
        {
            // ��������ֵ�б仯���򱣴�·��
            MyEditor.SavePaths();
        }


        if (GUILayout.Button("��������..."))
        {
            MyEditor.SavePaths();

            // �رյ�ǰ���ô���
            Close();

        }
    }
}

