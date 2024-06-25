using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Globalization;

public class Quest_SlimeRed : MonoBehaviour
{
    public static Quest_SlimeRed instance;
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
        txtInfor.text = "Kill " + slimesNeeded + " Slime Red";
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
            txtQuest.text = "Kill Slimes Red " + slimesKilled + "/" + slimesNeeded;
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
        if (slimeType == SlimeType.Red)
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
        if (slimesKilled >= slimesNeeded)
        {
            Debug.Log("Quest Completed!");
            txtQuest.color = completedColor;
        }
    }

    void UpdateQuestText()
    {
        if (txtQuest != null)
        {
            txtQuest.text = "Kill Slimes Red " + slimesKilled + "/" + slimesNeeded;
            txtQuest.color = (slimesKilled >= slimesNeeded) ? completedColor : incompleteColor;
        }
    }
}
