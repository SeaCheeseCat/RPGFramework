using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    //Tip: ȫ���Ĺؿ����� 
    public LevelCfg[] levelcfgs;

    /// <summary>
    /// Init:
    /// ��ʼ��
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override IEnumerator Init(MonoBehaviour obj)
    {
        levelcfgs = ConfigManager.GetConfigList<LevelCfg>();
        return base.Init(obj);
    }

    /// <summary>
    /// Get:
    /// ��ȡһ���ؿ��Ĵ�������
    /// </summary>
    /// <param name="chapter">�½�</param>
    /// <param name="level">�ؿ�</param>
    /// <returns></returns>
    public int[] GetLevelTrigger(int chapter, int level) 
    {
        foreach (var item in levelcfgs)
        {
            if (item.Chapter == chapter && item.Level == level)
            {
                return item.Trigger;
            }
        }
        return new int[2];
    }

    /// <summary>
    /// Check:
    /// ���ȫ���ɴ򿪵Ĺؿ�
    /// </summary>
    public List<LevelCfg> CheckLevelCompleteState(List<LevelData> datas) 
    {
        var result = new List<LevelCfg>();
        for (int i = 0; i < levelcfgs.Length; i++)
        {
            var item = levelcfgs[i];
            if (item.Trigger[0] == 0 && item.Trigger[1] == 0)
            {
                result.Add(item);
                continue;
            }
            foreach (var data in datas)
            {
                if (data.chapter == item.Trigger[0] && data.level == item.Trigger[1] && data.completed)
                    result.Add(item);
            }
        }
        return result;
    }

    

}
