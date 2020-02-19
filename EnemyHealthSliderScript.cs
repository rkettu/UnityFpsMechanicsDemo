using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSliderScript : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Makes enemy healthbars always face the player
        transform.LookAt(player.position);
    }
}
