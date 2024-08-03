using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerQuests : MonoBehaviour
{
    public static PlayerQuests instance;
    [Header("Quest")]
    [SerializeField] public GameObject quest;
    [SerializeField] public GameObject txtQuestPrefabs;
    [SerializeField] public Transform questContent;
    public bool isOpen = false;
    private List<GameObject> activeQuests = new List<GameObject>();
    [SerializeField] public Color completedColor = Color.green;

    private void Awake()
    {
        if (instance == null)
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

    public void AddQuest(GameObject newQuest)
    {
        activeQuests.Add(newQuest);
    }

    public void DestroyCompletedQuests()
    {
        List<GameObject> completedQuests = new List<GameObject>();

        foreach (GameObject quest in activeQuests)
        {
            TextMeshProUGUI txtQuest = quest.GetComponent<TextMeshProUGUI>();
            if (txtQuest != null && txtQuest.color == completedColor)
            {
                completedQuests.Add(quest);
            }
        }
        foreach (GameObject quest in completedQuests)
        {
            activeQuests.Remove(quest);
            Destroy(quest);
        }
    }
}
