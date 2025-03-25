using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent;
    [SerializeField] private GameObject m_mesh;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_agent.SetDestination(Camera.main.transform.position);
        
        m_mesh.transform.LookAt(Camera.main.transform.position);
        m_mesh.transform.rotation = Quaternion.Euler(0, m_mesh.transform.rotation.eulerAngles.y, 0);
    }
}
