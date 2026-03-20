using System;
using UnityEngine;
using UnityEngine.UI;
//蓄力时加镜头抖动；完成角色设计；完成ui与拾取动作绑定
public class PickUpHelper : MonoBehaviour
{

    [Header("Pickup")]
    public float pickupTime = 3f;
    public GameObject pickupEffect;

    private Rigidbody2D rb;
    private bool isPickingUp = false;
    private float pickupTimer = 0f;
    private GameObject currentPickupTarget;
    private Vector2 originalGravity;
    private float rbGravity;

    public Camera mainCamera;
    private float originalSize; // 相机原始大小
    private Vector3 originalPosition; // 相机原始位置

    public Image progress;

    public static Action onCollocted;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = Physics2D.gravity;
        rbGravity = rb.gravityScale;

        originalSize = mainCamera.orthographicSize;
        originalPosition = mainCamera.transform.localPosition;
    }

    void Update()
    {

        // ʰȡ�߼�
        if (Input.GetKey(KeyCode.E) && currentPickupTarget != null && !isPickingUp)
        {
            StartPickup();
        }
        else if (!Input.GetKey(KeyCode.E) && isPickingUp)
        {
            CancelPickup();
        }

        if (isPickingUp)
        {
            pickupTimer += Time.deltaTime;
            if (progress != null)
            {
                progress.fillAmount = pickupTimer / pickupTime;
            }

            // ������Ҿ�ֹ
            rb.linearVelocity = Vector2.zero;

            if (pickupTimer >= pickupTime)
            {
                CompletePickup();
            }

            Vector2 shakeOffset = UnityEngine.Random.insideUnitCircle * 0.1f;
            mainCamera.transform.localPosition = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);
            mainCamera.orthographicSize = mainCamera.orthographicSize - Time.deltaTime;
        }
    }

    void StartPickup()
    {
        isPickingUp = true;
        pickupTimer = 0f;
        Physics2D.gravity = Vector2.zero; // ȡ������
        rb.gravityScale = 0; // ȷ����Ҳ�������Ӱ��
    }

    void CancelPickup()
    {
        isPickingUp = false;
        Physics2D.gravity = originalGravity; // �ָ�����
        rb.gravityScale = rbGravity;
        ResetAll();
    }

    void CompletePickup()
    {
        // ����������Ч
        if (pickupEffect != null)
        {
            Instantiate(pickupEffect, currentPickupTarget.transform.position, Quaternion.identity);
        }

        // ���ٱ�ʰȡ������
        Destroy(currentPickupTarget);

        onCollocted?.Invoke();

        // �ָ�״̬
        isPickingUp = false;
        Physics2D.gravity = originalGravity;
        rb.gravityScale = rbGravity;
        currentPickupTarget = null;
        progress.fillAmount = 0;
        ResetAll();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickUp"))
        {
            currentPickupTarget = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == currentPickupTarget)
        {
            if (isPickingUp)
            {
                CancelPickup();
            }
            currentPickupTarget = null;
        }
    }

    void ResetAll()
    {
        mainCamera.orthographicSize = originalSize;
        mainCamera.transform.localPosition = originalPosition;
    }
}