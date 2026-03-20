
using UnityEngine;


public class ChairController : MonoBehaviour
{
    [Header("旋转设置")]
    public float chairRotationSpeed = 100f;
    public float cameraRotationSpeed = 100f;
    public float maxCameraAngle = 80f;
    public float minCameraAngle = -80f;
    [Tooltip("是否反转垂直视角")]
    public bool invertVertical = false;

    [Header("平滑设置")]
    public bool useSmoothing = true;
    public float rotationSmoothTime = 0.1f;

    private Transform cameraTransform;
    private float cameraVerticalRotation = 0f;
    private float currentChairRotationVelocity; // 用于平滑旋转

    void Start()
    {
        // 获取子物体中的主相机
        cameraTransform = GetComponentInChildren<Camera>().transform;

        // 锁定鼠标到屏幕中心并隐藏
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 椅子左右旋转
        if (useSmoothing)
        {
            float targetRotation = transform.eulerAngles.y + mouseX * chairRotationSpeed * Time.deltaTime;
            float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation,
                                  ref currentChairRotationVelocity, rotationSmoothTime);
            transform.eulerAngles = new Vector3(0f, smoothRotation, 0f);
        }
        else
        {
            transform.Rotate(Vector3.up, mouseX * chairRotationSpeed * Time.deltaTime);
        }

        // 相机上下旋转
        float verticalInput = invertVertical ? -mouseY : mouseY;
        cameraVerticalRotation -= verticalInput * cameraRotationSpeed * Time.deltaTime;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, minCameraAngle, maxCameraAngle);

        cameraTransform.localEulerAngles = new Vector3(cameraVerticalRotation, 0f, 0f);
    }
    void OnDestroy()
    {
        // 释放鼠标锁定
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}






