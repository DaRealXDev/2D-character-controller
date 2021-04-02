using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Movement : MonoBehaviour
{
    //Public declarations
    [Header("Movement")]
    public bool canMove;
    public float moveSpeed;
    public bool xLock;
    public bool yLock;
    [Header("Jumping")]
    public bool canJump;
    public float jumpPower;
    public Transform groundCheck;
    public LayerMask isGround;
    [Header("Other")]
    public bool doFlipping;

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
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            dir = new Vector2(x,y);
        }
        else if (!xLock && yLock) {
            float x = Input.GetAxisRaw("Horizontal");
            dir = new Vector2(x,0);
        }
        else if (!yLock && xLock) {
            float y = Input.GetAxisRaw("Vertical");
            dir = new Vector2(0,y);
        }
        Move();

        if (canJump && Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            dir = new Vector2(dir.x,jumpPower);
        }
    }

    bool isGrounded() {
        Collider2D[] check = Physics2D.OverlapCircleAll(groundCheck.position,0.05f,isGround);
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
        if (!doFlipping) 
            return;

        if (dir.x == -1 && facingRight) {
            Flip();
        }
        else if (dir.x == 1 && !facingRight) {
            Flip();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        transform.localScale *= -1;
    }
}
