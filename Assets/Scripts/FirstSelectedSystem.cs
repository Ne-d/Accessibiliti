using UnityEngine;
using UnityEngine.EventSystems;

public class FirstSelectedSystem : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

    public void SetSelected(GameObject go)
    {
        if (_eventSystem != GameManager.Instance.m_eventSystem && GameManager.Instance.m_eventSystem != null)
        {
            GameManager.Instance.m_eventSystem.SetSelectedGameObject(go);
            Destroy(gameObject);
        }
        else
            _eventSystem.SetSelectedGameObject(go);
    }
}
