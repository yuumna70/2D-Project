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
        // 플레이어 매니저를 검색해서 플레이어의 트랜스폼 컴포넌트 가져오기
        player = FindObjectOfType<PlayerManager>().transform;

        // 내가 갖고 있는 스프라이트렌더러 컴포넌트 가져오기
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
         Order in Layer = sortingOrder
        캐릭터가 건물 앞으로 가면 건물의 이미지 순서는 뒤로
        캐릭터가 건물 뒤로 가면 건물의 이미지 순서는 앞으로
        (캐릭터의 이미지 순서는 변경 X)
         */

        // 플레이어보다 장애물이 더 앞(아래)에 있다면 
        if (player.position.y > transform.position.y)
        {
            // 장애물을 더 앞으로
            sr.sortingOrder = 1;
        }
        // 플레이어보다 장애물이 더 뒤(위)에 있다면
        else
        {
            // 장애물을 더 뒤로
            sr.sortingOrder = -1;
        }

    }
}
