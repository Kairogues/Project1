using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] public string weaponName;
    [SerializeField] public GameObject projectile;
    [SerializeField] public float cooldown;
    [SerializeField] public Vector3 offset;
}
