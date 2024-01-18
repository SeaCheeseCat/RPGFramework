using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

/// <summary>
/// 音效管理类
/// 管理统一的音效
/// </summary>
public class AudioManager : SingleMono<AudioManager>
{
    public AudioSource MusicAudio;  //背景音乐组件
    public AudioSource EfficAudio;   //音效组件

    //---------------  默认音效区 --------------------------
    private const string DEFALUTCLICK = "click"; //defalut 默认点击音效
    private const string DEFALUTFAIL = "";   //默认失败音效
   

    //-------------------------储存以及 路径区 ------------------
    private const string MUSICVOLUMEKEY = "MusicVolume";    //背景音量大小  Key （用于储存）
    private const string EFFICVOLUEKEY = "EfficVolume";   //音效音效大小 Key（用于储存）
   
    private const string PATH = "Audio/Effic/";           //音效的统一路径
    private const string MUSICPATH = "Audio/Music/";      //背景音效统一路径


    private float MusicMax;
    private float EfficMax;

    // -----------------------配置区 （单独生成配置表 对于个人开发太麻烦了 直接写下来比较高效） ----------------
    private Dictionary<int, string> PageMusicConfig = new Dictionary<int, string>();

    

    /// <summary>
    /// 初始化组件数据
    /// </summary>
    public void InitCompentConfig()
    {
        MusicAudio = transform.Find("Music").GetComponent<AudioSource>();
        EfficAudio = transform.Find("Audio").GetComponent<AudioSource>();
        MusicAudio.volume = PlayerPrefs.GetFloat(MUSICVOLUMEKEY, 1);
        EfficAudio.volume = PlayerPrefs.GetFloat(EFFICVOLUEKEY, 1);
        MusicMax = MusicAudio.volume;
        EfficMax = EfficAudio.volume;
        DontDestroyOnLoad(gameObject);
        AudioSettings.OnAudioConfigurationChanged += OnAudioConfigurationChanged;
    }



    /// <summary>
    /// 初始化 音效设置
    /// </summary>
    public void Init()
    {
        if (!PlayerPrefs.HasKey(MUSICVOLUMEKEY))
            PlayerPrefs.SetFloat(MUSICVOLUMEKEY, 0.5f);
        if (!PlayerPrefs.HasKey(EFFICVOLUEKEY))
            PlayerPrefs.SetFloat(EFFICVOLUEKEY, 0.5f);
        SetMusicVolume(PlayerPrefs.GetFloat(MUSICVOLUMEKEY));
        SetEfficVolume(PlayerPrefs.GetFloat(EFFICVOLUEKEY));
    }


    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name">音效名称</param>
    /// <param name="isLoop">是否循环</param>
    public void PlayMusic(string name, bool isLoop)
    {
        if (name == "")
            return;
        MusicAudio.clip = Resources.Load<AudioClip>(MUSICPATH + name);
        MusicAudio.loop = isLoop;
        MusicAudio.Play();
    }

    /// <summary>
    /// 播放章节背景音效（配置在上面）
    /// </summary>
    /// <param name="page"></param>
    public void PlayPageMusic(int page)
    {
        if (!PageMusicConfig.ContainsKey(page))
            return;
        var name = PageMusicConfig[page];
        PlayMusic(name, true);
    }

    /// <summary>
    /// 播放默认点击音效
    /// </summary>
    public void PlayDefaultClickAudio()
    {
        PlayEffic(DEFALUTCLICK);
    }



    /// <summary>
    /// 获取音乐大小
    /// </summary>
    /// <returns>音效大小</returns>
    public float GetMusicVolum()
    {
        return MusicAudio.volume;
    }



    /// <summary>
    /// 获取音效大小
    /// </summary>
    /// <returns>音效大小</returns>
    public float GetEffectVolum()
    {
        return EfficAudio.volume;
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="name"> 音效名字 </param>
    /// <param name="time"> 持续时间 </param>
    /// <param name="grad"> 是否是渐变音效 </param>
    public void PlayEffic(string name, float time = 0, bool grad = false)
    {
        DebugEX.Log("播放音效   ", name);
        if (EfficAudio.clip == null || EfficAudio.clip.name != name)
        {
            EfficAudio.clip = Resources.Load<AudioClip>(PATH + name);
        }
        EfficAudio.Play();

        if (time != 0)
            StartCoroutine(StopEffic(time));

        if (grad )
        {
            var cliplength = 0f;
            if (time == 0)
                cliplength =  EfficAudio.clip.length;
            else
                cliplength = time;

            StartCoroutine(GradEffic(cliplength));
        }

    }

    IEnumerator StopEffic(float time) {

        yield return new WaitForSeconds(time);

        EfficAudio.Stop();
    }

    IEnumerator GradEffic(float cliplength)
    {
        EfficAudio.volume = 0;
        var startlength = EfficMax ;
        var endlength = EfficMax ;
        while (EfficAudio.volume < EfficMax)
        {
            EfficAudio.volume += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(cliplength - (endlength+ startlength));

        while (EfficAudio.volume > 0)
        {
            EfficAudio.volume -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


        EfficAudio.volume = EfficMax;

        EfficAudio.Stop();
    }

    /// <summary>
    /// 设置背景音量大小
    /// </summary>
    /// <param name="num">音量大小  最大100</param>
    public void SetMusicVolume(float num)
    {
        MusicAudio.volume = num;
        DebugEX.Log("设置音乐大小", num);
        PlayerPrefs.SetFloat(MUSICVOLUMEKEY, num);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 设置音效音量大小
    /// </summary>
    /// <param name="num">音量大小 最大100</param>
    public void SetEfficVolume(float num)
    {
        EfficAudio.volume = num;
        PlayerPrefs.SetFloat(EFFICVOLUEKEY, num);
        PlayerPrefs.Save();
    }


    //执行协成函数 并且返回时间
    private IEnumerator AudioPlayFinished(float time, UnityAction callback)
    {
        yield return new WaitForSeconds(time);
        //声音播放完毕后的回调方法
        callback?.Invoke();
    }

    /// <summary>
    /// 切换设备（蓝牙）后重新播放背景音乐
    /// </summary>
    /// <param name="deviceWasChanged"></param>
    void OnAudioConfigurationChanged(bool deviceWasChanged)
    {
        if (deviceWasChanged)
        {
            AudioConfiguration config = AudioSettings.GetConfiguration();
            config.dspBufferSize = 64;
            AudioSettings.Reset(config);
        }
        MusicAudio.Play();
    }

}
