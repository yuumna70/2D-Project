using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;

    // ī�޶� �̵� �ӵ�
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾��� x, y�� ī�޶��� z
        Vector3 target = new Vector3(player.position.x, player.position.y, transform.position.z);

        // �÷��̾� ������ ����ٴϱ�
        transform.position = Vector3.Lerp(transform.position, target, cameraSpeed * Time.deltaTime);
    }
}
