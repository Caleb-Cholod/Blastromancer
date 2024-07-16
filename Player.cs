using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 3;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;

    float moveInput;
    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;

    public float shotSpeed = 1f;
    public float shotTimer = 0.0f;

    private bool isGrounded;
    public Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    public GameObject bullet;
    public GameObject bigBullet;
    private GameObject bull;

    //checkpoints
    private int checkpointInt = 0;
    public GameObject chk1;

    private float mouseChargeTimer = 2f;
    private bool isWeaponCharged = false;
    private bool isWeaponCharging = false;

    private bool isBoosting = false;
    private Vector2 boostingVel;
    private float BoostingMoveTimer = 0.3f;

    public int CoresCollected = 1;
    private bool hasGun = false;
    public GameObject gunModel;

    private bool isDashing = false;
    private float dashingTimer = 1f;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
        gunModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        dashingTimer -= Time.deltaTime;
        // Check if the character is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        moveInput = Input.GetAxis("Horizontal");

        //limit speed
        if (isGrounded)
        {
            isBoosting = false;
            
        }

        if (!isBoosting)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            Vector2 currVel = rb.velocity;
        }
        else
        {
            BoostingMoveTimer -= Time.deltaTime;
            if(BoostingMoveTimer >= 0)
            {
                boostingVel = rb.velocity;    
            }
            else
            {
                Vector2 currVel = rb.velocity;
                rb.velocity = currVel + new Vector2(.05f * moveInput * moveSpeed, 0);
                //if boostingvel.x is less than 5(walking speed), lets add to it
                /*if(boostingVel.x > 0 && boostingVel.x < 5)
                {
                    boostingVel.x += moveInput * .05f;
                }
                if (boostingVel.x < 0 && boostingVel.x > -5)
                {
                    boostingVel.x -= moveInput * .05f;
                }*/


                //limit x velocity
                if (boostingVel.x > 0)
                {
                    if (rb.velocity.x > boostingVel.x)
                    {
                        rb.velocity = new Vector2(boostingVel.x, rb.velocity.y);
                    }
                    if (rb.velocity.x < 0 - boostingVel.x)
                    {
                        rb.velocity = new Vector2(0 - boostingVel.x, rb.velocity.y);
                    }
                }

                if (boostingVel.x < 0)
                {
                    if (rb.velocity.x < boostingVel.x)
                    {
                        rb.velocity = new Vector2(boostingVel.x, rb.velocity.y);
                    }
                    if (rb.velocity.x > 0 - boostingVel.x)
                    {
                        rb.velocity = new Vector2(0 - boostingVel.x, rb.velocity.y);
                    }
                }

                //regular is 5 and -5
                //if(currVel.x <= 5f && currVel.y <= 5f)
                //{
                //    isBoosting = false;
                //}
                Debug.Log(boostingVel.x);
                //rb.velocity += new Vector2(moveInput * moveSpeed, 0f);
            }   
        }
        

        

        //Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");

            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        //Dashing
        if (Input.GetKeyDown(KeyCode.Q) && dashingTimer <= 0f)
        {
            //leftdash
            Debug.Log("dashing");

            rb.AddForce(new Vector2(-500f, 0f), ForceMode2D.Impulse);
            isDashing = true;
            dashingTimer = 1f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //rightdash

        }

        //Shooting
        if (hasGun)
        {
            if (shotTimer > shotSpeed && Input.GetMouseButton(0))
            {
                isWeaponCharging = true;
                mouseChargeTimer -= Time.deltaTime;

            }
            if (isWeaponCharging)
            {
                //Firing big bullet
                if (CoresCollected >= 1 && Input.GetMouseButtonUp(0) && mouseChargeTimer <= 0)
                {
                    //fire bullet
                    Debug.Log("fire big bullet");
                    bull = Instantiate(bigBullet);
                    bull.SetActive(true);
                    bull.GetComponent<Bullet>().tobeDestroyed = true;

                    //Add force to the player in the direction opposite the mouse
                    Vector3 mousePosition = Input.mousePosition;
                    mousePosition.z = 0;
                    Vector3 mousepos = Camera.main.ScreenToWorldPoint(mousePosition);

                    Debug.Log("mouseposs " + mousepos);

                    //Get angle to mouse using cotan
                    float y = mousepos.y - transform.position.y;
                    float x = mousepos.x - transform.position.x;
                    float deg = (float)Math.Atan2(y, x);
                    float angleInDegrees = deg * Mathf.Rad2Deg;
                    Debug.Log("ANGLE: " + angleInDegrees);

                    //convert angle to x and y counterparts for force
                    float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
                    float xValue = Mathf.Cos(angleInRadians);
                    float yValue = Mathf.Sin(angleInRadians);

                    //multiply x compnent
                    //xValue = 10 * xValue;

                    //let us go zoom
                    
                    isBoosting = true;
                    

                    Debug.Log("y = " + yValue + " x = " + xValue);

                    Vector2 shotForce = new Vector2(xValue, yValue);

                    rb.AddForce(20f * -1 * shotForce, ForceMode2D.Impulse);
                    boostingVel = rb.velocity;
                    //apply correct x value
                    //Vector2 curVel = rb.velocity;

                    //curVel.x += 5f * xValue;
                    //rb.velocity = curVel;

                    //Reset shot values
                    mouseChargeTimer = 1.5f;
                    isWeaponCharging = false;
                }
                //firing normal bullet
                else if (Input.GetMouseButtonUp(0))
                {
                    //fire bullet
                    Debug.Log("normal shot");
                    bull = Instantiate(bullet);
                    bull.SetActive(true);
                    bull.GetComponent<Bullet>().tobeDestroyed = true;

                    //Reset shot values
                    shotTimer = 0;
                    mouseChargeTimer = 2f;
                    isWeaponCharging = false;
                }
            }
        }


        //Checkpoint checking
        if(transform.position.x >= chk1.transform.position.x)
        {
            checkpointInt = 1;
        }


    }

    public void giveGun()
    {
        hasGun = true;
        gunModel.SetActive(true);
    }
    public void Hurt()
    {
        hp -= 1;
        if(hp == 2)
        {
            hp3.SetActive(false);
        }
        else if (hp == 1)
        {
            hp2.SetActive(false);
        }
        else if (hp <= 0)
        {
            hp1.SetActive(false);

            //YOU DIED
            hp = 3;
            hp1.SetActive(true);
            hp2.SetActive(true);
            hp3.SetActive(true);
            //reset to checkpoint
            switch (checkpointInt)
            {
                case 0:
                    transform.position = new Vector3(-24f, 0f, 0f);
                    break;

                case 1:
                    transform.position = chk1.transform.position;
                    break;
            }



        }
    }
}
