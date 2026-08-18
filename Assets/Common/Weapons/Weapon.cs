using UnityEngine;

[System.Serializable]
public class Weapon
{
    [SerializeField] private WeaponData weaponData;
    // [SerializeField] private bool canAttack = true;
    private float nextCanAttackTime;



    public Weapon (WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
        nextCanAttackTime = Time.time;
    }


    public void ChangeWeapon(WeaponData newWeaponData)
    {
        weaponData = newWeaponData;
        nextCanAttackTime = Time.time;
    }


    //public IEnumerator CoolingDown()
    //{
    //    canAttack = false;
    //    yield return new WaitForSeconds(weaponData.cooldown);
    //    canAttack = true;
    //}


    public bool CanAttack()
    {
        if (Time.time <= nextCanAttackTime)
        {
            return false;
        }

        return true;
    }


    public void Attack(Vector3 initPosition, Quaternion initRotation)
    {
        if (weaponData == null)
        {
            return;
        }

        // Quaternion spawnRotation = Quaternion.Euler(initRotation);
        Object.Instantiate(weaponData.projectile, initPosition + weaponData.offset, initRotation);
        nextCanAttackTime = Time.time + weaponData.cooldown;
        //StartCoroutine(CoolingDown());
    }
}
