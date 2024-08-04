using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] public int gold;
    [Header("Stats")]
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public float health;
    [SerializeField] public float atkDame = 3f;
    [SerializeField] public float atkSpeed = 0.5f;
    [Header("TotalStats")]
    [SerializeField] float totalMoveSpeed;
    [SerializeField] float totalHealth;
    [SerializeField] float totalATKDame;
    [SerializeField] float totalATKSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        gold = PlayerPrefs.GetInt("Gold", gold);
    }

    private void Update()
    {
        Coin();
        TotalStats();
    }

    public void Coin()
    {
        txtCoin.text = gold.ToString()+ "$";
    }

    public void TotalStats()
    {
        totalMoveSpeed = moveSpeed;
        totalHealth = health;
        if (Bow.instance != null)
        {            
            totalATKDame = Bow.instance.dame + atkDame;
        }
        else
        {
            totalATKDame = atkDame;
        }
        if (Bow.instance != null)
        {
            totalATKSpeed = Bow.instance.shootDelay - atkSpeed;
        }
    }
}
