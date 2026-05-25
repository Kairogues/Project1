using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected bool canAttack = true;
    [SerializeField] protected AttackComponent attackComponent;

    public virtual void SetAttackComponent(AttackComponent attackComponent)
    {
        this.attackComponent = attackComponent;
    }

    public virtual void ChangeWeapon(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
    }

    public IEnumerator CoolingDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(weaponData.cooldown);
        canAttack = true;
    }

    public virtual void TryToAttack()
    {
        if (weaponData == null || canAttack == false)
        {
            return;
        }

        Attack();
    }

    public virtual void Attack()
    {
        UnityEngine.Debug.Log("Trying to attack with the Base Weapon...");
    }
}
