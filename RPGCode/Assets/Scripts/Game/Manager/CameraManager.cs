using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class CameraManager : MonoBehaviour
{
    // Tip: ��Ƭ����
    public GameObject photoObj;

    // Tip: ��Ƭ����
    private CanvasGroup photoCanvas;

    // Tip: ͼƬ��ʾ����
    public RawImage imageDisplay;

    // Tip: �������
    public Camera sceneCamera;

    // Tip: ��Ⱦ����
    private RenderTexture renderTexture;

    // Tip: ��Ⱦ���
    private Camera renderCamera;

    // Tip: �������ʱ��
    public float flashDuration = 0.1f;

    // Tip: ����ǿ��
    public float flashIntensity = 1.5f;

    // Tip: ����ͼ��
    public Image flashImage;

    // Tip: ԭʼ��ɫ
    public Color originalColor;


    void Start()
    {
        Init();
    }

    /// <summary>
    /// Init:
    /// ��ʼ������
    /// </summary>
    public void Init() 
    {
        renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        //Tip: ���������������Ⱦ
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
    /// ��������
    /// </summary>
    public void OnKeyInput() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!GameManager.Instance.cameraModeling)
                return;
            //Tip: �����
            OpenFlash(); 
            //Tip: ���
            OpenPhotoObj(); 
            StartCoroutine(DoCaptureScene());  
            //Tip: ������
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
    /// ���
    /// </summary>
    public void OpenPhotoObj() 
    {
        photoObj.gameObject.SetActive(true);
        photoCanvas.DOFade(1, 0.5f);
    }

    /// <summary>
    /// Close:
    /// ���
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
    /// ����
    /// </summary>
    /// <returns></returns>
    IEnumerator DoCaptureScene()
    {       
        Vector3 originalPosition = sceneCamera.transform.position;
        Quaternion originalRotation = sceneCamera.transform.rotation;
        float originalFieldOfView = sceneCamera.fieldOfView;
        //Tip: ����ͼ�����״̬����Ϊ���������ͬ
        renderCamera.transform.position = sceneCamera.transform.position;
        renderCamera.transform.rotation = sceneCamera.transform.rotation;
        renderCamera.fieldOfView = sceneCamera.fieldOfView;
        yield return null;
        //Tip: �� RenderTexture ����Ϊ PNG �ļ���Ҳ����ѡ��������ʽ��
        Texture2D screenshot = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        screenshot.Apply();
        //Tip: �����ͼ
        byte[] bytes = screenshot.EncodeToPNG();
        string filePath = Application.persistentDataPath + "/screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
        System.IO.File.WriteAllBytes(filePath, bytes); 
        //Log: ��ӡ����·��
        Debug.Log("Screenshot saved to: " + filePath);
        //Tip: ��ԭ�������״̬
        sceneCamera.transform.position = originalPosition;
        sceneCamera.transform.rotation = originalRotation;
        sceneCamera.fieldOfView = originalFieldOfView;
        RenderTexture.active = null;
    }

    /// <summary>
    /// Open:
    /// ������
    /// </summary>
    public void OpenFlash()
    {
        StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// Ienumerator:
    /// ����Э��
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashCoroutine()
    {
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, flashIntensity);
        yield return new WaitForSeconds(flashDuration);
        flashImage.color = originalColor;
    }
}
