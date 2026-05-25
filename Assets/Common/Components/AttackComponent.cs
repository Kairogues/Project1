using UnityEngine;
using System.Collections.Generic;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private List<Weapon> weapons;
    private Queue<Weapon> attackQueue = new Queue<Weapon>();

    private void Awake()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.SetAttackComponent(this);
        }
    }
    
    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        newWeapon.SetAttackComponent(this);
    }

    public void RemoveWeapon(Weapon newWeapon)
    {
        weapons.Remove(newWeapon);
    }

    public void RegisterAttack(Weapon weapon)
    {
        attackQueue.Enqueue(weapon);
    }

    public void AutoAttack()
    {
        if (attackQueue.Count > 0)
        {
            Weapon currentWeapon = attackQueue.Dequeue();
            if (weapons.Contains(currentWeapon))
            {
                currentWeapon.TryToAttack();
            }   
        }
    }

    private void Update()
    {
        AutoAttack();
    }
}
