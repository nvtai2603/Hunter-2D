using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public static Arrow instance;
    [SerializeField] LayerMask destroyOnCollisionWith;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float newDame = Bow.instance.dame + PlayerStats.instance.atkDame;
            collision.GetComponent<Slime_Health>().TakeDamage(newDame);
            Destroy(gameObject);
        }
        else if (((1 << collision.gameObject.layer) & destroyOnCollisionWith) != 0)
        {
            Destroy(gameObject);
        }
    }
}
