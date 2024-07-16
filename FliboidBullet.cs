using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FliboidBullet : MonoBehaviour
{

    public bool tobeDestroyed = false;

    private float timer = 0;
    private float homingTimer = 0f;
    public float bulletSpeed = 2f;

    public int bulletType;

    private GameObject player;

    Rigidbody2D rb;

    private float angleInDegrees = 0f;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletType == 0)
        {
            timer += Time.deltaTime;


            transform.position += transform.right * bulletSpeed * Time.deltaTime;
            //move towards mouse position

            if (timer >= 7 && tobeDestroyed)
            {
                //kill bullet
                Destroy(this.gameObject);
            }
        }

        else if (bulletType == 1)
        {
            timer += Time.deltaTime;
            homingTimer += Time.deltaTime;

            transform.position += transform.right * bulletSpeed * Time.deltaTime;
            //move towards mouse position
            if(homingTimer >= 1)
            {
                homingTimer = 0;

                float y = player.transform.position.y - transform.position.y;
                float x = player.transform.position.x - transform.position.x;
                float deg = (float)Math.Atan2(y, x);
                float temp = angleInDegrees;

                angleInDegrees = deg * Mathf.Rad2Deg;

                float newangle = angleInDegrees - temp;
                //Debug.Log("Firing at " + angleInDegrees);
                //transform.rotation = Quaternion
                transform.Rotate(0, 0, newangle);

            }
            if (timer >= 7 && tobeDestroyed)
            {
                //kill bullet
                Destroy(this.gameObject);
            }
        }
    }

    public void Target(GameObject player_)
    {
        player = player_;

        float y = player.transform.position.y - transform.position.y;
        float x = player.transform.position.x - transform.position.x;
        float deg = (float)Math.Atan2(y, x);
        angleInDegrees = deg * Mathf.Rad2Deg;
        //Debug.Log("Firing at " + angleInDegrees);
        //transform.rotation = Quaternion
        transform.Rotate(0, 0, angleInDegrees);

        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with an enemy
        if (collision.CompareTag("Player"))
        {
            // Handle collision with enemy
            Debug.Log("player hit by bullet");
            player.GetComponent<Player>().Hurt();
            Destroy(this.gameObject);
            //Destroy(collision.gameObject); // Destroy enemy
            //Destroy(gameObject); // Destroy bullet
        }
        if (collision.CompareTag("Ground"))
        {
            //Destroy(this.gameObject);
            
            rb.AddForce(new Vector2(0f, 30f));
            //play an animation
        }

    }

}
