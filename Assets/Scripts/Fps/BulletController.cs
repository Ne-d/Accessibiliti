using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_damage;
    [SerializeField] private float m_lifeTime;

    private bool m_shouldDie;
    private float m_spawnTime;

    private void Start()
    {
        m_spawnTime = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (m_speed * Time.deltaTime));

        if (Time.time > m_spawnTime + m_lifeTime)
            m_shouldDie = true;

        if (m_shouldDie)
            Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            enemy.TakeDamage(m_damage);
        }

        m_shouldDie = true;
    }
}
