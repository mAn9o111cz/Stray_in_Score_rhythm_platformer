using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float speed;
    private Rigidbody2D rb;

    public float step = 2f;

    void OnEnable()
    {
        bpmRhythm.onBeat += OnBeat;
    }

    void OnDisable()
    {
        bpmRhythm.onBeat -= OnBeat;
    }

    public void Initialize(Vector2 shootDirection, float bulletSpeed)
    {
        direction = shootDirection;
        speed = bulletSpeed;
        rb = GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnBeat()
    {
        DOTween.Complete(transform);
        transform.DOMove(transform.position + (Vector3)direction * step, 0.1f);
    }

    //void FixedUpdate()
    //{
    //    rb.linearVelocity = direction * speed;
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Ground")|| collision.CompareTag("glass"))
        {
            Destroy(gameObject);

            if (collision.CompareTag("Player"))
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.Die();
                }
            }

            if (collision.CompareTag("glass"))
            {
                Destroy(collision.gameObject);
            }

        }
    }
}