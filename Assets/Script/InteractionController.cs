// InteractionController.cs

using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f; // 交互距离
    [SerializeField] private KeyCode interactionKey = KeyCode.E; // 交互按键
    [SerializeField] private LayerMask interactableLayer; // 可交互物体所在的层

    private Camera mainCamera;
    private IInteractable currentInteractable;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckForInteractable();
        HandleInteractionInput();
    }

    // 检测视线中的可交互物体
    private void CheckForInteractable()
    {
        // Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // 屏幕中心发射射线
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null && interactable.IsInteractable())
            {
                // 如果看到新的可交互物体
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.OnHoverExit();
                    }

                    currentInteractable = interactable;
                    currentInteractable.OnHover();
                }
                return;
            }
        }

        // 如果没有看到可交互物体
        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }
    }

    // 处理交互输入
    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(interactionKey) && currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }

    // 可选：在编辑器中显示交互距离
    private void OnDrawGizmosSelected()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (mainCamera != null)
        {
            Gizmos.color = Color.green;
            Vector3 rayStart = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            Gizmos.DrawLine(rayStart, rayStart + mainCamera.transform.forward * interactionRange);
        }
    }
}