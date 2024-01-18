using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject Owner;
    private Transform m_transform;
    public float speed = 1.5f;//�����ƶ��ٶ�
    public float moveSpeed = 5f; // �ƶ��ٶ�

    public float swingAmount = 10f; // �ڶ��ĽǶ�
    public float swingSpeed = 5f;   // �ڶ����ٶ�

    private float timeCounter = 0f;


    private void Awake()
    {
        m_transform = Owner.GetComponent<Transform>();
    }

    private void Update()
    {
        //ControlMove();
    }

    public void ControlMove() 
    {
        // ���������ƶ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ��ȡ�����ǰ���򣨲��������תӰ�죩
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // ��y����Ϊ0��ʹ�䱣����ˮƽ����
        cameraForward.Normalize(); // ��һ��

        // �����ƶ�����
        Vector3 moveDirection = (cameraForward * verticalInput + Camera.main.transform.right * horizontalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

     


        // ���Ұڶ�Ч��
        if (moveDirection != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // ʹ�� Lerp ����ƽ����ת
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, swingSpeed * Time.deltaTime);
        }


        // �ƶ�����
        transform.Translate(moveAmount, Space.World);

    }

}
