using UnityEngine;

public class Monster : MonoBehaviour
{
    public int HP = 100;
    public float Speed = 1f;
    public float Delay = 1f;
    public Transform ms1;
    public Transform ms2;
    public GameObject bullet;

    // 아이템 가져오기
    [SerializeField] private GameObject Item;
    [SerializeField] private GameObject Effect;
    

    void Start()
    {
        Invoke("CreateBullet", Delay);
    }

    void CreateBullet()
    {
        Instantiate(bullet, ms1.position, Quaternion.identity);
        Instantiate(bullet, ms2.position, Quaternion.identity);

        //재귀
        Invoke("CreateBullet", Delay);
    }

    void Update()
    {
        // 아래 방향으로 움직임
        transform.Translate(Vector3.down * Speed * Time.deltaTime);
    }

    // 미사일에 데미지 입은 함수
    public void OnDamaged(int attack)
    {
        HP -= attack;

        GameObject go = Instantiate(Effect, transform.position, Quaternion.identity);
        Destroy(go, 1f);

        if (HP <= 0)
        {
            ItemDrop();

            Destroy(gameObject);
        }
    }

    public void ItemDrop()
    {
        Instantiate(Item, transform.position, Quaternion.identity);
    }
}
