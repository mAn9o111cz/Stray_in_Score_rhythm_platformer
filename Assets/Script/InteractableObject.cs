using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "按E交互";

    public UnityEvent OnHoverEvent;
    public UnityEvent OnExitEvent;
    public UnityEvent OnInteractEvent;

    private void Start()
    {

    }

    public void OnHover()
    {
        Debug.Log("看向物体: " + gameObject.name);
        // 显示UI提示
        InteractUI.Instance.ShowText(interactText);
        OnHoverEvent?.Invoke();
    }

    public void OnHoverExit()
    {
        Debug.Log("移开视线: " + gameObject.name);
        // 隐藏UI提示
        InteractUI.Instance.HideText();
        OnExitEvent?.Invoke();
    }

    public void OnInteract()
    {
        OnInteractEvent?.Invoke();
        Debug.Log("与物体交互: " + gameObject.name);
    }

    public bool IsInteractable()
    {
        // 可以在这里添加条件判断，比如是否解锁、是否有能量等
        return true;
    }
}



