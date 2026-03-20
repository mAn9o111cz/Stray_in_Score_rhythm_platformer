using UnityEngine;
using System;
public class SpikeDamage : MonoBehaviour
{
    [Tooltip("尖刺顶部判定区域的高度")]
    public float topDamageHeight = 0.2f;
    private PolygonCollider2D spikeCollider;
    private Vector3 respawnPosition; 
    void Start()
    {
        spikeCollider = GetComponent<PolygonCollider2D>();
        respawnPosition = transform.position;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 获取碰撞接触点
            //ContactPoint2D contact = collision.GetContact(0);

            // 计算接触点相对于尖刺的位置
            //Vector2 localContact = transform.InverseTransformPoint(contact.point);

            // 获取尖刺碰撞器的顶点
            //Vector2[] points = spikeCollider.points;

            // 找出尖刺的最高点
            //float spikeTop = Mathf.Max(points[0].y, points[1].y, points[2].y);

            // 如果接触点在尖刺顶部区域内
            /*if (localContact.y > spikeTop - topDamageHeight)
            {
                
            }*/
            // 玩家死亡
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
        }
    }



    public void Respawn()
    {
        // 重置位置到重生点
        transform.position = respawnPosition;

        // 重新启用玩家渲染和碰撞
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = true;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;
    }

    // 设置新的重生点
    public void SetRespawnPosition(Vector3 newPosition)
    {
        respawnPosition = newPosition;
    }
}






