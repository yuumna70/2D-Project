using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;

    // 카메라 이동 속도
    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 x, y과 카메라의 z
        Vector3 target = new Vector3(player.position.x, player.position.y, transform.position.z);

        // 플레이어 서서히 따라다니기
        transform.position = Vector3.Lerp(transform.position, target, cameraSpeed * Time.deltaTime);
    }
}
