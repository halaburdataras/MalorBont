using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public int extraJumps;
    public int extraJumpsValue;

    bool facingRight = true;

    bool touchDead;
    public LayerMask whatIsBad;

    bool grounded = false;
    public Transform groundCheck;
    public float groundRadius;
    public LayerMask whatIsGround;

    public float move;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;

    private Vector2 boxPos = new Vector2(0.01f, 0.81f);
    private Vector2 circlePos = new Vector2(0.01f, -1.22f);

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        touchDead = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsBad);

        move = Input.GetAxis("Horizontal");
    }
    
    void Update()
    {
        if (grounded)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps = extraJumpsValue;
            animator.SetBool("isJumping", false);

            this.gameObject.GetComponent<BoxCollider2D>().offset = boxPos;
            this.gameObject.GetComponent<CircleCollider2D>().offset = circlePos;
        }
        else
        {
            Vector2 newBoxPos = new Vector2(1.79f, 0.81f);
            Vector2 newCirclePos = new Vector2(1.65f, -1.22f);

            this.gameObject.GetComponent<BoxCollider2D>().offset = newBoxPos;
            this.gameObject.GetComponent<CircleCollider2D>().offset = newCirclePos;
            animator.SetBool("isJumping", true);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && extraJumps > 0) 
        {
            rb.velocity = (Vector2.up * jumpForce) * 1.5f;

            animator.SetTrigger("takeOf");

            extraJumps--;
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;

                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && extraJumps <= 0)
        {
            rb.gravityScale = 3;
        }
        else
        {
            rb.gravityScale = 8;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }

        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);

        animator.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("move_plat"))
        {
            this.transform.parent = col.transform;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("move_plat"))
        {
            this.transform.parent = null;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
