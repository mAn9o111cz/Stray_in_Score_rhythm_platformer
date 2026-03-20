using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    [Header("颜色设置")]
    public Color targetColor = Color.red; // 可以设置为任意你想要的颜色

    [Header("目标父物体")]
    public Transform parentObject; // 拖拽包含所有要改变颜色的子物体的父物体到这里

    private bool hasTriggered = false; // 确保只触发一次

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        // 检查是否是玩家触发的
        if (other.CompareTag("Player"))
        {
            ChangeChildrenColor();
            hasTriggered = true;
        }
    }*/

    private void Start()
    {
        parentObject = transform;
    }

    public void ChangeChildrenColor()
    {
        if (parentObject == null)
        {
            Debug.LogWarning("没有指定父物体!");
            return;
        }

        // 遍历所有子物体
        foreach (Transform child in parentObject)
        {
            // 获取子物体的SpriteRenderer组件
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = targetColor;
            }

            // 如果有子物体的子物体也要改变，可以取消下面的注释
            // ChangeColorRecursively(child);
        }

        Debug.Log($"已改变 {parentObject.childCount} 个子物体的颜色");
    }

    // 如果需要递归改变所有层级的子物体，可以使用这个方法
    private void ChangeColorRecursively(Transform parent)
    {
        foreach (Transform child in parent)
        {
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = targetColor;
            }

            // 递归调用
            if (child.childCount > 0)
            {
                ChangeColorRecursively(child);
            }
        }
    }
}