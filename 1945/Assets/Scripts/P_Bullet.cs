using UnityEngine;

public class P_Bullet : MonoBehaviour
{
    public float bulletSpeed = 4f;

    // 공격력
    public int Attack = 10;
    // 이펙트

    void Start()
    {
        
    }

    void Update()
    {
        // 미사일 위쪽 방향으로 움직이기
        transform.Translate(Vector3.up * bulletSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Monster>().OnDamaged(Attack);
        }
    }
}
