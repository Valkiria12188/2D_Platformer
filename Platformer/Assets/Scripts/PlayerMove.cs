using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float speed = 5;
    private Animator anim;
    private Rigidbody2D rigid2d;

    [Header("Ground Settings")]
    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    private bool isGrounded;
    private bool jumped;
    [SerializeField]
    private float jumpPower = 5f;


    private void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }


    void PlayerWalk()
    {
        float h, v;
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h > 0)
        {
            rigid2d.velocity = new Vector2(speed, rigid2d.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            rigid2d.velocity = new Vector2(-speed, rigid2d.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            rigid2d.velocity = new Vector2(0f, rigid2d.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)rigid2d.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                rigid2d.velocity = new Vector2(rigid2d.velocity.x, jumpPower);
                anim.SetBool("jump", true);
            }
        }
    }
}
