using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public int itemID;
    private bool slerpingCore = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (slerpingCore)
        {
            float step = 1f * Time.deltaTime; // Calculate distance to move
            transform.position = Vector3.Slerp(transform.position, new Vector3(85.5f, 1.14f, 0f), step);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with an enemy
        if (collision.CompareTag("Player"))
        {
            Debug.Log("picked up");
            this.gameObject.SetActive(false);

            //Give player whatever they just got
            if(itemID == 0)
            {
                collision.gameObject.GetComponent<Player>().giveGun();
            }
            if(itemID == 1)
            {
                collision.gameObject.GetComponent<Player>().CoresCollected += 1;
            }
        }

    }

    public void DropCore()
    {
        this.gameObject.SetActive(true);
        slerpingCore = true;

    }
}