using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBow : MonoBehaviour
{
    [SerializeField] Transform transBow;
    [SerializeField] public List<GameObject> bowPrefabs = new List<GameObject>();
    [SerializeField] public List<Image> slots = new List<Image>();
    public GameObject currentBowInstance;
    public Transform[] spawnImage;

    private int currentBowIndex = -1;

    void Start()
    {
        transBow = GameObject.FindWithTag("SpawnBow").GetComponent<Transform>();
        LoadBowPrefabs();
        LoadIconPrefabs();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleBowPrefab(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleBowPrefab(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleBowPrefab(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ToggleBowPrefab(3);
        }
    }

    public void ToggleBowPrefab(int index)
    {
        if (index == currentBowIndex)
        {
            Destroy(currentBowInstance);
            currentBowInstance = null;
            currentBowIndex = -1;
        }
        else
        {
            SetBowPrefab(index);
            currentBowIndex = index;
        }
    }

    public void SetBowPrefab(int index)
    {
        if (index >= 0 && index < bowPrefabs.Count)
        {
            if (currentBowInstance != null)
            {
                Destroy(currentBowInstance);
            }
            currentBowInstance = Instantiate(bowPrefabs[index], transBow.position, transBow.rotation);
            currentBowInstance.transform.SetParent(transBow);
        }
    }

    public void AddBowPrefab(GameObject bowPrefab)
    {
        bowPrefabs.Add(bowPrefab);
    }

    public void AddSlot(Image bow)
    {
        slots.Add(bow);
        int index = slots.IndexOf(bow);
        if (index < spawnImage.Length)
        {
            Image newImage = Instantiate(bow, spawnImage[index].position, spawnImage[index].rotation);
            newImage.transform.SetParent(spawnImage[index]);
        }
    }

    private void LoadBowPrefabs()
    {
        if (PlayerPrefs.HasKey("BowPrefabs"))
        {
            string json = PlayerPrefs.GetString("BowPrefabs");
            SerializableList<string> bowPrefabNames = JsonUtility.FromJson<SerializableList<string>>(json);

            foreach (string bowPrefabName in bowPrefabNames.list)
            {
                GameObject bowPrefab = Resources.Load<GameObject>("Bows/" + bowPrefabName);
                if (bowPrefab != null)
                {
                    bowPrefabs.Add(bowPrefab);
                }
            }
        }
    }

    private void LoadIconPrefabs()
    {
        if (PlayerPrefs.HasKey("BowPrefabs"))
        {
            string json = PlayerPrefs.GetString("BowPrefabs");
            SerializableList<string> imagePrefabNames = JsonUtility.FromJson<SerializableList<string>>(json);

            foreach (string imagePrefabName in imagePrefabNames.list)
            {
                Image imagePrefab = Resources.Load<Image>("ImageIcon/" + imagePrefabName);
                if (imagePrefab != null)
                {
                    slots.Add(imagePrefab);
                    int index = slots.IndexOf(imagePrefab);
                    if (index < spawnImage.Length)
                    {
                        Image newImage = Instantiate(imagePrefab, spawnImage[index].position, spawnImage[index].rotation);
                        newImage.transform.SetParent(spawnImage[index]);
                    }
                }
            }
        }
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
}
