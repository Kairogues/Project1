using UnityEngine;
using System.Collections.Generic;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;

    private void Awake()
    {
        
    }
    
    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
    }

    public void RemoveWeapon(Weapon newWeapon)
    {
        weapons.Remove(newWeapon);
    }

    public void AutoAttackAll()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.CanAttack())
            {
                weapon.Attack(transform.position, transform.rotation);
            }
        }
    }
}
