using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("HealthBar")]
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Slider healthBar;

    [Header("Potion")]
    [SerializeField] TextMeshProUGUI txtAmountPotion;
    [SerializeField] int amountPotion;
    [SerializeField] int healAmount = 20;

    private void Start()
    {
        healthBar = GameObject.FindWithTag("HealthBar").GetComponent<Slider>();
        PlayerStats.instance.health = maxHealth;
        HealthBarSlider();
        UpdatePotionText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy_Attack"))
        {
            PlayerStats.instance.health -= Slime.instance.dame;
            HealthBarSlider();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && PlayerStats.instance.health < maxHealth)
        {
            UsePotion();
        }
    }

    void HealthBarSlider()
    {
        healthBar.value = PlayerStats.instance.health / maxHealth;
    }

    void UsePotion()
    {
        if (amountPotion > 0)
        {
            PlayerStats.instance.health += healAmount;
            if (PlayerStats.instance.health > maxHealth)
            {
                PlayerStats.instance.health = maxHealth;
            }
            HealthBarSlider();

            amountPotion--;
            UpdatePotionText();
        }
    }

    void UpdatePotionText()
    {
        txtAmountPotion.text = amountPotion.ToString();
    }
}
