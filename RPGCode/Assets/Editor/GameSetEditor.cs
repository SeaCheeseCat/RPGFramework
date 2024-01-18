using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetEditor
{
    [Title("CheeseFramework","欢迎使用框架！先给自己打个气！一定可以做完的,阴暗地爬行.jpg")]
    [LabelText("游戏名称"), BoxGroup("Tool")]
    public string Name;
    [LabelText("默认语言"), BoxGroup("Tool")]
    public Language language = Language.Chinese;

    [Button("生成配置",ButtonSizes.Large),GUIColor(35f/225f, 219f/225f, 66f/225f)]
    public void BuildConfig() 
    { 
        
    }


}
