using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsPlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController m_cc;
    [SerializeField] private CinemachineCamera m_camera;
    [SerializeField] private GameObject m_bulletPrefab;
    [SerializeField] private Transform m_shootTransform;
    
    private float m_moveSpeed = 10.0f;
    private float m_lookSensitivity = 0.25f;
    
    private Vector2 m_walkInput;
    private Vector2 m_lookInput;

    private Vector2 m_viewDirection;
    
    public void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            Vector3 walkDirection = new Vector3(m_walkInput.x, 0, m_walkInput.y);
            m_cc.Move(transform.rotation * walkDirection * (m_moveSpeed * (GameManager.Instance.PlayerSpeed / 100.0f) * Time.deltaTime));

            transform.rotation = Quaternion.Euler(0, m_viewDirection.x, 0);
            m_camera.transform.localRotation = Quaternion.Euler(-m_viewDirection.y, 0, 0);
        }
    }
    
    [UsedImplicitly]
    public void OnWalk(InputValue value)
    {
        m_walkInput = value.Get<Vector2>();
    }
    
    [UsedImplicitly]
    public void OnLook(InputValue value)
    {
        if (GameManager.Instance.IsPlaying)
        {
            m_lookInput = value.Get<Vector2>();
            m_viewDirection += m_lookInput * m_lookSensitivity * GameManager.Instance.FpsMouseSensitivity;
        }
    }

    [UsedImplicitly]
    private void OnShoot()
    {
        Instantiate(m_bulletPrefab, m_shootTransform.position, m_camera.transform.rotation);
    }
}
