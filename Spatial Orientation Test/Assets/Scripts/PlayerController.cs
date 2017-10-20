using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
    private Animator animator;

    public LayerMask blockingLayer;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;

    private bool playerMoving;
    private Vector2 lastMove;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        playerMoving = false;

        if (GameManager.instance.reset) Reset();
        if (!GameManager.instance.playersTurn) return;

        int hor =(int) Input.GetAxisRaw("Horizontal");
        int ver = (int)Input.GetAxisRaw("Vertical");

        float xDir = hor * moveSpeed * Time.deltaTime;
        float yDir = ver * moveSpeed * Time.deltaTime;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        boxCollider.enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {

            Move(hor, ver);
        }
    }

    void Move(int hor,int ver)
    {
        float xDir = hor * moveSpeed * Time.deltaTime;
        float yDir = ver * moveSpeed * Time.deltaTime;
        if (hor > 0.5f || hor < -0.5f)
        {
            transform.Translate(new Vector3(xDir, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(hor, 0f);
        }
        else if (ver > 0.5f || ver < -0.5f)
        {
            transform.Translate(new Vector3(0f, yDir, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f,ver);
        }

        animator.SetFloat("MoveX", hor);
        animator.SetFloat("MoveY", ver);
        animator.SetBool("Moving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Object")
        {
            int hor = (int)Input.GetAxisRaw("Horizontal");
            int ver = (int)Input.GetAxisRaw("Vertical");
            animator.SetFloat("LastMoveX", -hor);
            animator.SetFloat("LastMoveY", -ver);
            animator.SetBool("Moving", false);
            GameManager.instance.playersTurn = false;
            GameManager.instance.init = transform.position;
            GameManager.instance.draw = true;
        }
    }

    void Reset()
    {
        transform.position = new Vector3(0, 0, 0);
        animator.SetFloat("LastMoveX", -0);
        animator.SetFloat("LastMoveY", -1);
        animator.SetBool("Moving", false);
        GameManager.instance.playersTurn = false;
        GameManager.instance.reset = false;
    }
}
