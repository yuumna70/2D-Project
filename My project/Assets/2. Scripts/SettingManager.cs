using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    public GameObject settingView;

    public void ClickSetting()
    {
        settingView.SetActive(true);

        Time.timeScale = 0;
    }

    public void ClickContinue()
    {
        settingView.SetActive(false);

        Time.timeScale = 1;
    }

    public void ClickHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickRestart()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

}
