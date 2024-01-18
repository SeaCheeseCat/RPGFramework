using UnityEngine.UI;
using UnityEngine;
public class GroundFilmUI : UIBase
{
    public FillItem[] FillItems;
    public override void Awake()
    {
        base.Awake();
        Init();
    }

    /// <summary>
    /// Init:
    /// 初始化界面
    /// </summary>
    private void Init() 
    {
        for (int i = 0; i < FillItems.Length; i++)
        {
            var index = i;
            var item = FillItems[i];
            item.Btn.onClick.AddListener(() => { FillItemOnClick(index); });
        }
    }

    /// <summary>
    /// Callback:
    /// 胶卷被点击
    /// </summary>
    /// <param name="index">点击的下标</param>
    public void FillItemOnClick(int index)
    {
        var item = FillItems[index];
        UIManager.Instance.OpenUI<TestSaveUI>();

    }
      
}
