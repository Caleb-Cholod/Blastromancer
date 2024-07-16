using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public int hp;
    private float shootingTimer = 2f;
    public GameObject player;
    private float detectionRadius = 10f;

    public GameObject bullet;
    private GameObject bul;
    private bool playerSeen = false;

    public int Variant;
    public GameObject drop;

    public GameObject gate;

    // Start is called before the first frame update
    void Start()
    {
        if(Variant == 0)
        {
            hp = 2;
        }
        else if(Variant == 1)
        {
            hp = 7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Math.Abs(transform.position.x - player.transform.position.x) < 10f)
        {
            playerSeen = true;
        }
        else
        {
            playerSeen = false;
        }


        //shoot bullets
        if (playerSeen)
        {
            shootingTimer -= Time.deltaTime;
            if(shootingTimer <= 0f)
            {
                if (Variant == 0)
                {
                    shootingTimer = 4f;

                    bul = Instantiate(bullet);
                    //set bullet position
                    bul.transform.position = transform.position;
                    bul.SetActive(true);

                    bul.GetComponent<FliboidBullet>().Target(player);
                    bul.GetComponent<FliboidBullet>().tobeDestroyed = true;
                }
                else if(Variant == 1)
                {
                    shootingTimer = 4f;

                    bul = Instantiate(bullet);
                    //set bullet position
                    bul.transform.position = transform.position;
                    bul.SetActive(true);

                    bul.GetComponent<FliboidBullet>().Target(player);
                    bul.GetComponent<FliboidBullet>().tobeDestroyed = true;
                    bul.GetComponent<FliboidBullet>().bulletType = 1;
                    bul.GetComponent<FliboidBullet>().bulletSpeed = 2.5f;

                }

            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Handle collision with enemy
            Debug.Log("EnemyBul");
            //Destroy(collision.gameObject); // Destroy enemy
            //Destroy(gameObject); // Destroy bullet
        }
    }

    public void Hurt()
    {
        hp -= 1;
        if(hp <= 0)
        {
            if (drop)
            {
                drop.SetActive(true);
                
                drop.GetComponent<Pickup>().DropCore();
            }

            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            UnlockGate();   
        }
    }



    public void UnlockGate()
    {
        gate.SetActive(false);
    }

}
