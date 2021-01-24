using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    public int speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isBehindRun = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        

        //-----------------------------------------------
        //Checked Horizontal movement
        if (Input.GetAxis("Horizontal") < 0)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isSideWayRun", true);
            animator.SetBool("isCalm", false);
            isBehindRun = false;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isSideWayRun", true);
            animator.SetBool("isCalm", false);
            isBehindRun = false;
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
            isBehindRun = false;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            animator.SetBool("isBehindRun", true);
            animator.SetBool("isCalm", false);
            animator.SetBool("isBehindCalm", false);
            animator.SetBool("isInFronRun", false);
            isBehindRun = true;
        }
        else if (Input.GetAxis("Vertical") == 0)
        {
            animator.SetBool("isBehindCalm", isBehindRun);
            animator.SetBool("isCalm", !isBehindRun);
            animator.SetBool("isBehindRun", false);
            animator.SetBool("isInFronRun", false);
        }

        Vector3 currentPosition = transform.position;
        currentPosition.x += Input.GetAxis("Horizontal") * speed;
        currentPosition.y += Input.GetAxis("Vertical") * speed;
        transform.position = currentPosition;
    }
}
