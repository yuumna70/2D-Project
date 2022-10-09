using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    float speed;

    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        //speed = Random.Range(3f, 5f);

        rig = GetComponent<Rigidbody2D>();

        GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.3f, 1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= new Vector3(0, speed * Time.deltaTime, 0);

        ////아이템이 -7보다 아래로 내려가면
        //if (transform.position.y < -7)
        //{
        //    // 자기자신 삭제 (오브젝트를 완변하게 삭제하려면 게임 오브젝트를 삭제해야 함)
        //    Destroy(gameObject);
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
