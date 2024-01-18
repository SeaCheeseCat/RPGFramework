using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashEffect : MonoBehaviour
{
    public float flashDuration = 0.1f; // �������ʱ��
    public float flashIntensity = 1.5f; // ����ǿ��

    private Image flashImage;
    private Color originalColor;

    void Start()
    {
        flashImage = GetComponent<Image>();
        originalColor = flashImage.color;
    }

    public void Flash()
    {
        // ����Э������������Ч��
        StartCoroutine(FlashCoroutine());
    }

    IEnumerator FlashCoroutine()
    {
        // �����������ɫ��ǿ��
        flashImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, flashIntensity);

        // �ȴ�һ��ʱ��
        yield return new WaitForSeconds(flashDuration);

        // ��ԭ�������ɫ
        flashImage.color = originalColor;
    }
}
