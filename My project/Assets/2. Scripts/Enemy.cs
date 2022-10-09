using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    Rigidbody2D rig;

    int dir;

    SpriteRenderer sr;

    Animator anim;

    bool isChange;

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        RandomDir();


    }

    // Update is called once per frame
    private void Update()
    {
        anim.SetInteger("speed", dir);

        // 오른족으로 이동하고 있다면
        if (dir > 0)
        {
            sr.flipX = true;
        }

        // 왼족으로 이동하고 있다면
        else if (dir < 0)
        {
            sr.flipX = false;
        }

        // raycase : 충돌을 감지

        // dir이 0이 아닐 때만
        if (dir != 0)
        {
            // 레이 기즈모 그리기
            Debug.DrawRay(transform.position + new Vector3(dir * 0.6f, 0, 0), Vector2.down * 1);

            // 레이를 발사해서 맞은 물체 저장 (에너미보다 더 앞에서 발사)
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(dir * 0.6f, 0, 0), Vector2.down, 1);

            // 맞은 물체가 없다면 (= 바닥이 없다면) + 방향을 바꾸기 전이라면
            if (!hit && !isChange)
            {
                // print(hit.collider.gameObject.name);

                // 방향 바꾸기
                dir = dir * -1;   // dir *= -1;    ==    dir = -dir;

                // 인보크 취소
                CancelInvoke("RandomDir");

                // 이미 방향을 바꿨음을 저장
                isChange = true;

                // 랜덤한 주기로 반복해서 방향 결정
                Invoke("RandomDir", Random.Range(2f, 3f));
            }

            // 맞은 물체가 있다면 (= 방향을 바꿔서 땅에 닿았다면)
            else if (hit)
            {
                // 다시 방향을 바꿀 수 있도록 초기화
                isChange = false;
            }

        }

    }

    float t;

    // Update is called once per frame
    void FixedUpdate()
    {
        // 죽었다면
        if (isDead)
        {
            t += Time.deltaTime;

            // 서서히 사라지도록
            sr.color = Vector4.Lerp(Color.white, new Vector4(1, 1, 1, 0), t / 2);

            // 완전히 투명해지면
            if (sr.color.a == 0)
            {
                // 자기자신 삭제
                Destroy(gameObject);
            }

            // 함수 탈출
            return;
        }

        // 랜덤한 방향으로 이동
        rig.velocity = new Vector2(dir, rig.velocity.y);
    }

    // 랜덤으로 방향 결정
    void RandomDir()
    {
        // 랜덤으로 방향 결정
        dir = Random.Range(-1, 2);

        // 랜덤한 주기로 반복해서 방향 결정
        Invoke("RandomDir", Random.Range(2f, 3f));
    }

    // 에너미가 죽으면 호출
    void Dead()
    {
        // 죽은 애니메이션으로 전환
        anim.SetTrigger("dead");

        // 이동 중단 (= 죽었음을 저장)
        isDead = true;

        // 닿아도 죽지 않게 (콜라이더를 비활성화)
        GetComponent<BoxCollider2D>().enabled = false;

        // 리지드바디도 비활성화
        rig.simulated = false;

        // 방향 바꾸는 인보크 취고
        CancelInvoke("RandomDir");
    }

}
