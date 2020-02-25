using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheriting MonoBehaviour through GunBaseScript... maybe make GunBaseScript into an interface instead and inherit MB separately for all children?
public class gun2script : GunBaseScript
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void MyTestFunction()
    {
        Debug.Log("Calling test function of gun 2");
    }
}
