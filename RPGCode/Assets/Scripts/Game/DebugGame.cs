using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGame : MonoBehaviour
{
    // Tip: ��ʼ�ؿ� - �½�
    [BoxGroup("��ʼ�ؿ�")]
    public int chapter;

    // Tip: ��ʼ�ؿ� - �ؿ�����
    [BoxGroup("��ʼ�ؿ�")]
    public int level;

    // Tip: �ƶ��ٶ�
    public float moveSpeed = 5f;

    // Tip: �������
    public GameObject cameraObj;

    // Tip: ��ת�ٶ�
    public float rotationSpeed = 3f;

    // Tip: Shift �����ƶ��ٶȱ���
    public float shiftSpeedMultiplier = 2f;

    // Tip: Alt �����ƶ��ٶȱ���
    public float altSpeedMultiplier = 2f;

    /// <summary>
    /// Base: start
    /// </summary>
    public void Start()
    {
        GameBaseData.Chapter = chapter;
        GameBaseData.Level = level;
    }
    
    /// <summary>
    /// Base: update
    /// </summary>
    void Update()
    {
        //Camerarotation();
    }

    /// <summary>
    /// Do:
    /// ��һ�˳��ӽ�ʽ���ƶ�
    /// </summary>
    public void DoCamerarotation()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Tip: ��ȡ������ҷ���
        Vector3 cameraRight = cameraObj.transform.right;
        //Tip: ��ȡ Shift �� Alt ����״̬
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isAltPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
        //Tip: ���ݰ���״̬�����ƶ�����
        Vector3 moveDirection = Vector3.zero;
        if (isShiftPressed)
            moveDirection += Vector3.up;
        else if (isAltPressed)
            moveDirection -= Vector3.up;
        moveDirection += (cameraRight * horizontal + cameraObj.transform.forward * vertical).normalized;
        //Tip: ���ݰ���״̬�����ƶ��ٶ�
        float currentMoveSpeed = moveSpeed;
        if (isShiftPressed)
            currentMoveSpeed *= shiftSpeedMultiplier;
        else if (isAltPressed)
            currentMoveSpeed *= altSpeedMultiplier;
        //Tip: �����ƶ�����
        Vector3 moveAmount = moveDirection * currentMoveSpeed * Time.deltaTime;
        cameraObj.transform.Translate(moveAmount, Space.World);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotationX = -mouseY * rotationSpeed;
        float rotationY = mouseX * rotationSpeed;
        Vector3 currentRotation = cameraObj.transform.rotation.eulerAngles;
        currentRotation.x += rotationX;
        currentRotation.y += rotationY;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);
        cameraObj.transform.rotation = Quaternion.Euler(currentRotation);
    }


}
