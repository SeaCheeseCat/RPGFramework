using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SingleMono<ItemManager>
{
    public List<InteractableObject> items = new List<InteractableObject>();
    /// <summary>
    /// Create:
    /// ����һ��DialogItem
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
    /// ɾ��һ��Dialog
    /// </summary>
    /// <param name="npc"></param>
    public void DeleteDialogItem(NpcBase npc)
    {
       /* npc.DeleteDialog();*/
    }

    /// <summary>
    /// Creat:
    /// ����һ�����岢���ø���
    /// </summary>
    /// <typeparam name="T">����ķ���</typeparam>
    /// <param name="followTarget">���������</param>
    /// <returns></returns>
    public T CreateItem<T>(NpcBase followTarget) where T : MonoBehaviour
    {
        var item = CreateItem<DialogItem>();
        item.SetFollow(followTarget);
        items.Add(item);
        return item as T;
    }

    /// <summary>
    /// Create: ����
    /// ����һ��Item 
    /// </summary>
    public T CreateItem<T>() where T : InteractableObject
    {
        var prefab = ResourceManager.LoadPrefabSync("Prefabs/Item/" + typeof(T).Name);
        if (prefab != null)
        {
            // ʵ����Ԥ����
            GameObject instantiatedPrefab = Instantiate(prefab) as GameObject;
            // ��ȡ��������
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
    /// ����������ק�¼�
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
