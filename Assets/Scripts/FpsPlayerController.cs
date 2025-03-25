using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsPlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController m_cc;

    private float m_moveSpeed = 10.0f;
    private float m_lookSensitivity = 0.25f;
    
    private Vector2 m_walkInput;
    private float m_lookInput;

    private float m_rotation;
    
    [UsedImplicitly]
    public void OnWalk(InputValue value)
    {
        m_walkInput = value.Get<Vector2>();
    }
    
    public void OnLook(InputValue value)
    {
        m_lookInput = value.Get<float>();
        m_rotation += m_lookInput * m_lookSensitivity;
    }

    public void Start()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
    }

    public void Update()
    {
        Vector3 walkDirection = new Vector3(m_walkInput.x, 0, m_walkInput.y);
        m_cc.Move(transform.rotation * walkDirection * (m_moveSpeed * Time.deltaTime));
        
        transform.rotation = Quaternion.Euler(0, m_rotation, 0);
    }
}
