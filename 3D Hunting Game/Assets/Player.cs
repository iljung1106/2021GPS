using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerMoveSpeed = 5f;
    private CharacterController cc;
    private Camera mainCam;

    public Animator animator;

    public float gravity = -9.8f;
    float velocity = 0;
    public float jumpPower = 5f;

    private bool isOnGround = false;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        mainCam = Camera.main;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 0.2 0.0 0.8 의 배율로 허리를 돌려버리자

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        dir = mainCam.transform.TransformDirection(dir);
        float tmp = dir.magnitude;
        dir.y = 0;
        dir = dir.normalized * tmp;

        if(cc.collisionFlags == CollisionFlags.Below)
        {
            isOnGround = true;
            velocity = 0;
        }
        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            velocity = jumpPower * Time.deltaTime;
        }
        isOnGround = false;
        velocity += Time.deltaTime * gravity;

        dir.y = velocity;

        cc.Move(dir * playerMoveSpeed * Time.deltaTime);

        //transform.position += dir * playerMoveSpeed * Time.deltaTime;

        Vector3 tmpVector = mainCam.transform.eulerAngles;
        tmpVector.x = 0;
        tmpVector.z = 0;
        transform.eulerAngles = tmpVector;

        float headDir = mainCam.transform.eulerAngles.x;
        headDir = (headDir > 180) ? headDir - 360 : headDir;
        //360 ~ 270 , 0 ~ 90
        animator.SetFloat("Spine2", -(headDir / 180f) + 0.5f);
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
    }
}
