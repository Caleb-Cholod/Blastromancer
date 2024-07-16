using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCollisionLayers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int bulletLayer = LayerMask.NameToLayer("Bullets");
        int groundLayer = LayerMask.NameToLayer("groundLayer");

        Debug.Log("Layers: " + bulletLayer + " - " + groundLayer);

        Physics2D.IgnoreLayerCollision(bulletLayer, groundLayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
