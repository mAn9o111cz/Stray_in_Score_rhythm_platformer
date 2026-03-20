using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
public class BounceEffect : MonoBehaviour
{

    public float bounceForceUp = 10f;    // ���ϵĵ�����
    public float bounceForceForward = 5f; // ��ǰ�ĵ�����
    public float torqueForce = 2f;       // ��תŤ�أ���ѡЧ����

 
    public ParticleSystem bounceParticles; // ��������Ч��
    public AudioClip bounceSound;         // ������Ч

    public UnityEvent OnBounce;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // ȷ�������"Player"��ǩ
        {
            BouncePlayer(other.GetComponent<Rigidbody2D>());
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player")) // ȷ�������"Player"��ǩ
        {
            BouncePlayer(collision.gameObject.GetComponent<Rigidbody2D>());
            OnBounce?.Invoke();
        }
    }

    private void BouncePlayer(Rigidbody2D playerRb)
    {
        if (playerRb != null)
        {
            // ���ô�ֱ�ٶ��Ա����������
            //playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 0);

            // ���㵯����������+����泯����
            //int facingDirection = transform.localScale.x > 0 ? 1 : -1;
            //Vector2 bounceDirection = new Vector2(facingDirection * bounceForceForward, bounceForceUp);

            // ʩ����
            playerRb.AddForce(Vector2.up * bounceForceUp, ForceMode2D.Impulse);

            // ��ѡ��������תЧ��
            //playerRb.AddTorque(torqueForce * -facingDirection, ForceMode2D.Impulse);

            // ����Ч��
            PlayBounceEffects();

            DOTween.CompleteAll();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.1f);
        }
    }

    private void PlayBounceEffects()
    {
        // ����Ч��
        if (bounceParticles != null)
        {
            bounceParticles.Play();
        }

        // ��Ч
        if (bounceSound != null)
        {
            AudioSource.PlayClipAtPoint(bounceSound, transform.position);
        }
    }
}