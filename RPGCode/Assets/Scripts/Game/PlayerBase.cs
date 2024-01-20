using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : NpcBase
{
    //Tip: ��ɫ�ƶ��ٶ�
    public float moveSpeed = 5f;
    //Tip�� ���߳���
    private const float raycastDistance = 0.5f;
    //Tip: �������ƶ���ͼ��
    private const string nonMovingLayerName = "Wall";
    //Tip����ײ�������ƶ���ͼ��
    private int nonMovingLayer;
    //Tip: �����뾶
    [HideInInspector]
    public float interactionRadius = 1f;
    //Tip: ���Խ����ı�ǩ
    private const string interactableTag = "NPC";
    //Tip: ��ǰ������
    private GameObject currentIneract;

    public override void OnCreate()
    {
        base.OnCreate();
        nonMovingLayer = LayerMask.NameToLayer(nonMovingLayerName);
    }

    /// <summary>
    /// Base: update
    /// </summary>
    public virtual void Update()
    {
        InputKey();
        CheckIneract();
    }

    /// <summary>
    /// Check:��⽻��
    /// </summary>
    public void CheckIneract() 
    {
        Collider2D[] interactableObjects = GetInteractWithNearbyObjects();
        if ((interactableObjects == null || interactableObjects.Length <= 0) && currentIneract != null)
            currentIneract = null;
        foreach (Collider2D interactableObject in interactableObjects)
        {
            if(currentIneract == null || (interactableObject.gameObject != null && currentIneract != interactableObject.gameObject))
                OnIneract(interactableObject.gameObject);
        }
    }

    /// <summary>
    /// Callback:
    /// ���ɽ��������崫��
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void OnIneract(GameObject ineractobj)
    {
        currentIneract = ineractobj;
    }

    /// <summary>
    /// Input:
    /// �������
    /// </summary>
    void InputKey()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �ϲ����뷽��
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // �����������Ƿ���ײ������
        bool hitUp = DrawRayAndCheckHit(transform.position, Vector2.up);
        bool hitDown = DrawRayAndCheckHit(transform.position, Vector2.down);
        bool hitLeft = DrawRayAndCheckHit(transform.position, Vector2.left);
        bool hitRight = DrawRayAndCheckHit(transform.position, Vector2.right);

        // �����ײ�����壬���ֹ��Ӧ������ƶ�
        if (hitUp && verticalInput > 0)
        {
            moveDirection.y = 0;
        }

        if (hitDown && verticalInput < 0)
        {
            moveDirection.y = 0;
        }

        if (hitLeft && horizontalInput < 0)
        {
            moveDirection.x = 0;
        }

        if (hitRight && horizontalInput > 0)
        {
            moveDirection.x = 0;
        }

        // �����ײ��������Ӧ������ƶ�״̬
        bool canMove = !DrawRayAndCheckHit(transform.position, moveDirection);

        if (canMove)
        {
            // ʹ��Translate�����ƶ�
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Draw:
    /// ��������
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    bool DrawRayAndCheckHit(Vector2 origin, Vector2 direction)
    {
        // �������߼����ײ
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, raycastDistance, 1 << nonMovingLayer);

        // ��Scene��ͼ�л�������
        Debug.DrawRay(origin, direction.normalized * raycastDistance, hit.collider != null ? Color.red : Color.green);

        // �����Ƿ�����ײ
        return (hit.collider != null);
    }



    /// <summary>
    /// Get:
    /// ��ȡ�ɽ�����������
    /// </summary>
    /// <returns></returns>
    Collider2D[] GetInteractWithNearbyObjects()
    {
        Vector2 playerPosition = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(playerPosition, interactionRadius);
        List<Collider2D> interactableObjects = new List<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            if (collider == m_boxCollider2D)
                continue;
            if (collider.CompareTag(interactableTag))
            {
                interactableObjects.Add(collider);
            }
        }
        return interactableObjects.ToArray();
    }

    /// <summary>
    /// Draw:
    /// ���ƽ����뾶
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // ��Scene��ͼ����ʾ�����뾶
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
