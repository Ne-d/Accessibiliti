using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_spawnedPrefab;
    [SerializeField] private Throttler m_throttler;
    
    // Update is called once per frame
    private void Update()
    {
        if (m_throttler.IsReady())
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        Instantiate(m_spawnedPrefab, transform.position, Quaternion.identity);
    }
}
