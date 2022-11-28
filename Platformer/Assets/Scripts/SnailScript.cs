using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Rigidbody2D rigid;
    private Animator anim;

    public LayerMask playerLayer;

    private bool moveLeft;

    private bool canMove;
    private bool stunned;

    public Transform left_Collision, right_Collision, down_Collision, top_Collision;
    private Vector3 left_Collision_Position, right_Collision_Position;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        left_Collision_Position = left_Collision.position;
        right_Collision_Position = right_Collision.position;

        if (canMove)
        {
            if (moveLeft)
            {
                rigid.velocity = new Vector2(-moveSpeed, rigid.velocity.y);
            }
            else if (!moveLeft)
            {
                rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);
            }
        }

        CheckCollision();

        //anim.SetBool("",);
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position,Vector2.left,0.1f,playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if(topHit!=null)
        {
            if(topHit.gameObject.tag==MyTags.Player_Tag)
            {
                if(!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    rigid.velocity = new Vector2(0, 0);
                    anim.Play("SnailStunned");
                    stunned = true;

                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.Player_Tag)
            {
                if (!stunned)
                {
                    //dmg to player
                    Debug.Log("left DMG");
                }
                else
                {
                    rigid.velocity = new Vector2(15f, rigid.velocity.y);
                }
            }
        }


        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.Player_Tag)
            {
                if (!stunned)
                {
                    //dmg to player
                    Debug.Log("Right DMG");
                }
                else
                {
                    rigid.velocity = new Vector2(-15f, rigid.velocity.y);
                }
            }
        }

        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.5f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft; //zmiana kierunku ruchu
        Vector3 tempScale = transform.localScale;//obrót kierunku œlimaka

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);
            left_Collision.position = left_Collision_Position;
            right_Collision.position = right_Collision_Position;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);
            left_Collision.position = right_Collision_Position;
            right_Collision.position = left_Collision_Position;
        }

        transform.localScale = tempScale;
    }











}
