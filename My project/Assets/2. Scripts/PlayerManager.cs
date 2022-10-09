using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public float maxSpeed;

    public GameManager gm;

    public float jumpPower;

    public List<Transform> respawnPoints = new List<Transform>();

    Transform realRespawnPoint;

    int life = 3;

    Rigidbody2D rig;

    float h;

    int jumpCount;

    Animator anim;

    SpriteRenderer sr;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>();

        audio = GetComponent<AudioSource>();

        realRespawnPoint = respawnPoints[0];

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && jumpCount < 2)   // && isGrount  1단 점프
        {
            // 순간적으로 위로 힘을 가하기 
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);    // Force = 지속적으로 가해짐  / Impulse = 순간적

            // 점프 애니메이션으로 전환
            anim.SetBool("isJump", true);

            audio.Play();

            // 점프한 횟수 증가
            jumpCount++;
        }

        if (gm.isPlay == false)  // gm.isPlay == false   =     !gm.isPlay
        {
            // 함수 탈출
            return;
        }

        // 좌우로 이동하는 키를 숫자로 받아와서 저장
        h = Input.GetAxisRaw("Horizontal");  // * speed * Time.deltaTime

        if (Input.GetButtonUp("Horizontal"))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

        anim.SetInteger("speed", (int)rig.velocity.x);

        if (h > 0)
        {
            sr.flipX = false;
        }
        else if (h < 0)
        {
            sr.flipX = true;
        }

        // 상하로 이동하는 키를 숫자로 받아와서 저장
        // float v = Input.GetAxis("Vertical");  // * speed * Time.deltaTime

        // 받아온 값을 이용해서 상하좌우이동 (모든 기기에서 같은 속도로)
        //transform.position += new Vector3(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);   // (h, v, 0) * * speed * Time.deltaTime;
        // transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);
    }


    private void FixedUpdate()
    {
        rig.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        float xClamp = Mathf.Clamp(rig.velocity.x, -maxSpeed, maxSpeed);

        rig.velocity = new Vector2(xClamp, rig.velocity.y);


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "BadItem":
                life--;

                life = Mathf.Clamp(life, 0, 3);

                gm.PlayerLife(life);
                break;

            case "GoodItem":
                life++;

                life = Mathf.Clamp(life, 0, 3);

                gm.PlayerLife(life);
                break;

            case "DeadZone":
            case "Enemy":
                life--;

                life = Mathf.Clamp(life, 0, 3);

                gm.PlayerLife(life);

                transform.position = realRespawnPoint.position;
                break;

            case "Ground":
                jumpCount = 0;

                anim.SetBool("isJump", false);
                break;

        }





    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SavePoint")
        {
            string number = collision.gameObject.name.Split('_')[1];

            int index = int.Parse(number);

            if (index > respawnPoints.IndexOf(realRespawnPoint))
            {
                realRespawnPoint = respawnPoints[index];
            }
        }

            if (collision.gameObject.tag == "EndingPoint")
            {
                gm.Ending("게임 성공!");
            }
        }


    
}
