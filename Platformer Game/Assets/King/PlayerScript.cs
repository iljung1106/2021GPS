using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float jumpPower = 5.0f;

    [SerializeField]
    private Transform groundChecker;
    [SerializeField]
    private float groundRadius = 0.2f;
    [SerializeField]
    private LayerMask GroundLayer;

    private Rigidbody2D rigid;
    private bool isGround = true;
    private Animator animator;

    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private float cameraSpeed = 5f;

    private bool isAttacking = false;

    [SerializeField]
    private float attackCoolTime = 0.1f;

    public float damage = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            StartCoroutine(AttackTimerStart());
            //animator.Play("King_Attack");
            animator.SetBool("isAttacking", true);
            SoundManager.soundManager.PlayAttackSound();
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position + transform.right, 1, (1 << LayerMask.NameToLayer("Enemy")));

            foreach (Collider2D c in col)
            {
                c.GetComponent<Enemy>().Damage(damage);
            }
        }
    }

    IEnumerator AttackTimerStart()
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackCoolTime);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isGround = Physics2D.OverlapCircle(groundChecker.position, groundRadius, GroundLayer);
        Move();
        Jump();
        mainCamera.position = Vector2.Lerp(mainCamera.position, transform.position, Time.deltaTime * cameraSpeed);
    }
    

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGround)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    void Move()
    {
        float posX = Input.GetAxis("Horizontal");
        if (posX != 0)
        {
            if(posX > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (isGround && !isAttacking)
            {
                animator.SetBool("isRunning", true);
                animator.SetBool("isFalling", false);
                //animator.Play("King_Run");
            }
        }
        else
        {
            if (isGround && !isAttacking)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isFalling", false);
                //animator.Play("King_Idle");
            }
        }

        if (!isGround && !isAttacking)
        {
            if (rigid.velocity.y >= 0)
            {
                //animator.Play("King_Jump");
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            else
            {
                //animator.Play("King_Fall");
                //animator.SetBool(1, true);
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }

        //if (!isAttacking)
       // {
            transform.Translate(Mathf.Abs(posX) * Vector3.right * moveSpeed * Time.fixedDeltaTime);
        //}

    }

}
