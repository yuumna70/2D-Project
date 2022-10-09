using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public GameObject title;

    public GameObject dontdestory;

    private void Awake()
    {
        DontDestroyOnLoad(dontdestory);

        GameObject[] audios = GameObject.FindGameObjectsWithTag("Audio");

        if (audios.Length >= 2)
        {
            // ������Ŵ��� ����
            Destroy(audios[1]);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1;

        GetComponent<GameObject>();
        GetComponent<Transform>();
        GetComponent<MainManager>();

        title.GetComponent<Transform>();

    }

    // Update is called once per frame
    public void ClickStart()
    {
        SceneManager.LoadScene("2. PlayScene");
    }

    public void ClickQuit()
    {
        // ���� ����
#if UNITY_EDITOR  // ����Ƽ �����Ϳ�����
        UnityEditor.EditorApplication.isPlaying = false;
#else // ����Ƽ �����Ͱ� �ƴ� ��
        Application.Quit();
#endif       

    }

}
