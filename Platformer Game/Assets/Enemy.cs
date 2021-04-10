using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 10f;
    private bool isDie = false;
    [SerializeField]
    private Animator animator;
    bool isGoingLeft = false;
    bool isGoingRight = false;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {

        if(isGoingLeft)
        {
            animator.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0, 0, 0);
            rigid.MovePosition(rigid.position + (Vector2)transform.right * Time.deltaTime);
        }
        else if (isGoingRight)
        {
            animator.SetBool("isRunning", true);
            transform.eulerAngles = new Vector3(0, 180, 0);
            rigid.MovePosition(rigid.position + (Vector2)transform.right * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void Damage(float dmg)
    {
        health -= dmg;
            SoundManager.soundManager.PlayHitSound();
            animator.Play("Pig_Hit");
        if (health <= 0f)
        {
            isDie = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetBool("isDie", true);
            animator.SetBool("isDie", true);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }
    IEnumerator Move()
    {
        while(true)
        {
            isGoingRight = true;
            isGoingLeft = false;
            yield return new WaitForSeconds(3f);
            isGoingRight = false;
            isGoingLeft = false;
            yield return new WaitForSeconds(3f);
            isGoingRight = false;
            isGoingLeft = true;
            yield return new WaitForSeconds(3f);
        }
    }
}
