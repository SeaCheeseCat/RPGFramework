using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class LoadUI : UIBase
{
    //Tip: ����ֵ���ı���ʾ
    public Text progressText;
    //Tip: ����ֵ��ͼƬ��ʾ
    public Image progressImage;
    //Tip: ����ֵ��������ƿ��
    public float maxProgressWidthVal;
    //Tip: ƽ���ƶ�ʱ��
    private float animationDuration = 0.01f;
    /// <summary>
    /// To:
    /// ǰ��һ��Ŀ��ֵ
    /// </summary>
    /// <param name="progress"></param>
    public void ToTargetValue(float targetProgress) 
    {
        DOTween.To(() => progressImage.rectTransform.sizeDelta.x,
                   x => UpdateProgressBar(x),
                   targetProgress * maxProgressWidthVal,
                   animationDuration)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() => UpdateProgressText(targetProgress))
            .OnComplete(() => UpdateProgressText(targetProgress));
    }

    /// <summary>
    /// Update:
    /// ����ProgressBar����
    /// </summary>
    /// <param name="width"></param>
    private void UpdateProgressBar(float width)
    {
        RectTransform rt = progressImage.rectTransform;
        rt.sizeDelta = new Vector2(width, rt.sizeDelta.y);
    }
    
    /// <summary>
    /// Update:
    /// ����ProgressBar�ı���Ľ��� 
    /// </summary>
    /// <param name="targetProgress"></param>
    private void UpdateProgressText(float targetProgress)
    {
        float currentProgress = progressImage.rectTransform.sizeDelta.x / maxProgressWidthVal;
        float progress = Mathf.Lerp(currentProgress, targetProgress, progressImage.fillAmount);
        progressText.text = Mathf.RoundToInt(progress * 100) + " %";
    }
}
