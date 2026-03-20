using UnityEngine;

public class PlatformAttachment : MonoBehaviour
{
    public KeyCode attachKey = KeyCode.Q;
    public LayerMask platformLayer; // ����ƽ̨��Layer

    private Rigidbody2D rb;
    private Collider2D currentPlatform;
    private bool isAttached = false;
    private Vector3 attachedOffset; // �洢��ƽ̨�����λ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(attachKey) && currentPlatform != null)
        {
            if (!isAttached)
            {
                // ���ŵ�ƽ̨
                AttachToPlatform();
            }
            else
            {
                // ��ƽ̨����
                DetachFromPlatform();
            }
        }

        // ����Ѹ��ţ�����ƽ̨�ƶ�
        if (isAttached && currentPlatform != null)
        {
            transform.position = currentPlatform.transform.position + attachedOffset;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �����ײ�����Ƿ���ƽ̨
        if (((1 << collision.gameObject.layer) & platformLayer) != 0)
        {
            currentPlatform = collision.collider;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // �뿪ƽ̨ʱ�������ǰƽ̨���뿪��ƽ̨�����������
        if (currentPlatform == collision.collider && !isAttached)
        {
            currentPlatform = null;
        }
    }

    void AttachToPlatform()
    {
        isAttached = true;
        rb.gravityScale = 0; // ��������
        rb.linearVelocity = Vector2.zero; // ����ٶ�
        rb.isKinematic = true; // ����Ϊ�˶�ѧ����
        attachedOffset = transform.position - currentPlatform.transform.position; // �������λ��
    }

    void DetachFromPlatform()
    {
        isAttached = false;
        rb.isKinematic = false; // �ָ�Ϊ��̬����
        rb.gravityScale = 1; // �ָ�����
        currentPlatform = null; // ���ƽ̨����
    }
}