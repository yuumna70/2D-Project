using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트시스템을 사용하기 위함


public class PlayerFire : MonoBehaviour
{
    // 총 발사하는 힘
    public float firePower;

    SpriteRenderer sr;

    // 방향을 결정하는 변수
    int dir;

    // 총알 개수
    public int bulletCount = 5;

    // 총알 오브젝트 풀 (=탄창)
    public GameObject[] bulletPool;

    // 총 효과음 종류
    public AudioClip[] clips;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        audio = transform.GetChild(0).GetComponent<AudioSource>();

        // 1. 탄창에 들어갈 총알 개수 결정
        bulletPool = new GameObject[bulletCount];

        // 2. 탄창에 들어갈 총알 개수만큼 반복해서
        for (int i = 0; i < bulletCount; i++)
        {
            // 3, 총알 복제해서 생성
            GameObject bullet = Instantiate(Resources.Load("Bullet")) as GameObject;

            // 4. 복제한 총알 탄창에 넣기
            bulletPool[i] = bullet;

            // 5. 일단 비활성화
            bullet.SetActive(false);
        }




    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭 했다면 + UI 위에 커서가 있는게 아니라면 + 설정 화면이 열려있지 않다면
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0)
        {
            // 랜덤으로 어떤 총 효과음을 낼지 결정
            audio.clip = clips[Random.Range(0, 2)];

            // 총알 효과음 실행
            audio.Play();

            // 오른쪽을 보고 있다면
            if (!sr.flipX)
            {
                // 방향을 오른쪽으로
                dir = 1;
            }
            // 왼쪽을 보고 있다면
            else
            {
                // 방향을 왼쪽으로
                dir = -1;
            }


            // 플레이어의 위치보다 조금 앞/아래에 총알 생성 후 저장
            // GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + new Vector3(dir * 0.6f , -0.5f, 0), Quaternion.identity) as GameObject;

            // 총알 개수만큼 반복
            for (int i = 0; i < bulletCount; i++)
            {
                // i번째 총알 저장
                GameObject bullet = bulletPool[i];

                // 발사하기 전 총알이라면 (=비활성화 상태라면)
                if (!bullet.activeSelf)
                {


                    // 발사 시작할 위치 설정
                    bullet.transform.position = transform.position + new Vector3(dir * 0.6f, -0.5f, 0);

                    // 발사하기 전 활성화
                    bullet.SetActive(true);

                    // 총알을 바라보는 방향으로 발사
                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * firePower, ForceMode2D.Impulse);



                    // 총알 재사용
                    StartCoroutine(ResetBuulet(bullet));

                    break;
                }


            }


        }
    }

    // 총알 재사용
    IEnumerator ResetBuulet(GameObject bullet)
    {

        // 2초 쉬고
        yield return new WaitForSeconds(2);

        // 총알 비활성화
        bullet.SetActive(false);
    }

}
