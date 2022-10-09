using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManeger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알이 에너미와 닿았다면
        if (collision.gameObject.tag == "Enemy")
        {
            // 총에 맞은 에너미한테 죽으라고 전달
            collision.SendMessage("Dead");

            // 비활성화해서 재사용하도록
            gameObject.SetActive(false);
        }
    }

}
