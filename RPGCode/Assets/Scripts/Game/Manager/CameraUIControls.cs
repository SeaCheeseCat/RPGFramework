using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CameraUIControls : SingleMono<CameraUIControls>
{

    // Tip: 视野对象
    public GameObject fovObj;

    // Tip: 自定义光标纹理
    public Texture2D customCursorTexture;

    // Tip: 光标缩放比例
    public float cursorScale = 1.5f;

    // Tip: 在 Unity 编辑器中将 Image 分配给该字段
    public Image customCursorImage;

    // Tip: 自定义光标的 RectTransform
    private RectTransform customCursorRectTransform;

    // Tip: 视野画布
    public CanvasGroup fovCanvas;

    // Tip: 数据画布
    public CanvasGroup dataCanvas;


    /// <summary>
    /// 打开取景器摄像模式
    /// </summary>
    public void OpenCameraModle() 
    {
        SetCustomCursor(cursorScale);
        customCursorImage.gameObject.SetActive(true);
        // 隐藏系统鼠标
        Cursor.visible = false;
        GameManager.Instance.cameraModeling = true;
        OpenFovUI();
        OpenDataUI();

    }

    /// <summary>
    /// 关闭取景器摄像模式
    /// </summary>
    public void CloseCameraModle() 
    {
        customCursorImage.gameObject.SetActive(false);
        Cursor.visible = true;
        GameManager.Instance.cameraModeling = false;
        CloseDataUI();
        CloseFovUI();
        GameManager.Instance.ResetNpc();
    }

    /// <summary>
    /// 打开焦距界面
    /// </summary>
    public void OpenFovUI() {
        fovCanvas.gameObject.SetActive(true);
        fovCanvas.DOFade(1, 0.5f);
        fovCanvas.gameObject.transform.DOLocalMoveX(-210, 0.6f);
    }

    /// <summary>
    /// 关闭焦距界面
    /// </summary>
    public void CloseFovUI() {
       
        fovCanvas.DOFade(0, 0.5f);
        fovCanvas.gameObject.transform.DOLocalMoveX(-332f, 0.6f).OnComplete(()=> 
        {
            fovCanvas.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// 打开数据显示
    /// </summary>
    public void OpenDataUI()
    {
        dataCanvas.gameObject.SetActive(true);
        dataCanvas.DOFade(1, 0.5f);
        dataCanvas.transform.DOLocalMoveY(-190, 0.6f);
    
    }

    /// <summary>
    /// 关闭数据现实
    /// </summary>
    public void CloseDataUI() 
    {
        dataCanvas.DOFade(0, 0.5f);
        dataCanvas.transform.DOLocalMoveY(-237f, 0.6f).OnComplete(() => {
            dataCanvas.gameObject.SetActive(false);
        });
    }


    void Start()
    {
        customCursorRectTransform = customCursorImage.GetComponent<RectTransform>();
    }


    private void Update()
    {
        fovObj.transform.DOLocalMoveY(-GameManager.Instance.fovValue * 7 + 96,0.2f);
        if (GameManager.Instance.cameraModeling)
        {
            Vector3 mousePosition = Input.mousePosition;
            // 更新自定义鼠标的位置
            customCursorRectTransform.position = mousePosition;
        }
    }

    void SetCustomCursor(float scale)
    {
        customCursorRectTransform.sizeDelta = new Vector2(customCursorTexture.width * scale, customCursorTexture.height * scale);
        customCursorImage.sprite = Sprite.Create(customCursorTexture, new Rect(0, 0, customCursorTexture.width, customCursorTexture.height), Vector2.zero);
    }


}
