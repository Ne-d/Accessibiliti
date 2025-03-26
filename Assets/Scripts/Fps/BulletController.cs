using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_damage;

    private bool m_shouldDie;

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * (m_speed * Time.deltaTime));

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
