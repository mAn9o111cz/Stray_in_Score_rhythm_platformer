using UnityEngine;

public class DestroyPoint : MonoBehaviour
{
    private Rigidbody2D rb;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("glass"))
        {
            Destroy(collision.gameObject);
        }
    }
}