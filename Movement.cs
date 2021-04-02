using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    //Public declarations
    [Header("Movement")]
    [Space]
    public bool canMove;
    public float moveSpeed;
    public bool xLock;
    public bool yLock;
    [Header("Jumping")]
    [Space]
    public bool canJump;
    public float jumpPower;
    public Transform groundCheck;
    public LayerMask isGround;

    //Private declarations
    bool facingRight = true;
    Vector2 dir;
    Transform t;
    Rigidbody2D rb;
    GameObject obj;

    //Methods
    private void Start() {
        t = transform;
        rb = this.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        obj = this.gameObject;
    }
    private void FixedUpdate() {
        if (!xLock && !yLock) {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
            dir = new Vector2(x,y);
        }
        else if (!xLock && yLock) {
            x = Input.GetAxisRaw("Horizontal");
            dir = new Vector2(x,0);
        }
        else if (!yLock && xLock) {
            y = Input.GetAxisRaw("Vertical");
            dir = new Vector2(0,y);
        }
        Move()

        if (canJump && Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            dir = new Vector2(dir.x,jumpPower)
        }
    }

    bool isGrounded() {
        Collider2D[] check = Physics2D.OverlapCircleAll(groundCheck.position,0.05,isGround);
        foreach (Collider2D v in check)
        {
            if (v != obj) {
                return true;
            }
        }
        return false;
    }

    void Move() {
        rb.velocity = dir*moveSpeed*Time.deltaTime;
    }
}