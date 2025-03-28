using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedSystem : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    private void Awake()
    {
        if (_eventSystem != GameManager.Instance.m_eventSystem && GameManager.Instance.m_eventSystem != null)
        {
            Destroy(gameObject);
        }
    }

    public void SetSelected(GameObject go)
    {
        _eventSystem.SetSelectedGameObject(go);
    }
}
