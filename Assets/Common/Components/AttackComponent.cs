using UnityEngine;
using System.Collections.Generic;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    private Vector2 facingDirection;
    public Vector2 GetFacingDirection()
    {
        return facingDirection;
    }
    public void UpdateFacingDirection(Vector2 newFacingDirection)
    {
        if (newFacingDirection == Vector2.zero)
        {
            return;
        }
        facingDirection = newFacingDirection;
    }


    
    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
    }


    public void RemoveWeapon(Weapon newWeapon)
    {
        weapons.Remove(newWeapon);
    }


    private Quaternion RotationFromDirection()
    {
        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;

        Quaternion projectileRotation = Quaternion.Euler(0f, 0f, angle); 

        return projectileRotation;
    }


    public void AutoAttackAll()
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.CanAttack())
            {
                weapon.Attack(transform.position, RotationFromDirection());
            }
        }
    }


    public void AttackSingleWeapon()
    {
        if (weapons[0].CanAttack())
        {
            weapons[0].Attack(transform.position, RotationFromDirection());
        }
    }
}
