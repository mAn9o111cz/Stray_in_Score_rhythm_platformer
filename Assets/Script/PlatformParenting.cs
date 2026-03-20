using UnityEngine;

public class PlatformParenting : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 检查是否从上方接触
            if (IsFromAbove(collision))
            {
                // 将玩家设为平台的子对象
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 将玩家恢复为世界层级
            collision.transform.SetParent(null);

            // 保持玩家在世界空间中的缩放
            collision.transform.localScale = Vector3.one;
        }
    }

    private bool IsFromAbove(Collision2D collision)
    {
        // 获取接触点法线
        ContactPoint2D contact = collision.GetContact(0);

        // 如果接触点法线朝上(大于0.7)，则认为是从上方接触
        return contact.normal.y > 0.7f;
    }
}