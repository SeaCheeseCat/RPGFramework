using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class CameraUIControls : SingleMono<CameraUIControls>
{

    // Tip: ��Ұ����
    public GameObject fovObj;

    // Tip: �Զ���������
    public Texture2D customCursorTexture;

    // Tip: ������ű���
    public float cursorScale = 1.5f;

    // Tip: �� Unity �༭���н� Image ��������ֶ�
    public Image customCursorImage;

    // Tip: �Զ������ RectTransform
    private RectTransform customCursorRectTransform;

    // Tip: ��Ұ����
    public CanvasGroup fovCanvas;

    // Tip: ���ݻ���
    public CanvasGroup dataCanvas;


    /// <summary>
    /// ��ȡ��������ģʽ
    /// </summary>
    public void OpenCameraModle() 
    {
        SetCustomCursor(cursorScale);
        customCursorImage.gameObject.SetActive(true);
        // ����ϵͳ���
        Cursor.visible = false;
        GameManager.Instance.cameraModeling = true;
        OpenFovUI();
        OpenDataUI();

    }

    /// <summary>
    /// �ر�ȡ��������ģʽ
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
    /// �򿪽������
    /// </summary>
    public void OpenFovUI() {
        fovCanvas.gameObject.SetActive(true);
        fovCanvas.DOFade(1, 0.5f);
        fovCanvas.gameObject.transform.DOLocalMoveX(-210, 0.6f);
    }

    /// <summary>
    /// �رս������
    /// </summary>
    public void CloseFovUI() {
       
        fovCanvas.DOFade(0, 0.5f);
        fovCanvas.gameObject.transform.DOLocalMoveX(-332f, 0.6f).OnComplete(()=> 
        {
            fovCanvas.gameObject.SetActive(false);
        });
    }

    /// <summary>
    /// ��������ʾ
    /// </summary>
    public void OpenDataUI()
    {
        dataCanvas.gameObject.SetActive(true);
        dataCanvas.DOFade(1, 0.5f);
        dataCanvas.transform.DOLocalMoveY(-190, 0.6f);
    
    }

    /// <summary>
    /// �ر�������ʵ
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
            // �����Զ�������λ��
            customCursorRectTransform.position = mousePosition;
        }
    }

    void SetCustomCursor(float scale)
    {
        customCursorRectTransform.sizeDelta = new Vector2(customCursorTexture.width * scale, customCursorTexture.height * scale);
        customCursorImage.sprite = Sprite.Create(customCursorTexture, new Rect(0, 0, customCursorTexture.width, customCursorTexture.height), Vector2.zero);
    }


}
