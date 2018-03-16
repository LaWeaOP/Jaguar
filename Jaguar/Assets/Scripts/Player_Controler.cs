using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controler : MonoBehaviour
{
    #region Variables
    public static Player_Controler sharedInstance;
    public float moreVelocity;
    public float forceJump;
    public float boxwidth;
    public float boxHeight;
    public float fireDelay;
    public bool isGround;
    public bool isJumping;
    public bool canMove;
    public Transform feet;
    public Transform kunaiIz;
    public Transform kunaiDe;
    public Transform bulletIz;
    public Transform bulletDe;
    public LayerMask whatIsGround;
    private Rigidbody2D rb;
    private Vector3 checkPoint;
    private Vector3 mousePosition;
    Animator anim;
    public SpriteRenderer sr;

    float ca, co, h, timer;
    public float cos, sin, ang;
    #endregion

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameManager.sharedInstance.currentStateGame = GameState.inTheGame;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        CheckPoint();
        canMove = true;
    }
   

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.y > gameObject.transform.position.y)
        { co = mousePosition.y - gameObject.transform.position.y; }
        else { co = 0; }
        ca = mousePosition.x - gameObject.transform.position.x;
        h = Mathf.Sqrt(Mathf.Pow(ca, 2) + Mathf.Pow(co, 2));
        cos = (float)((double)ca / (double)h);
        sin = (float)((double)co / (double)h);
        ang = Mathf.Acos(cos) * 180 / Mathf.PI;
        if (ang < 90 && ang > 50)
        {
            ang = 50;
        }
        if (ang >= 90 && ang < 130)
        {
            ang = 130;
        }

        if (GameManager.sharedInstance.currentStateGame == GameState.inTheGame)
        {
            isGround = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxwidth, boxHeight), 90f, whatIsGround);
            float playerSpeed = Input.GetAxisRaw("Horizontal");
            playerSpeed = playerSpeed * moreVelocity;
            timer = timer + Time.deltaTime;
            if (canMove)
            {
                if (playerSpeed != 0)
                {
                    MoveHorizontal(playerSpeed);
                }
                else
                {
                    MoveStop();
                }
                if (Input.GetButtonDown("Jump"))
                {
                    PlayerJummp();
                }
                if (Input.GetButtonDown("Fire1") && timer >= fireDelay && Inventory.sharedInstance.bullets > 0)
                {
                    Fire();
                    timer = 0;
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(boxwidth, boxHeight, 0));
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
        }
        else if (other.gameObject.tag == "Bullet")
        {
            Physics2D.IgnoreCollision(other.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }

    }

    void MoveHorizontal(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        if (speed < 0)
            sr.flipX = true;
        else if (speed > 0)
            sr.flipX = false;
        if (!isJumping)
        {
            anim.SetInteger("State", 1);
        }
    }

    void MoveStop()
    {
        if (ang < 90) { sr.flipX = false; }
        else { sr.flipX = true; }
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("State", 0);
        }
    }

    void PlayerJummp()
    {
        Debug.Log(isGround);
        if (isGround)
        {
            isJumping = true;
            rb.AddForce(new Vector2(0, forceJump));
            anim.SetInteger("State", 2);
        }
    }

    void Fire()
    {
        if (sr.flipX)
        {
            Instantiate(bulletIz, kunaiIz.position, Quaternion.identity);
        }
        if (!sr.flipX)
        {
            Instantiate(bulletDe, kunaiDe.position, Quaternion.identity);
        }
    }

    public void CheckPoint()
    {
        checkPoint = gameObject.transform.position;
    }
}

