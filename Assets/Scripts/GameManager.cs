using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] Transform spawnPlayer;
    [SerializeField] GameObject player;

    [Header("Enemy")]
    [SerializeField] Transform[] spawnEnemy;
    [SerializeField] GameObject[] enemy;
    [SerializeField] int maxEnemiesPerSpawnPoint = 5;
    [SerializeField] float spawnInterval = 10f;

    private List<List<GameObject>> spawnedEnemies;

    [Header("Menu")]
    [SerializeField] GameObject panelMenu;
    bool isOpen;
    [SerializeField] Button resume, setting, menu, btnApplySetting;
    [SerializeField] GameObject panelBtn;
    [SerializeField] GameObject panelSetting;
    bool isOpenSetting;

    private void Start()
    {
        spawnedEnemies = new List<List<GameObject>>();
        for (int i = 0; i < spawnEnemy.Length; i++)
        {
            spawnedEnemies.Add(new List<GameObject>());
        }

        SpawnPlayer();
        for (int i = 0; i < spawnEnemy.Length; i++)
        {
            StartCoroutine(SpawnEnemy(i));
        }
        resume.onClick.AddListener(OnClickButtonResume);
        setting.onClick.AddListener(OnClickButtonSetting);
        btnApplySetting.onClick.AddListener(OnClickButtonApplySetting);
        menu.onClick.AddListener(OnClickButtonMenu);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                panelMenu.SetActive(true);
                Time.timeScale = 0f;
                isOpen = true;
            }
            else
            {
                panelMenu.SetActive(false);
                Time.timeScale = 1f;
                isOpen = false;
            }
        }
    }
    void SpawnPlayer()
    {
        if (spawnPlayer != null && player != null)
        {
            Instantiate(player, spawnPlayer.position, spawnPlayer.rotation);
        }
    }

    IEnumerator SpawnEnemy(int spawnIndex)
    {
        while (true)
        {
            if (spawnedEnemies[spawnIndex].Count < maxEnemiesPerSpawnPoint)
            {
                for (int i = 0; i < maxEnemiesPerSpawnPoint - spawnedEnemies[spawnIndex].Count; i++)
                {
                    GameObject newEnemy = Instantiate(enemy[spawnIndex], spawnEnemy[spawnIndex].position, spawnEnemy[spawnIndex].rotation);
                    spawnedEnemies[spawnIndex].Add(newEnemy);
                }
            }
            CleanUpEnemies(spawnIndex);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void CleanUpEnemies(int spawnIndex)
    {
        for (int i = spawnedEnemies[spawnIndex].Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[spawnIndex][i] == null)
            {
                spawnedEnemies[spawnIndex].RemoveAt(i);
            }
        }
    }
    void OnClickButtonResume()
    {
        panelMenu.SetActive(false);
        Time.timeScale = 1f;
        isOpen = false;
    }
    void OnClickButtonSetting()
    {
        if (!isOpenSetting)
        {
            panelSetting.SetActive(true);
            panelBtn.SetActive(false);
            isOpenSetting = true;
        }
    }
    void OnClickButtonApplySetting()
    {
        if (isOpenSetting)
        {
            panelSetting.SetActive(false);
            panelBtn.SetActive(true);
            isOpenSetting = false;
        }
    }
    void OnClickButtonMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
