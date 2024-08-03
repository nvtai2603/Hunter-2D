using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Rewards_Quest : MonoBehaviour
{
    public static Rewards_Quest instance;
    [SerializeField] GameObject txtOpen;
    [SerializeField] GameObject panelRewards;
    [SerializeField] public GameObject txtQuestComplete;
    [SerializeField] public Transform rewardsContent;
    bool playerInRange;
    [SerializeField] TextMeshProUGUI txtCoinReward;
    [SerializeField] public int totalCoinReward = 0;
    [SerializeField] Button collectRewardButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        collectRewardButton.onClick.AddListener(CollectReward);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            panelRewards.SetActive(true);
        }
        UpdateCoin();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txtOpen.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txtOpen.SetActive(false);
            playerInRange = false;
        }
    }

    void UpdateCoin()
    {
        txtCoinReward.text = "$" + totalCoinReward;
    }

    void CollectReward()
    {
        PlayerStats.instance.gold += totalCoinReward;
        PlayerStats.instance.Coin();
        PlayerPrefs.SetInt("Gold", PlayerStats.instance.gold);
        totalCoinReward = 0;
        UpdateCoin();

        foreach (Transform child in rewardsContent)
        {
            Destroy(child.gameObject);
        }
        PlayerQuests.instance.DestroyCompletedQuests();

        if(Quest_SlimeWhite.instance.isCompleted == true)
        {
            Quest_SlimeWhite.instance.QuestReset();
        }
        if (Quest_SlimeGreen.instance.isCompleted == true)
        {
            Quest_SlimeGreen.instance.QuestReset();
        }
        if (Quest_SlimeBlue.instance.isCompleted == true)
        {
            Quest_SlimeBlue.instance.QuestReset();
        }
        if (Quest_SlimeRed.instance.isCompleted == true)
        {
            Quest_SlimeRed.instance.QuestReset();
        }
    }
}
