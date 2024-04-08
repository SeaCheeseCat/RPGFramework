using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class KeyTipUI : UIBase
{
    public RectTransform Panel;
    private const float openPostionY = -510f;
    private const float closePostionY = -650f;

    /// <summary>
    /// Override:
    /// </summary>
    public override void OpenWithAnimation()
    {
        Panel.DOLocalMoveY(openPostionY, 0.3f);
    }

    /// <summary>
    /// Override:
    /// </summary>
    public override void CloseWithAnimation()
    {
        Panel.DOLocalMoveY(closePostionY, 0.3f).OnComplete(()=> {
            DestroyUiWithAnimatoin();
        });
    }

}
