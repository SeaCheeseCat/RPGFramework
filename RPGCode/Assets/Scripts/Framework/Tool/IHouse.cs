using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IHouse : MonoBehaviour 
{

    public float DoorX; //��
    public Transform OutPos;
    public Transform InPos;
    public Collider2D LimitCamaera;
   
    //����
    public virtual void OpenDoor() { 
    }

    public virtual void CloseDoor()
    {
    }

    //������Я������Ϣ
    public string Content1 { get; set; }     //�ҵ�ͷ���˿��ֱ� ��ʾ��ʱ�����賿�����

    public string Conten2 { get; set; }   //���ȹ�   ����ڵ���ҹ��    ֻ��΢���ĵƹⰵʾ���Ĵ���


}
