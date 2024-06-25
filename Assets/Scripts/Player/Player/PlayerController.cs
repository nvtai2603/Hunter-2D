using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] Transform bow;
    Rigidbody2D r2d;
    Vector2 move;
    Vector2 lastMove;
    Animator anim;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        transform.rotation = Quaternion.identity;
        Move();
        RotateBow();
    }
    void Move()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        if (move != Vector2.zero)
        {
            lastMove = move;
        }
        anim.SetFloat("Horizontal", lastMove.x);
        anim.SetFloat("Vertical", lastMove.y);
        anim.SetFloat("Speed", move.sqrMagnitude);
        r2d.velocity = move.normalized * PlayerStats.instance.moveSpeed;
    }
    void RotateBow()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 direction = (mousePos - bow.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bow.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));
    }
}
