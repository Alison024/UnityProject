using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovePLayer2 : NetworkBehaviour
{
    // Start is called before the first frame update

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
    const string IN_FRON_RUN = "inFronRun";

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
            spriteRenderer.flipX = true;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal > 0 && vertical == 0)//move right
        {
            isBehindRun = false;
            spriteRenderer.flipX = false;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal < 0 && vertical > 0)//move left up
        {
            isBehindRun = false;
            spriteRenderer.flipX = true;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal < 0 && vertical < 0)//move left down
        {
            isBehindRun = false;
            spriteRenderer.flipX = true;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal > 0 && vertical > 0)//move right up
        {
            isBehindRun = false;
            spriteRenderer.flipX = false;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal > 0 && vertical < 0)//move right down
        {
            isBehindRun = false;
            spriteRenderer.flipX = false;
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

    void ChangeAnimationState(string newState)
    {
        animator.Play(newState);
    }
}
