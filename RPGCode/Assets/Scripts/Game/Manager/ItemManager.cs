using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleMono<ItemManager>
{
    public List<InteractableObject> items = new List<InteractableObject>();
    /// <summary>
    /// Create:
    /// 创建一个DialogItem
    /// </summary>
    public void CreateDialogItem(NpcBase npc,string content,Dictionary<Language, string> languageValue = null) 
    {
        var item = CreateItem<DialogItem>();
        item.SetFollow(npc);
        item.InitLanguage(languageValue);
        item.SetDialogText(content);
        /*npc.SetDialog(item);*/
        item.transform.parent = npc.gameObject.transform;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.transform.localRotation = Quaternion.Euler(0, 0, 0);
        items.Add(item);
    }

    /// <summary>
    /// Delete:
    /// 删除一个Dialog
    /// </summary>
    /// <param name="npc"></param>
    public void DeleteDialogItem(NpcBase npc)
    {
       /* npc.DeleteDialog();*/
    }

    /// <summary>
    /// Creat:
    /// 创造一个物体并设置跟随
    /// </summary>
    /// <typeparam name="T">物体的泛型</typeparam>
    /// <param name="followTarget">跟随的物体</param>
    /// <returns></returns>
    public T CreateItem<T>(NpcBase followTarget) where T : MonoBehaviour
    {
        var item = CreateItem<DialogItem>();
        item.SetFollow(followTarget);
        items.Add(item);
        return item as T;
    }

    /// <summary>
    /// Create: 泛型
    /// 创建一个Item 
    /// </summary>
    public T CreateItem<T>() where T : InteractableObject
    {
        var prefab = ResourceManager.LoadPrefabSync("Prefabs/Item/" + typeof(T).Name);
        if (prefab != null)
        {
            // 实例化预制体
            GameObject instantiatedPrefab = Instantiate(prefab) as GameObject;
            // 获取或添加组件
            T itemComponent = instantiatedPrefab.GetComponent<T>();
            if (itemComponent == null)
            {
                itemComponent = instantiatedPrefab.AddComponent<T>();
            }
            return itemComponent;
        }
        return null;
    }

    /// <summary>
    /// Callback:
    /// 触发物体拖拽事件
    /// </summary>
    public void OnItemDrag()
    {
        foreach (var item in items)
        {
            item.OnDray();
        }
    }

    /// <summary>
    /// Recycle
    /// </summary>
    public void Recycle()
    {
        items.Clear();
    }

}
