using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private GameObject m_visual;
    private Camera m_camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        m_agent.SetDestination(m_camera.transform.position);
        
        m_visual.transform.LookAt(m_camera.transform.position);
        m_visual.transform.rotation = Quaternion.Euler(0, m_visual.transform.rotation.eulerAngles.y, 0);
    }
}
