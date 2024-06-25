using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StreetStall : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtOpenStore;
    private string bowFolderPath = "ScriptableObjects/Bows";
    [SerializeField] List<BowScriptable> _listDatabow;
    private bool playerInRange;
    [SerializeField] GameObject shopPanel;
    [SerializeField] GameObject bowBtn;
    [SerializeField] Transform contentBow;

    private void Start()
    {
        shopPanel.SetActive(false);
        LoadGunScriptableObjects();
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            shopPanel.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txtOpenStore.gameObject.SetActive(true);
            playerInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            txtOpenStore.gameObject.SetActive(false);
            playerInRange = false;
        }
    }
    private void LoadGunScriptableObjects()
    {
        _listDatabow.Clear();
        BowScriptable[] bowScriptableObjects = Resources.LoadAll<BowScriptable>(bowFolderPath);
        foreach (BowScriptable bowScriptableObject in bowScriptableObjects)
        {
            _listDatabow.Add(bowScriptableObject);
        }

        if (_listDatabow.Count > 0)
        {
            foreach (var item in _listDatabow)
            {
                var itemSpawned = Instantiate(bowBtn, contentBow).GetComponent<ButtonBuy>();
                itemSpawned.InitData(item);
                itemSpawned.gameObject.SetActive(true);
            }
        }
    }
}
