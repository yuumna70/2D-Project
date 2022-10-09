using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 유아이 클래스 사용하기 위함

public class GameManager : MonoBehaviour
{
    // public GameObject player;

    // 스폰 포인트들의 부모
    public Transform spawnPoints;

    // 숫자 이미지 컴포넌트
    public Image numberImg;

    // 숫자 스프라이트들
    public Sprite[] numberSpriters;

    // 게임 진행 시간을 출력할 테스트 컴포넌트
    public Text gameTime;

    public Text lifeTxt;

    public GameObject endingView;

    // 게임 진행 시간이 담길 변수
    float time;

    // 초를 나타내는 변수
    int sec;

    // 분
    int min;

    // 플레이 가능한 상태인지, 아닌지
    [HideInInspector]
    public bool isPlay;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(player);
        // Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);   //identity = (0,0,0)
        // Instantiate(player, player.transform); // player를 복제해서 생성하여 player의 자식으로 생성한다.
        // Instantiate(Resources.Load("Bomb"), spawnPoint.transform);   // Resources에 있는 Bomb를 가지고 옴    // 경로를 쓸때는 / 를 사용하여 작성

        //SpawnBomb();

        // 3,2,1 카운트 시작
        StartCoroutine(BeforeStart());

        // print("a");




        // transform.position : 월드좌표를 기준으로 한 위치
        // transform.localPosition : 부모를 기준으로 한 위치
        // 부모자식 관계는 transform에 있기 때문에 로컬과의 위치가 차이남
    }

    // 폭탄 스폰 함수
    void SpawnBomb() // 재귀함수 : 내가 나를 부르는 함수
    {
        // (폭탄을 복제해서 생성) 스폰 포인트들의 부모가 갖고 있는 자식 중 하나에 랜덤으로 생성
        Instantiate(Resources.Load("Bomb"), spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)));    // 자식이 가지고 있는 개수만큼

        // 랜덤한 딜레이 후에 자기 자신 호출
        Invoke("SpawnBomb", Random.Range(0f, 2f));  // 1초마다 반복
                                                    // Invoke : 다른 함수를 호출할 때 딜레이를 주면서 호출

    }

    void SpawnPotion() // 재귀함수 : 내가 나를 부르는 함수
    {
        // (폭탄을 복제해서 생성) 스폰 포인트들의 부모가 갖고 있는 자식 중 하나에 랜덤으로 생성
        Instantiate(Resources.Load("Potion"), spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)));    // 자식이 가지고 있는 개수만큼

        // 랜덤한 딜레이 후에 자기 자신 호출
        Invoke("SpawnPotion", Random.Range(0f, 2f));  // 1초마다 반복
                                                    // Invoke : 다른 함수를 호출할 때 딜레이를 주면서 호출

    }

    // 게임이 시작되기 전 카운트
    IEnumerator BeforeStart()
    {
        // 1초 쉬고
        yield return new WaitForSeconds(1);

        // 숫자 2 스프라이트로 교체
        numberImg.sprite = numberSpriters[0];

        // 1초 쉬고
        yield return new WaitForSeconds(1);

        // 숫자 1 스프라이트로 교체
        numberImg.sprite = numberSpriters[1];

        // 1초 쉬고
        yield return new WaitForSeconds(1);

        // 숫자 이미지가 안 보이게
        numberImg.gameObject.SetActive(false);

        // 아이템 떨어지기 시작
        SpawnBomb();

        SpawnPotion();

        // 플레이 가능한 상태임을 저장
        isPlay = true;




    }




    // Update is called once per frame
    void Update()
    {
        // 플레이 가능한 상태일 때만
        if (isPlay == true)
        {
            // 게임 진행 시간 구하기
            GameTime();
        }
    }

    void GameTime()
    {
        // 게임 진행 시간 구하기
        time += Time.deltaTime;

        // 정수부분만 잘라서 초에 넣기
        sec = (int)time;

        // 60초가 되면
        if (sec == 60)
        {
            // 분 증가
            min++;

            // 게임 진행 시간 0으로 초기화
            time = 0;
        }

        // 게임 진행 시간을 텍스트에 출력
        gameTime.text = min.ToString("00") + (":") + sec.ToString("00");
    }

    public void PlayerLife(int life)
    {
        lifeTxt.text = life.ToString();

        if(life == 0)
        {
            Ending("살아남은 시간");
        }

    }

    public void Ending(string txt)
    {
        endingView.SetActive(true);

        endingView.GetComponentInChildren<Text>().text = "\n" + txt +  "\n" + gameTime.text;

        isPlay = false;

        Time.timeScale = 0;

        FallingItem[] items = FindObjectsOfType<FallingItem>();

        foreach (var item in items)
        {
            // 삭제
            Destroy(item.gameObject);
        }

    }


}