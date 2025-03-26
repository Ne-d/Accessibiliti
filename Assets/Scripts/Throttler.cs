using UnityEngine;
using UnityEngine.Serialization;

public class Throttler : MonoBehaviour
{
    [SerializeField] private float m_interval;
    [SerializeField] private float m_initialDelay;
    
    private float m_startTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        m_startTime = Time.time + m_initialDelay - m_interval;
    }

    public bool IsReady()
    {
        bool isReady = Time.time >= m_startTime + m_interval;
        
        if(isReady)
            m_startTime = Time.time;
        
        return isReady;
    }
}
