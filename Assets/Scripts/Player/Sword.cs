using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Transform _weaponCollider;

    public override void StartAttack()
    {
        _weaponCollider.gameObject.SetActive(true);
    }
    public override void DoneAttack()
    {
        IsAttacking = false;
        _weaponCollider.gameObject.SetActive(false);
    }
}
