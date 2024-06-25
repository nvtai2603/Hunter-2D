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
}