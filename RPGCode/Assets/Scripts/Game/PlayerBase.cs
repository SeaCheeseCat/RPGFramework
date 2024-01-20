using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : NpcBase
{
    //Tip: 角色移动速度
    public float moveSpeed = 5f;
    //Tip： 射线长度
    private const float raycastDistance = 0.5f;
    //Tip: 不可以移动的图层
    private const string nonMovingLayerName = "Wall";
    //Tip：碰撞到不可移动的图层
    private int nonMovingLayer;
    //Tip: 交互半径
    [HideInInspector]
    public float interactionRadius = 1f;
    //Tip: 可以交互的标签
    private const string interactableTag = "NPC";
    //Tip: 当前交互的
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
    /// Check:检测交互
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
    /// 当可交互的物体传来
    /// </summary>
    /// <param name="gameObject"></param>
    public virtual void OnIneract(GameObject ineractobj)
    {
        currentIneract = ineractobj;
    }

    /// <summary>
    /// Input:
    /// 检测输入
    /// </summary>
    void InputKey()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 合并输入方向
        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // 检测各个方向是否碰撞到物体
        bool hitUp = DrawRayAndCheckHit(transform.position, Vector2.up);
        bool hitDown = DrawRayAndCheckHit(transform.position, Vector2.down);
        bool hitLeft = DrawRayAndCheckHit(transform.position, Vector2.left);
        bool hitRight = DrawRayAndCheckHit(transform.position, Vector2.right);

        // 如果碰撞到物体，则禁止相应方向的移动
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

        // 检测碰撞并设置相应方向的移动状态
        bool canMove = !DrawRayAndCheckHit(transform.position, moveDirection);

        if (canMove)
        {
            // 使用Translate进行移动
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Draw:
    /// 画出射线
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    bool DrawRayAndCheckHit(Vector2 origin, Vector2 direction)
    {
        // 发射射线检测碰撞
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, raycastDistance, 1 << nonMovingLayer);

        // 在Scene视图中画出射线
        Debug.DrawRay(origin, direction.normalized * raycastDistance, hit.collider != null ? Color.red : Color.green);

        // 返回是否有碰撞
        return (hit.collider != null);
    }



    /// <summary>
    /// Get:
    /// 获取可交互的物体们
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
    /// 绘制交互半径
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // 在Scene视图中显示交互半径
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
