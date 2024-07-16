using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public bool tobeDestroyed;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tobeDestroyed)
        {
            timer += Time.deltaTime;
            if(timer >= 0.3f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
