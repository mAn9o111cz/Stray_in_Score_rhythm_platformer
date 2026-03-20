using UnityEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ChargeCollectSystem : MonoBehaviour
{
    [Header("收集设置")]
    public float maxCharge = 1f; // 最大蓄力值
    public float currentCharge = 0f; // 当前蓄力值
    public float collectSpeed = 10f; // 收集速度

    [Header("UI设置")]
    public Image chargeSlider; // 头顶进度条

    [Header("相机效果")]
    public Camera mainCamera;
    public float shakeIntensity = 0.1f; // 震动强度
    public float shakeDuration = 0.2f; // 震动持续时间
    public float zoomAmount = 0.5f; // 缩放量
    public float zoomSpeed = 5f; // 缩放速度

    private float originalSize; // 相机原始大小
    private Vector3 originalPosition; // 相机原始位置
    private bool isShaking = false;
    private bool isPlayerEnter = false;
    private float shakeTimer = 0f;

    public UnityEvent OnCollected;

    private void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        originalSize = mainCamera.orthographicSize;
        originalPosition = mainCamera.transform.position;

        // 初始化UI
        chargeSlider.fillAmount = 0;
    }

    private void Update()
    {

        // 相机震动处理
        if (isShaking)
        {
            // 随机震动偏移
            Vector2 shakeOffset = Random.insideUnitCircle * shakeIntensity;
            mainCamera.transform.position = originalPosition + new Vector3(shakeOffset.x, shakeOffset.y, 0);
        }

        // 相机缩放恢复
        if (mainCamera.orthographicSize < originalSize)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalSize, Time.deltaTime * zoomSpeed);
        }

        if (isPlayerEnter && Input.GetKey(KeyCode.E))
        {
            Time.timeScale = 0.5f;
            isShaking = true;
            currentCharge += Mathf.Clamp(currentCharge + Time.deltaTime, 0, 1);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            ResetAll();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerEnter = false;
        }
    }

    private System.Collections.IEnumerator CollectItem(GameObject item)
    {
        Time.timeScale = 0.5f;

        // 收集效果
        StartCameraShake();
        ZoomCamera();

        // 增加蓄力值
        currentCharge += collectSpeed;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        chargeSlider.fillAmount = currentCharge;

        // 这里可以添加收集物品的其他效果，如粒子效果、音效等
        item.SetActive(false); // 暂时简单禁用物品

        yield return null;
    }

    private void StartCameraShake()
    {
        if (!isShaking)
        {
            originalPosition = mainCamera.transform.position;
        }

        isShaking = true;
        shakeTimer = shakeDuration;
    }

    private void ZoomCamera()
    {
        mainCamera.orthographicSize = originalSize - zoomAmount;
    }

    private void ResetAll()
    {
        isPlayerEnter = false;
        isShaking = false;
        mainCamera.orthographicSize = originalSize;
        mainCamera.transform.position = originalPosition;
        currentCharge = 0;
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        OnCollected?.Invoke();
    }

}
