using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private GameObject m_visual;
    [SerializeField] private float m_startingHealth = 100.0f;
    
    private Camera m_camera;
    private float m_health;
    private bool m_shouldDie;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_camera = Camera.main;
        m_health = m_startingHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        m_agent.SetDestination(m_camera.transform.position);
        
        m_visual.transform.LookAt(m_camera.transform.position);
        m_visual.transform.rotation = Quaternion.Euler(0, m_visual.transform.rotation.eulerAngles.y, 0);

        if (m_shouldDie)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        if (m_health <= 0)
            m_shouldDie = true;
    }
}
