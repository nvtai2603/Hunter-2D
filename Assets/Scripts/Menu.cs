using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Button btnStart, btnQuit;
    [Header("Setting")]
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnApplySetting;
    [SerializeField] GameObject btn;
    [SerializeField] GameObject panelSetting;
    bool isOpen;
    private void Start()
    {
        btnStart.onClick.AddListener(OnClickButtonStart);
        btnSetting.onClick.AddListener(OnClickButtonSetting);
        btnApplySetting.onClick.AddListener(OnClickButtonApplySetting);
        btnQuit.onClick.AddListener(OnClickButtonQuit);
    }

    void OnClickButtonStart()
    {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1.0f;
    }
    void OnClickButtonSetting()
    {
        if(!isOpen)
        {
            panelSetting.SetActive(true);
            btn.SetActive(false);
            isOpen = true;
        }
    }
    void OnClickButtonApplySetting()
    {
        if (isOpen)
        {
            panelSetting.SetActive(false);
            btn.SetActive(true);
            isOpen = false;
        }
    }
    void OnClickButtonQuit()
    {
        Application.Quit();
    }
}
