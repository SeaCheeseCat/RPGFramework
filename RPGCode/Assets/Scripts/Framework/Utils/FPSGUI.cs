using UnityEngine;
using System.Collections;
/// <summary>
/// ONGUI֡��ʾ
/// </summary>
public class FPSGUI : MonoBehaviour
{
    private float currentTime = 0;
    private float lateTime = 0;
    private float framesNum = 0;
    private float fpsTime = 0;
    
    GUIStyle labelFont;
    private const int fontsize = 40;  //GUI��ʾ��  �����С
    private Color fontcolor = new Color(1, 1, 1, 1);   //GUI��ʾ��  ������ɫ

    private void Awake()
    {

        //����һ��GUIStyle�Ķ���
        labelFont = new GUIStyle();
        //�����ı���ɫ
        labelFont.normal.textColor = fontcolor;
        //���������С
        labelFont.fontSize = fontsize;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        framesNum++;
        if (currentTime - lateTime >= 1.0f)
        {
            fpsTime = framesNum / (currentTime - lateTime);
            lateTime = currentTime;
            framesNum = 0;
        }
    }
    void OnGUI()
    {
        GUI.Label(new Rect(20, 20, 100, 100), "֡�ʣ�FPS�� : " + string.Format("{0:F}", fpsTime),labelFont);
    }
}