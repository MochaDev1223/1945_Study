using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject[] bullet; // 미사일 프리팹
    public Transform pos = null;

    public int power = 0;
    [SerializeField]
    private GameObject powerUp; // 파워업 아이템 프리팹

    Animator ani; //애니메이터 컴포넌트

    void Start()
    {
        ani = GetComponent<Animator>();
    }


    void Update()
    {
        //이동
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        //-1 0 1
        if (Input.GetAxis("Horizontal") <= -0.5f)
        {
            ani.SetBool("left", true);
        }
        else
        {
            ani.SetBool("left", false);
        }

        if (Input.GetAxis("Horizontal") >= 0.5f)
        {
            ani.SetBool("right", true);
        }
        else
        {
            ani.SetBool("right", false);
        }


        if (Input.GetAxis("Vertical") >= 0.5f)
        {
            ani.SetBool("up", true);
        }
        else
        {
            ani.SetBool("up", false);
        }

        transform.Translate(moveX, moveY, 0);

        // 화면 밖으로 나가지 않도록 월드 좌표를 뷰포트 좌표로 변환합니다.
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        // 뷰포트 x 값을 0~1 범위로 클램프합니다.
        viewPos.x = Mathf.Clamp01(viewPos.x);
        // 뷰포트 y 값을 0~1 범위로 클램프합니다.
        viewPos.y = Mathf.Clamp01(viewPos.y);
        // 클램프된 뷰포트 좌표를 다시 월드 좌표로 변환하여 적용합니다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos; // 위치 갱신

        // 미사일 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet[power], pos.position, Quaternion.identity);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {

            if (power >= 3)
            {
                power = 3;
            }
            else
            {
                power += 1;
                // 파워업 UI 생성 후 삭제
                GameObject go = Instantiate(powerUp, transform.position, Quaternion.identity);
                Destroy(go, 1);
            }



            Destroy(collision.gameObject);
        }
    }
}
