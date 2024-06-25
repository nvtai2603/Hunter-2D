using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerQuests : MonoBehaviour
{
    public static PlayerQuests instance;
    [Header("Quest")]
    [SerializeField] public GameObject quest;
    [SerializeField] public GameObject txtQuestPrefabs;
    [SerializeField] public Transform questContent;
    public bool isOpen = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }       
    }
    private void Start()
    {
        quest.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (!isOpen)
            {
                quest.SetActive(true);
                isOpen = true;
            }
            else
            {
                quest.SetActive(false);
                isOpen = false;
            }
        }
    }
    public void DestroyQuest()
    {
        foreach (Transform quest in questContent)
        {
            if (quest.GetComponent<txtMission>().isCompleted)
            {
                Destroy(quest.gameObject);
                Quest_SlimeBlue.instance.questAccepted = false;
            }
            if (quest.GetComponent<txtMission>().isCompleted)
            {
                Destroy(quest.gameObject);
                Quest_SlimeRed.instance.questAccepted = false;
            }
            if (quest.GetComponent<txtMission>().isCompleted)
            {
                Destroy(quest.gameObject);
                Quest_SlimeWhite.instance.questAccepted = false;
            }
            if (quest.GetComponent<txtMission>().isCompleted)
            {
                Destroy(quest.gameObject);
                Quest_SlimeGreen.instance.questAccepted = false;
            }
        }
    }
}