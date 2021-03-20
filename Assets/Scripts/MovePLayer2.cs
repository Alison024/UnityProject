using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovePLayer2 : NetworkBehaviour
{
    public bool isFlipX = true;
    public float speed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isBehindRun;
    private Rigidbody2D rigidbody2D;
    private Vector3 currentPosition;

    const string CALM = "calm";
    const string BEHIND_CALM = "behindCalm";
    const string BEHIND_RUN = "behindRun";
    const string SIDE_WAY_RUN = "sideWayRun";
    const string SIDE_WAY_RUN_LEFT = "sideWayRunLeft";
    const string IN_FRON_RUN = "inFromRun";

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isBehindRun = false;
    }

    void Update()
    {
       

        if (!isLocalPlayer)
        {
            // exit from update if this is not the local player
            return;
        }

        double horizontal = Input.GetAxis("Horizontal");
        double vertical = Input.GetAxis("Vertical");
        
        if (horizontal < 0 && vertical == 0)//move left
        {
            isBehindRun = false;
            //spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN_LEFT);
        }
        else if (horizontal > 0 && vertical == 0)//move right
        {
            isBehindRun = false;
            spriteRenderer.flipX = !isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal < 0 && vertical > 0)//move left up
        {
            isBehindRun = false;
            //spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN_LEFT);
        }
        else if (horizontal < 0 && vertical < 0)//move left down
        {
            isBehindRun = false;
            //spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN_LEFT);
        }
        else if (horizontal > 0 && vertical > 0)//move right up
        {
            isBehindRun = false;
            spriteRenderer.flipX = !isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal > 0 && vertical < 0)//move right down
        {
            isBehindRun = false;
            spriteRenderer.flipX = !isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal == 0 && vertical > 0)//move up
        {
            isBehindRun = true;
            ChangeAnimationState(BEHIND_RUN);
        }
        else if (horizontal == 0 && vertical < 0)//move down
        {
            isBehindRun = false;
            ChangeAnimationState(IN_FRON_RUN);
        }
        else if(horizontal == 0 && vertical == 0)//calm
        {
            if (isBehindRun)
            {
                ChangeAnimationState(BEHIND_CALM);
            }
            else
            {
                ChangeAnimationState(CALM);
            }
        }

        //CmdDebugLog();
    }
    /*[Command]
    void CmdDebugLog()
    {
        Debug.Log(connectionToClient.authenticationData.ToString());
        Debug.Log("Net id = "+netId);
    }*/
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }
    }
    void FixedUpdate()
    {
        if (!isLocalPlayer)
        {
            // exit from update if this is not the locssal player
            return;
        }
        currentPosition = transform.position;
        currentPosition.x += Input.GetAxis("Horizontal") * speed;
        currentPosition.y += Input.GetAxis("Vertical") * speed;
        rigidbody2D.MovePosition(currentPosition);
    }

    private void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
