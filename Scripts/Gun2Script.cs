using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun2Script : WeaponBaseClass
{
    void Start()
    {
        ammoClipMax = 50;
        ammoClipLeft = 50;
        ammoHeldMax = 0;
        ammoHeldLeft = 0;
    }
    public override void ShootPrimary()
    {
        ammoClipLeft--;
    }
    public override void ShootSecondary()
    {
    }
    public override void Reload()
    {
    }
    protected override void ChildInitActions()
    {
    }
    public override void PrimaryFireKeyUp()
    {
    }
    public override void SecondaryFireKeyUp()
    {
    }
}
