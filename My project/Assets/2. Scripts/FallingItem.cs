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

        ////�������� -7���� �Ʒ��� ��������
        //if (transform.position.y < -7)
        //{
        //    // �ڱ��ڽ� ���� (������Ʈ�� �Ϻ��ϰ� �����Ϸ��� ���� ������Ʈ�� �����ؾ� ��)
        //    Destroy(gameObject);
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }


}
