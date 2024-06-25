using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonBuy : MonoBehaviour
{
    [SerializeField] BowScriptable bow;
    [SerializeField] Button _buyButton;
    [SerializeField] Image bowImage;
    [SerializeField] TextMeshProUGUI txtName;
    [SerializeField] TextMeshProUGUI txtPrice;
    [SerializeField] TextMeshProUGUI txtDame;
    [SerializeField] TextMeshProUGUI txtAtkSpeed;
    int purchaseCount = 0;
    private string buttonName;
    [SerializeField] bool isButtonVisible = true;
    [SerializeField] public bool isBowPurchased = false;

    private void Start()
    {
        bowImage.sprite = bow.avatarBow;
        buttonName = bow.name;
        if (PlayerPrefs.HasKey("PurchaseCount_" + buttonName))
        {
            purchaseCount = PlayerPrefs.GetInt("PurchaseCount_" + buttonName);
        }

        if (purchaseCount >= bow.maxPurchase)
        {
            isButtonVisible = false;
            gameObject.SetActive(false);
        }
    }

    public void BuyBow()
    {
        ChooseBow chooseBow = FindObjectOfType<ChooseBow>();
        if (purchaseCount < bow.maxPurchase && PlayerStats.instance.gold >= bow.price)
        {
            PlayerStats.instance.gold -= bow.price;
            PlayerPrefs.SetInt("Gold", PlayerStats.instance.gold);
            purchaseCount++;
            PlayerPrefs.SetInt("PurchaseCount_" + buttonName, purchaseCount);
            isBowPurchased = true;
            chooseBow.AddSlot(bow.iconBow);
            chooseBow.AddBowPrefab(bow.bowPrefab);

            SaveBowPrefabs(chooseBow.bowPrefabs);
            SaveSlot(chooseBow.slots);

            if (purchaseCount >= bow.maxPurchase)
            {
                isButtonVisible = false;
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Không Đủ Tiền !!!!!");
        }
    }

    private void SaveBowPrefabs(List<GameObject> bowPrefabs)
    {
        List<string> bowPrefabNames = new List<string>();
        foreach (var bowPrefab in bowPrefabs)
        {
            bowPrefabNames.Add(bowPrefab.name);
        }

        string json = JsonUtility.ToJson(new SerializableList<string>(bowPrefabNames));
        PlayerPrefs.SetString("BowPrefabs", json);
        PlayerPrefs.Save();
    }

    private void SaveSlot(List<Image> slots)
    {
        List<string> slotNames = new List<string>();
        foreach (var slot in slots)
        {
            if (slot.gameObject.activeInHierarchy)
            {
                slotNames.Add(slot.name);
            }
        }

        string json = JsonUtility.ToJson(new SerializableList<string>(slotNames));
        PlayerPrefs.SetString("SavedSlots", json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class SerializableList<T>
    {
        public List<T> list;
        public SerializableList(List<T> list)
        {
            this.list = list;
        }
    }

    public void InitData(BowScriptable data)
    {
        bow = data;
        SetDataToItem();
        _buyButton.onClick.AddListener(BuyBow);
    }

    private void SetDataToItem()
    {
        bowImage.sprite = bow.avatarBow;
        txtName.text = bow.nameBow.ToString();
        txtPrice.text = "$" + bow.price.ToString();
        txtDame.text = "Attack Dame: " + bow.dameBow.ToString();
        txtAtkSpeed.text = "Attack Speed: " + bow.atkSpeedBow.ToString() + "s";
    }

    private void OnEnable()
    {
        if (isButtonVisible)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
