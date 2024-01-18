using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Tip: 旋转的目标
    public Transform target;

    // Tip: 控制水平旋转速度，如果为0，则禁用水平旋转
    public float xSpeed = 200;

    // Tip: 控制垂直旋转速度，如果为0，则禁用垂直旋转
    public float ySpeed = 200;

    // Tip: 鼠标滚轮缩放速度
    public float mSpeed = 10;

    // Tip: 垂直旋转的最小角度限制
    public float yMinLimit = -50;

    // Tip: 垂直旋转的最大角度限制
    public float yMaxLimit = 50;

    // Tip: 相机与目标的初始距离
    public float distance = 2;

    // Tip: 缩放的最小距离
    public float minDistance = 2;

    // Tip: 缩放的最大距离
    public float maxDistance = 30;

    // Tip: 是否启用平滑插值
    public bool needDamping = true;

    // Tip: 平滑插值的速度
    float damping = 5.0f;

    // Tip: 当前水平旋转角度
    public float x = 0.0f;

    // Tip: 当前垂直旋转角度
    public float y = 0.0f;

    // Tip: 示例：x值
    public float xval = 0f;

    // Tip: 示例：y值
    public float yval = 0f;
    
    void Start()
    {
        Init();
    }

    /// <summary>
    /// Init:
    /// 初始化
    /// </summary>
    public void Init() 
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        OnMouseRotation();
    }

    /// <summary>
    /// Callback:
    /// 鼠标旋转
    /// </summary>
    public void OnMouseRotation() 
    {
        if (target && !GameManager.Instance.OpenPhoto)
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                xval = (y % 360f);
                yval = x % 360f;
                GameManager.Instance.cameraRotateX = xval;
                GameManager.Instance.cameraRotateY = yval;
                y = ClampAngle(y, yMinLimit, yMaxLimit);
                GameManager.Instance.rotaing = true;
                GameManager.Instance.OnCameraRotateStart();
            }
            else
            {
                if (GameManager.Instance.rotaing)
                    GameManager.Instance.rotaing = false;
            }
            distance -= Input.GetAxis("Mouse ScrollWheel") * mSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
            GameManager.Instance.fovValue = distance;
            //Tip: 计算旋转和位置信息
            Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
            Vector3 disVector = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * disVector + target.position;
            //Tip: 调整相机的旋转和位置
            if (needDamping)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * damping);
            }
            else
            {
                transform.rotation = rotation;
                transform.position = position;
            }
            ItemManager.Instance.OnItemDrag();
        }
    }


    /// <summary>
    /// Clamp:
    /// 限制旋转角度
    /// </summary>
    /// <param name="angle">角度值</param>
    /// <param name="min">最小值</param>
    /// <param name="max">最大值</param>
    /// <returns></returns>
    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
