using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGame : MonoBehaviour
{
    // Tip: 初始关卡 - 章节
    [BoxGroup("初始关卡")]
    public int chapter;

    // Tip: 初始关卡 - 关卡级别
    [BoxGroup("初始关卡")]
    public int level;

    // Tip: 移动速度
    public float moveSpeed = 5f;

    // Tip: 相机对象
    public GameObject cameraObj;

    // Tip: 旋转速度
    public float rotationSpeed = 3f;

    // Tip: Shift 键的移动速度倍率
    public float shiftSpeedMultiplier = 2f;

    // Tip: Alt 键的移动速度倍率
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
    /// 第一人称视角式的移动
    /// </summary>
    public void DoCamerarotation()
    { 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //Tip: 获取相机的右方向
        Vector3 cameraRight = cameraObj.transform.right;
        //Tip: 获取 Shift 和 Alt 键的状态
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isAltPressed = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
        //Tip: 根据按键状态调整移动方向
        Vector3 moveDirection = Vector3.zero;
        if (isShiftPressed)
            moveDirection += Vector3.up;
        else if (isAltPressed)
            moveDirection -= Vector3.up;
        moveDirection += (cameraRight * horizontal + cameraObj.transform.forward * vertical).normalized;
        //Tip: 根据按键状态调整移动速度
        float currentMoveSpeed = moveSpeed;
        if (isShiftPressed)
            currentMoveSpeed *= shiftSpeedMultiplier;
        else if (isAltPressed)
            currentMoveSpeed *= altSpeedMultiplier;
        //Tip: 计算移动距离
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
