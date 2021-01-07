using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 currentPosition = transform.position;

        //-----------------------------------------------
        //Checked Horizontal movement
        if (Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isSideWayRun", true);
            animator.SetBool("isCalm", false);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isSideWayRun", true);
            animator.SetBool("isCalm", false);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            animator.SetBool("isSideWayRun", false);
            animator.SetBool("isCalm", true);
        }
        //-----------------------------------------------
        //Checked Vertical movement
        if (Input.GetAxis("Vertical") < 0)
        {
            animator.SetBool("isInFronRun", true);
            animator.SetBool("isCalm", false);
            animator.SetBool("isBehindCalm", false);
            animator.SetBool("isBehindRun", false);
            
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("isBehindRun", true);
            animator.SetBool("isCalm", false);
            animator.SetBool("isBehindCalm", false);
            animator.SetBool("isInFronRun", false);
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("isBehindRun", false);
            animator.SetBool("isInFronRun", false);
        }

        currentPosition.x += Input.GetAxis("Horizontal") * speed;
        currentPosition.y += Input.GetAxis("Vertical") * speed;

        transform.position = currentPosition;
    }
}
