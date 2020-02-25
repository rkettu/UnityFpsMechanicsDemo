using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inheriting MonoBehaviour through GunBaseScript... maybe make GunBaseScript into an interface instead and inherit MB separately for all children?
public class gun1script : GunBaseScript
{
    public override void MyTestFunction()
    {
        Debug.Log("Calling test function of gun 1");
    }
}
