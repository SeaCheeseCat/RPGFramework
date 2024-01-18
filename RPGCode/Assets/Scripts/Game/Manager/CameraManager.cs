using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CameraManager : MonoBehaviour
{
    // Tip: 照片对象
    public GameObject photoObj;

    // Tip: 照片画布
    private CanvasGroup photoCanvas;

    // Tip: 图片显示对象
    public RawImage imageDisplay;

    // Tip: 场景相机
    public Camera sceneCamera;

    // Tip: 渲染纹理
    private RenderTexture renderTexture;

    // Tip: 渲染相机
    private Camera renderCamera;

    // Tip: 闪光持续时间
    public float flashDuration = 0.1f;

    // Tip: 闪光强度
    public float flashIntensity = 1.5f;

    // Tip: 闪光图像
    public Image flashImage;

    // Tip: 原始颜色
    public Color originalColor;


    void Start()
    {
        Init();
    }

    /// <summary>
    /// Init:
    /// 初始化数据
    /// </summary>
    public void Init() 
    {
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //Tip: 创建新相机用于渲染
        renderCamera = new GameObject("RenderCamera").AddComponent<Camera>();
        renderCamera.CopyFrom(sceneCamera);
        renderCamera.targetTexture = renderTexture;
        imageDisplay.texture = renderTexture;
        photoCanvas = photoObj.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        OnKeyInput();
    }

    /// <summary>
    /// Callback:
    /// 按键按下
    /// </summary>
    public void OnKeyInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.Instance.cameraModeling)
                return;
            //Tip: 闪光灯
            OpenFlash(); 
            //Tip: 相册
            OpenPhotoObj(); 
            StartCoroutine(DoCaptureScene());  
            //Tip: 任务检测
            GameManager.Instance.OnCameraRotateStop();
            GameManager.Instance.OpenPhoto = true;
            GameManager.Instance.cameraModeling = false;
            CameraUIControls.Instance.CloseCameraModle();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.OpenPhoto)
                return;
            ClosePhotoObj();
            GameManager.Instance.OpenPhoto = false;
            GameManager.Instance.cameraModeling = true;
            CameraUIControls.Instance.OpenCameraModle();
        }

    }

    /// <summary>
    /// Open:
    /// 相册
    /// </summary>
    public void OpenPhotoObj() 
    {
        photoObj.gameObject.SetActive(true);
        photoCanvas.DOFade(1, 0.5f);
    }

    /// <summary>
    /// Close:
    /// 相册
    /// </summary>
    public void ClosePhotoObj() 
    {
        photoCanvas.DOFade(0, 0.5f).OnComplete(() =>
        {
            photoObj.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// Ienumerator:
    /// 拍照
    /// </summary>
    /// <returns></returns>
    IEnumerator DoCaptureScene()
    {       
        Vector3 originalPosition = sceneCamera.transform.position;
        Quaternion originalRotation = sceneCamera.transform.rotation;
        float originalFieldOfView = sceneCamera.fieldOfView;
        //Tip: 将截图相机的状态设置为与主相机相同
        renderCamera.transform.position = sceneCamera.transform.position;
        renderCamera.transform.rotation = sceneCamera.transform.rotation;
        renderCamera.fieldOfView = sceneCamera.fieldOfView;
        yield return null;
        //Tip: 将 RenderTexture 保存为 PNG 文件（也可以选择其他格式）
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();
        //Tip: 保存截图
        byte[] bytes = screenshot.EncodeToPNG();
        string filePath = Application.persistentDataPath + "/screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        System.IO.File.WriteAllBytes(filePath, bytes); 
        //Log: 打印保存路径
        Debug.Log("Screenshot saved to: " + filePath);
        //Tip: 还原摄像机的状态
        sceneCamera.transform.position = originalPosition;
        sceneCamera.transform.rotation = originalRotation;
        sceneCamera.fieldOfView = originalFieldOfView;
        RenderTexture.active = null;
    }

    /// <summary>
    /// Open:
    /// 打开闪光
    /// </summary>
    public void OpenFlash()
    {
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// Ienumerator:
    /// 闪光协程
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashCoroutine()
    {
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, flashIntensity);
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = originalColor;
    }
}
