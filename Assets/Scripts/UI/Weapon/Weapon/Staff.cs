using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon, IWeapon
{
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;

    public override void StartAttack() {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, transform.rotation);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(GetWeaponInfo().weaponRange);
    }

    public override void DoneAttack()
    {
        IsAttacking = false;
    }

}
