using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable/BowScriptable")]
public class BowScriptable : ScriptableObject
{
    public static BowScriptable instance;
    public string nameBow;
    public Sprite avatarBow;
    public Image iconBow;
    public GameObject bowPrefab;
    public float dameBow;
    public float atkSpeedBow;
    public int maxPurchase;
    public int price;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
