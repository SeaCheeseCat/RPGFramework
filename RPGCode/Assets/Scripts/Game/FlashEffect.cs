using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public float flashDuration = 0.1f; // 闪光持续时间
    public float flashIntensity = 1.5f; // 闪光强度

    private Image flashImage;
    private Color originalColor;

    void Start()
    {
        flashImage = GetComponent<Image>();
        originalColor = flashImage.color;
    }

    public void Flash()
    {
        // 启动协程来处理闪光效果
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        // 设置闪光的颜色和强度
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, flashIntensity);

        // 等待一段时间
        yield return new WaitForSeconds(flashDuration);

        // 还原闪光的颜色
        flashImage.color = originalColor;
    }
}
