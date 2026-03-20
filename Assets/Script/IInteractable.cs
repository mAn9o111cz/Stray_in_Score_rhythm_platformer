// IInteractable.cs
public interface IInteractable
{
    // 当玩家看向物体时调用
    void OnHover();

    // 当玩家移开视线时调用
    void OnHoverExit();

    // 当玩家与物体交互时调用
    void OnInteract();

    // 返回物体是否可以被交互
    bool IsInteractable();
}
