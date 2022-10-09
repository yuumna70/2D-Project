using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // �̺�Ʈ�ý����� ����ϱ� ����


public class PlayerFire : MonoBehaviour
{
    // �� �߻��ϴ� ��
    public float firePower;

    SpriteRenderer sr;

    // ������ �����ϴ� ����
    int dir;

    // �Ѿ� ����
    public int bulletCount = 5;

    // �Ѿ� ������Ʈ Ǯ (=źâ)
    public GameObject[] bulletPool;

    // �� ȿ���� ����
    public AudioClip[] clips;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        audio = transform.GetChild(0).GetComponent<AudioSource>();

        // 1. źâ�� �� �Ѿ� ���� ����
        bulletPool = new GameObject[bulletCount];

        // 2. źâ�� �� �Ѿ� ������ŭ �ݺ��ؼ�
        for (int i = 0; i < bulletCount; i++)
        {
            // 3, �Ѿ� �����ؼ� ����
            GameObject bullet = Instantiate(Resources.Load("Bullet")) as GameObject;

            // 4. ������ �Ѿ� źâ�� �ֱ�
            bulletPool[i] = bullet;

            // 5. �ϴ� ��Ȱ��ȭ
            bullet.SetActive(false);
        }




    }

    // Update is called once per frame
    void Update()
    {
        // ��Ŭ�� �ߴٸ� + UI ���� Ŀ���� �ִ°� �ƴ϶�� + ���� ȭ���� �������� �ʴٸ�
        if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0)
        {
            // �������� � �� ȿ������ ���� ����
            audio.clip = clips[Random.Range(0, 2)];

            // �Ѿ� ȿ���� ����
            audio.Play();

            // �������� ���� �ִٸ�
            if (!sr.flipX)
            {
                // ������ ����������
                dir = 1;
            }
            // ������ ���� �ִٸ�
            else
            {
                // ������ ��������
                dir = -1;
            }


            // �÷��̾��� ��ġ���� ���� ��/�Ʒ��� �Ѿ� ���� �� ����
            // GameObject bullet = Instantiate(Resources.Load("Bullet"), transform.position + new Vector3(dir * 0.6f , -0.5f, 0), Quaternion.identity) as GameObject;

            // �Ѿ� ������ŭ �ݺ�
            for (int i = 0; i < bulletCount; i++)
            {
                // i��° �Ѿ� ����
                GameObject bullet = bulletPool[i];

                // �߻��ϱ� �� �Ѿ��̶�� (=��Ȱ��ȭ ���¶��)
                if (!bullet.activeSelf)
                {


                    // �߻� ������ ��ġ ����
                    bullet.transform.position = transform.position + new Vector3(dir * 0.6f, -0.5f, 0);

                    // �߻��ϱ� �� Ȱ��ȭ
                    bullet.SetActive(true);

                    // �Ѿ��� �ٶ󺸴� �������� �߻�
                    bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * dir * firePower, ForceMode2D.Impulse);



                    // �Ѿ� ����
                    StartCoroutine(ResetBuulet(bullet));

                    break;
                }


            }


        }
    }

    // �Ѿ� ����
    IEnumerator ResetBuulet(GameObject bullet)
    {

        // 2�� ����
        yield return new WaitForSeconds(2);

        // �Ѿ� ��Ȱ��ȭ
        bullet.SetActive(false);
    }

}
