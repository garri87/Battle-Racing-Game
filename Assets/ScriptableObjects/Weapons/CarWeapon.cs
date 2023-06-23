using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Weapon", menuName = "Weapons/New Weapon")]

public class CarWeapon : ScriptableObject
{
    public int weaponID;
   public enum WeaponType
   {
    Bomb,
    MachineGun,
    SniperRifle,
    FreezingRay,
    GrapplingHook,
    FlameThrower,
    OilDropper,
   }

   

   public WeaponType weaponType;
   
   public int damage;
   public float fireRate;
   public int maxAmmo;
   public GameObject weaponPrefabFront;
   public GameObject weaponPrefabLeft;
   public GameObject weaponPrefabRight;
   public GameObject weaponPrefabRear;

   public GameObject bulletPrefab; 
   public AudioClip shotSound;
   public Sprite weaponImage;

   public Material bulletMaterial;

}
