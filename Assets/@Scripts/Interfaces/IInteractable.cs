using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// 상호작용 가능한 오브젝트에 구현
    /// </summary>
    /// <param name="target">상호작용 주체</param>
    void Interact(MonoBehaviour target);
}