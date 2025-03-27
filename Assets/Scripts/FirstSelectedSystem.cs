using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedSystem : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    public void SetSelected(GameObject go)
    {
        _eventSystem.SetSelectedGameObject(go);
    }
}
