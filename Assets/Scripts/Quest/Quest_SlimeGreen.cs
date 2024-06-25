using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quest_SlimeGreen : MonoBehaviour
{
    public static Quest_SlimeGreen instance;
    [SerializeField] TextMeshProUGUI txtViewQuest;
    [SerializeField] GameObject questPanel;
    [SerializeField] Button cancelButton;
    [SerializeField] Button acceptButton;

    private bool playerInRange;
    private bool questAccepted;
    [SerializeField] public int slimesKilled;
    [SerializeField] int slimesNeeded = 10;
    private TextMeshProUGUI txtQuest;

    [SerializeField] private Color completedColor = Color.green;
    [SerializeField] private Color incompleteColor = Color.red;
    [SerializeField] TextMeshProUGUI txtInfor;

    [SerializeField] public int coinReward = 100;
    bool isCompleted = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        txtViewQuest.gameObject.SetActive(false);
        questPanel.SetActive(false);
        txtInfor.text = "Kill " + slimesNeeded + " Green Slimes";
        cancelButton.onClick.AddListener(CancelQuest);
        acceptButton.onClick.AddListener(AcceptQuest);
    }

    void Update()
    {
        if (playerInRange && !questAccepted && Input.GetKeyDown(KeyCode.E))
        {
            questPanel.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !questAccepted)
        {
            txtViewQuest.gameObject.SetActive(true);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            txtViewQuest.gameObject.SetActive(false);
            playerInRange = false;
        }
    }

    void CancelQuest()
    {
        questPanel.SetActive(false);
    }

    void AcceptQuest()
    {
        GameObject newQuest = Instantiate(PlayerQuests.instance.txtQuestPrefabs, PlayerQuests.instance.questContent);
        txtQuest = newQuest.GetComponent<TextMeshProUGUI>();

        if (txtQuest != null)
        {
            txtQuest.text = "Kill Green Slimes " + slimesKilled + "/" + slimesNeeded;
            txtQuest.color = incompleteColor;
        }

        questPanel.SetActive(false);
        PlayerQuests.instance.quest.SetActive(true);
        PlayerQuests.instance.isOpen = true;
        questAccepted = true;
        txtViewQuest.gameObject.SetActive(false);
    }

    public void SlimeKilled(SlimeType slimeType)
    {
        if (!questAccepted) return;
        if (slimeType == SlimeType.Green)
        {
            if (slimesKilled < slimesNeeded)
            {
                slimesKilled++;
            }
            UpdateQuestText();
            CheckQuestCompletion();
        }
    }

    void CheckQuestCompletion()
    {
        if (isCompleted) return;
        if (slimesKilled >= slimesNeeded)
        {
            txtQuest.color = completedColor;
            GameObject newQuest = Instantiate(Rewards_Quest.instance.txtQuestComplete, Rewards_Quest.instance.rewardsContent); ;
            txtQuest = newQuest.GetComponent<TextMeshProUGUI>();
            txtQuest.text = "Kill Green Slimes Completed";
            Rewards_Quest.instance.totalCoinReward += coinReward;
            isCompleted = true;
        }
    }

    void UpdateQuestText()
    {
        if (txtQuest != null && !isCompleted)
        {
            txtQuest.text = "Kill Green Slimes " + slimesKilled + "/" + slimesNeeded;
            txtQuest.color = (slimesKilled >= slimesNeeded) ? completedColor : incompleteColor;
        }
    }
}
