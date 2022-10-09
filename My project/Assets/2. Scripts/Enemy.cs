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

        // ���������� �̵��ϰ� �ִٸ�
        if (dir > 0)
        {
            sr.flipX = true;
        }

        // �������� �̵��ϰ� �ִٸ�
        else if (dir < 0)
        {
            sr.flipX = false;
        }

        // raycase : �浹�� ����

        // dir�� 0�� �ƴ� ����
        if (dir != 0)
        {
            // ���� ����� �׸���
            Debug.DrawRay(transform.position + new Vector3(dir * 0.6f, 0, 0), Vector2.down * 1);

            // ���̸� �߻��ؼ� ���� ��ü ���� (���ʹ̺��� �� �տ��� �߻�)
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(dir * 0.6f, 0, 0), Vector2.down, 1);

            // ���� ��ü�� ���ٸ� (= �ٴ��� ���ٸ�) + ������ �ٲٱ� ���̶��
            if (!hit && !isChange)
            {
                // print(hit.collider.gameObject.name);

                // ���� �ٲٱ�
                dir = dir * -1;   // dir *= -1;    ==    dir = -dir;

                // �κ�ũ ���
                CancelInvoke("RandomDir");

                // �̹� ������ �ٲ����� ����
                isChange = true;

                // ������ �ֱ�� �ݺ��ؼ� ���� ����
                Invoke("RandomDir", Random.Range(2f, 3f));
            }

            // ���� ��ü�� �ִٸ� (= ������ �ٲ㼭 ���� ��Ҵٸ�)
            else if (hit)
            {
                // �ٽ� ������ �ٲ� �� �ֵ��� �ʱ�ȭ
                isChange = false;
            }

        }

    }

    float t;

    // Update is called once per frame
    void FixedUpdate()
    {
        // �׾��ٸ�
        if (isDead)
        {
            t += Time.deltaTime;

            // ������ ���������
            sr.color = Vector4.Lerp(Color.white, new Vector4(1, 1, 1, 0), t / 2);

            // ������ ����������
            if (sr.color.a == 0)
            {
                // �ڱ��ڽ� ����
                Destroy(gameObject);
            }

            // �Լ� Ż��
            return;
        }

        // ������ �������� �̵�
        rig.velocity = new Vector2(dir, rig.velocity.y);
    }

    // �������� ���� ����
    void RandomDir()
    {
        // �������� ���� ����
        dir = Random.Range(-1, 2);

        // ������ �ֱ�� �ݺ��ؼ� ���� ����
        Invoke("RandomDir", Random.Range(2f, 3f));
    }

    // ���ʹ̰� ������ ȣ��
    void Dead()
    {
        // ���� �ִϸ��̼����� ��ȯ
        anim.SetTrigger("dead");

        // �̵� �ߴ� (= �׾����� ����)
        isDead = true;

        // ��Ƶ� ���� �ʰ� (�ݶ��̴��� ��Ȱ��ȭ)
        GetComponent<BoxCollider2D>().enabled = false;

        // ������ٵ� ��Ȱ��ȭ
        rig.simulated = false;

        // ���� �ٲٴ� �κ�ũ ���
        CancelInvoke("RandomDir");
    }

}
