using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SlimeType
{
    Blue,
    Red,
    Green,
    White
}

public class Slime_Health : MonoBehaviour
{
    [Header("HealthBar")]
    [SerializeField] GameObject enemy;
    [SerializeField] float maxHealth = 100f;
    public float health;
    [SerializeField] Slider healthSlider;
    [SerializeField] SlimeType slimeType;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        HealthBar();
    }

    void HealthBar()
    {
        healthSlider.value = health / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Quest_SlimeBlue.instance.SlimeKilled(slimeType);
            Quest_SlimeRed.instance.SlimeKilled(slimeType);
            Quest_SlimeGreen.instance.SlimeKilled(slimeType);
            Quest_SlimeWhite.instance.SlimeKilled(slimeType);
            Destroy(enemy);
        }
    }
}
