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
            // 오디오매니저 삭제
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
        // 게임 종료
#if UNITY_EDITOR  // 유니티 에디터에서만
        UnityEditor.EditorApplication.isPlaying = false;
#else // 유니티 에디터가 아닐 때
        Application.Quit();
#endif       

    }

}
