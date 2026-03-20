using UnityEngine;
using UnityEngine.UI;
using System.Collections; // 添加此命名空间以支持协程

public class FloatingTextUI : MonoBehaviour
{
    public Text uiText;
    public string displayText = "Hello World";
    public float fadeInTime = 2f;

    void Start()
    {
        uiText.text = displayText;
        uiText.color = new Color(1, 1, 1, 0); // 初始透明
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0;
        while (elapsed < fadeInTime) // 补全括号
        {
            uiText.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, elapsed / fadeInTime));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}