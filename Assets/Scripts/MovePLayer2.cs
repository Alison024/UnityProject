using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MovePLayer2 : NetworkBehaviour
{
    // Start is called before the first frame update

    public bool isFlipX = true;
    public float speed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isBehindRun;
    private Rigidbody2D rigidbody2D;
    private Vector3 currentPosition;
    private WeaponScript weaponScript;
    private PlayerHealth healthBarScript;

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
        weaponScript = GetComponent<WeaponScript>();
        healthBarScript = GetComponent<PlayerHealth>();
        isBehindRun = false;
    }

    void Update()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if ((positionOnScreen - mouseOnScreen).x < 0)
        {
            weaponScript.FlipYWeapon(false);
        }
        else
        {
            weaponScript.FlipYWeapon(true);
        }

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
            spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
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
            spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
        }
        else if (horizontal < 0 && vertical < 0)//move left down
        {
            isBehindRun = false;
            spriteRenderer.flipX = isFlipX;
            ChangeAnimationState(SIDE_WAY_RUN);
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
        
        
    }

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
