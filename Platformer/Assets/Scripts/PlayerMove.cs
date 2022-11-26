using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5;
    private Animator anim;
    private Rigidbody2D rigid2d;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;


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
        if (Physics2D.Raycast(groundCheckPosition.position, Vector2.down,0.5f, groundLayer))
        {
            Debug.Log("wykryto grunt");
        }
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

    void ChangeDirection( int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    private void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Ground")
        {
            Debug.Log("wykryto kolizje");
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            Debug.Log("wykryto trigger");
        }
    }
}
