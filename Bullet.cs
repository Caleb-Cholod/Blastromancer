using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 mousepos;
    public GameObject ShootingPt;

    public bool tobeDestroyed = false;

    private float timer = 0;
    private float bulletSpeed = 10f;
    int groundLayer;

    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        explosion.SetActive(false);
        this.gameObject.SetActive(true);

        transform.position = ShootingPt.transform.position;
        //get mouse position
        // Get the mouse position in the world
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0;
        mousepos = Camera.main.ScreenToWorldPoint(mousePosition);

        int groundLayer = LayerMask.NameToLayer("groundLayer");

        //transform.LookAt(mousepos);
        //Get angle to mouse using cotan
        float y = mousepos.y - transform.position.y;
        float x = mousepos.x - transform.position.x;
        float deg = (float)Math.Atan2(y, x);
        float angleInDegrees = deg * Mathf.Rad2Deg;
        Debug.Log(angleInDegrees);
        //-> 0
        //^ z + 90
        // <- z + 180
        transform.Rotate(0, 0, angleInDegrees);

        //Debug.Log(transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        Vector3 forw = new Vector3(Vector3.forward.x, Vector3.forward.y, Vector3.forward.z);

        //transform.Translate(transform.forward * bulletSpeed * Time.deltaTime);
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
        //transform.position += transform.right;//new Vector3(0.01f, 0f, 0f);
        //transform.position += new Vector3(bulletSpeed, 0f, 0f);
        //move towards mouse position

        if (timer >= 5 && tobeDestroyed)
        {
            //kill bullet
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject expl = Instantiate(explosion);
        expl.GetComponent<BulletExplosion>().tobeDestroyed = true;
        expl.transform.position = transform.position;
        expl.SetActive(true);
        // Check if the bullet collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Perform actions upon hitting an enemy (e.g., destroy enemy, apply damage)
            
            
            //hurt enemt
            //Destroy(collision.gameObject);
            collision.gameObject.GetComponent<EnemyShooter>().Hurt();

            //play an animation

            //destroy bullet
            Destroy(this.gameObject);

        }
        //If we are hitting ground
        if (collision.CompareTag("Ground"))
        {
            Debug.Log("hit groudn");
            Destroy(this.gameObject);
            //play an animation
        }
    }
}
