using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetEditor
{
    [Title("CheeseFramework","��ӭʹ�ÿ�ܣ��ȸ��Լ��������һ�����������,����������.jpg")]
    [LabelText("��Ϸ����"), BoxGroup("Tool")]
    public string Name;
    [LabelText("Ĭ������"), BoxGroup("Tool")]
    public Language language = Language.Chinese;

    [Button("��������",ButtonSizes.Large),GUIColor(35f/225f, 219f/225f, 66f/225f)]
    public void BuildConfig() 
    { 
        
    }


}
