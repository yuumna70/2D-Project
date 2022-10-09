using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    Transform player;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        // �÷��̾� �Ŵ����� �˻��ؼ� �÷��̾��� Ʈ������ ������Ʈ ��������
        player = FindObjectOfType<PlayerManager>().transform;

        // ���� ���� �ִ� ��������Ʈ������ ������Ʈ ��������
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         Order in Layer = sortingOrder
        ĳ���Ͱ� �ǹ� ������ ���� �ǹ��� �̹��� ������ �ڷ�
        ĳ���Ͱ� �ǹ� �ڷ� ���� �ǹ��� �̹��� ������ ������
        (ĳ������ �̹��� ������ ���� X)
         */

        // �÷��̾�� ��ֹ��� �� ��(�Ʒ�)�� �ִٸ� 
        if (player.position.y > transform.position.y)
        {
            // ��ֹ��� �� ������
            sr.sortingOrder = 1;
        }
        // �÷��̾�� ��ֹ��� �� ��(��)�� �ִٸ�
        else
        {
            // ��ֹ��� �� �ڷ�
            sr.sortingOrder = -1;
        }

    }
}
