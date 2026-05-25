using UnityEngine;

public class SingleProjectileWeapon : Weapon
{
    public override void Attack()
    {
        GameObject projectile = Instantiate(weaponData.projectile, transform.position + weaponData.offset, transform.rotation);
        attackComponent.RegisterAttack(this);
        UnityEngine.Debug.Log("Attacking from " + weaponData.weaponName);
        StartCoroutine(CoolingDown());
    }

    private void Update()
    {
        TryToAttack();
    }
}
