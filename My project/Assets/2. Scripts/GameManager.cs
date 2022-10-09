using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ������ Ŭ���� ����ϱ� ����

public class GameManager : MonoBehaviour
{
    // public GameObject player;

    // ���� ����Ʈ���� �θ�
    public Transform spawnPoints;

    // ���� �̹��� ������Ʈ
    public Image numberImg;

    // ���� ��������Ʈ��
    public Sprite[] numberSpriters;

    // ���� ���� �ð��� ����� �׽�Ʈ ������Ʈ
    public Text gameTime;

    public Text lifeTxt;

    public GameObject endingView;

    // ���� ���� �ð��� ��� ����
    float time;

    // �ʸ� ��Ÿ���� ����
    int sec;

    // ��
    int min;

    // �÷��� ������ ��������, �ƴ���
    [HideInInspector]
    public bool isPlay;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate(player);
        // Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);   //identity = (0,0,0)
        // Instantiate(player, player.transform); // player�� �����ؼ� �����Ͽ� player�� �ڽ����� �����Ѵ�.
        // Instantiate(Resources.Load("Bomb"), spawnPoint.transform);   // Resources�� �ִ� Bomb�� ������ ��    // ��θ� ������ / �� ����Ͽ� �ۼ�

        //SpawnBomb();

        // 3,2,1 ī��Ʈ ����
        StartCoroutine(BeforeStart());

        // print("a");




        // transform.position : ������ǥ�� �������� �� ��ġ
        // transform.localPosition : �θ� �������� �� ��ġ
        // �θ��ڽ� ����� transform�� �ֱ� ������ ���ð��� ��ġ�� ���̳�
    }

    // ��ź ���� �Լ�
    void SpawnBomb() // ����Լ� : ���� ���� �θ��� �Լ�
    {
        // (��ź�� �����ؼ� ����) ���� ����Ʈ���� �θ� ���� �ִ� �ڽ� �� �ϳ��� �������� ����
        Instantiate(Resources.Load("Bomb"), spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)));    // �ڽ��� ������ �ִ� ������ŭ

        // ������ ������ �Ŀ� �ڱ� �ڽ� ȣ��
        Invoke("SpawnBomb", Random.Range(0f, 2f));  // 1�ʸ��� �ݺ�
                                                    // Invoke : �ٸ� �Լ��� ȣ���� �� �����̸� �ָ鼭 ȣ��

    }

    void SpawnPotion() // ����Լ� : ���� ���� �θ��� �Լ�
    {
        // (��ź�� �����ؼ� ����) ���� ����Ʈ���� �θ� ���� �ִ� �ڽ� �� �ϳ��� �������� ����
        Instantiate(Resources.Load("Potion"), spawnPoints.GetChild(Random.Range(0, spawnPoints.childCount)));    // �ڽ��� ������ �ִ� ������ŭ

        // ������ ������ �Ŀ� �ڱ� �ڽ� ȣ��
        Invoke("SpawnPotion", Random.Range(0f, 2f));  // 1�ʸ��� �ݺ�
                                                    // Invoke : �ٸ� �Լ��� ȣ���� �� �����̸� �ָ鼭 ȣ��

    }

    // ������ ���۵Ǳ� �� ī��Ʈ
    IEnumerator BeforeStart()
    {
        // 1�� ����
        yield return new WaitForSeconds(1);

        // ���� 2 ��������Ʈ�� ��ü
        numberImg.sprite = numberSpriters[0];

        // 1�� ����
        yield return new WaitForSeconds(1);

        // ���� 1 ��������Ʈ�� ��ü
        numberImg.sprite = numberSpriters[1];

        // 1�� ����
        yield return new WaitForSeconds(1);

        // ���� �̹����� �� ���̰�
        numberImg.gameObject.SetActive(false);

        // ������ �������� ����
        SpawnBomb();

        SpawnPotion();

        // �÷��� ������ �������� ����
        isPlay = true;




    }




    // Update is called once per frame
    void Update()
    {
        // �÷��� ������ ������ ����
        if (isPlay == true)
        {
            // ���� ���� �ð� ���ϱ�
            GameTime();
        }
    }

    void GameTime()
    {
        // ���� ���� �ð� ���ϱ�
        time += Time.deltaTime;

        // �����κи� �߶� �ʿ� �ֱ�
        sec = (int)time;

        // 60�ʰ� �Ǹ�
        if (sec == 60)
        {
            // �� ����
            min++;

            // ���� ���� �ð� 0���� �ʱ�ȭ
            time = 0;
        }

        // ���� ���� �ð��� �ؽ�Ʈ�� ���
        gameTime.text = min.ToString("00") + (":") + sec.ToString("00");
    }

    public void PlayerLife(int life)
    {
        lifeTxt.text = life.ToString();

        if(life == 0)
        {
            Ending("��Ƴ��� �ð�");
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
            // ����
            Destroy(item.gameObject);
        }

    }


}