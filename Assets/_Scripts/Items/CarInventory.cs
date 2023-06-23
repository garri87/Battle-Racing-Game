using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the car equipment and weapons
/// </summary>
public class CarInventory : MonoBehaviour
{
    public Car carScriptable;
    public CarWeapon carWeaponScriptable;

    private GameObject carPrefab;
    private GameObject carModel;


    private CarWeapon.WeaponType weaponType;

    private float timer;

    private int currentAmmo;
    private int maxAmmo;
    private float fireRate = 1f;

    private WeaponTransforms modelTransforms;
    public Transform frontAttachment;
    private GameObject frontWeaponGO;
    private Transform frontMuzzle;
    public Transform leftAttachment;
    private GameObject leftWeaponGO;
    private Transform leftMuzzle;

    public Transform rightAttachment;
    private GameObject rightWeaponGO;
    private Transform rightMuzzle;

    public Transform rearAttachment;
    private GameObject rearWeaponGO;
    private Transform rearMuzzle;




    private List<Transform> weaponAttachments;
    private List<Transform> weaponMuzzles;

    public bool isRayWeapon;
    public bool isDropperWeapon;
    public bool isBallisticWeapon;
    public GameObject rayModel;
    public MeshRenderer rayRenderer;
    private Bullet rayBullet;


    private CarController _carController;
    private ObjectPool objectPool;

    public bool isWeaponEquipped;

    public int carMeleeDamage;

    private string bulletTag = "";


    private void Start()
    {
        _carController = GetComponent<CarController>();
        carScriptable = _carController.carScriptable;
        carModel = SpawnCar(carScriptable);
        carModel.transform.parent = transform;

        objectPool = GameManager.Instance.objectPool;
        weaponAttachments = new List<Transform>
        {
            frontAttachment,
            leftAttachment,
            rightAttachment,
            rearAttachment
        };
        weaponMuzzles = new List<Transform>
        {
            frontMuzzle,
            leftMuzzle,
            rightMuzzle,
            rearMuzzle
        };

        if (carWeaponScriptable)
        {
            LoadWeapon(carWeaponScriptable);
        }
        timer = fireRate;


        rayBullet = rayModel.GetComponent<Bullet>();
    }

    private void Update()
    {

        timer -= Time.deltaTime;

        if (Input.GetKey(_carController.fireKey) && timer <= 0)
        {

            foreach (Transform muzzle in weaponMuzzles)
            {
                if (muzzle != null)
                {
                    FireWeapon(weaponType, muzzle);
                }
            }
            timer = fireRate;
        }
        else
        {
            rayModel.transform.localScale = new Vector3(rayModel.transform.localScale.x, rayModel.transform.localScale.y, rayModel.transform.localScale.z - Time.deltaTime);

            if (rayModel.transform.localScale.z <= 0)
            {
                rayModel.SetActive(false);
            }
        }
    }

    public void LoadWeapon(CarWeapon scriptableObject)
    {
        fireRate = scriptableObject.fireRate;
        weaponType = scriptableObject.weaponType;

        isRayWeapon = weaponType == CarWeapon.WeaponType.FreezingRay
        || weaponType == CarWeapon.WeaponType.GrapplingHook
        || weaponType == CarWeapon.WeaponType.FlameThrower;

        isBallisticWeapon = weaponType == CarWeapon.WeaponType.MachineGun
        || weaponType == CarWeapon.WeaponType.SniperRifle;

        isDropperWeapon = weaponType == CarWeapon.WeaponType.Bomb
        || weaponType == CarWeapon.WeaponType.OilDropper;

        if (isRayWeapon)
        {
            rayRenderer.material = scriptableObject.bulletMaterial;
        }

        if (scriptableObject.weaponPrefabFront)
        {
            frontWeaponGO = SpawnWeapon(scriptableObject.weaponPrefabFront, frontAttachment);
            frontMuzzle = frontWeaponGO.GetComponentInChildren<WeaponTransforms>().muzzleTransform;
        }
        else
        {
            frontMuzzle = null;

        }

        if (scriptableObject.weaponPrefabLeft)
        {
            leftWeaponGO = SpawnWeapon(scriptableObject.weaponPrefabLeft, leftAttachment);
            leftMuzzle = leftWeaponGO.GetComponentInChildren<WeaponTransforms>().muzzleTransform;

        }
        else
        {
            // leftMuzzle = null;
        }
        if (scriptableObject.weaponPrefabRight)
        {
            rightWeaponGO = SpawnWeapon(scriptableObject.weaponPrefabRight, rightAttachment);
            rightMuzzle = rightWeaponGO.GetComponentInChildren<WeaponTransforms>().muzzleTransform;

        }
        else
        {
            //rightMuzzle = null;
        }
        if (scriptableObject.weaponPrefabRear)
        {
            rearWeaponGO = SpawnWeapon(scriptableObject.weaponPrefabRear, rearAttachment);
            rearMuzzle = rearWeaponGO.GetComponentInChildren<WeaponTransforms>().muzzleTransform;
        }
        else
        {
            rearMuzzle = null;
        }

        weaponMuzzles = new List<Transform>
        {
            frontMuzzle,
            leftMuzzle,
            rightMuzzle,
            rearMuzzle
        };
        GameObject SpawnWeapon(GameObject weaponGO, Transform attachment)
        {
            if (attachment.childCount > 0)
            {
                foreach (Transform child in attachment.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            GameObject instWeapon = Instantiate(weaponGO, attachment.position, attachment.rotation, attachment);

            return instWeapon;
        }
    }



    /// <summary>
    /// Instantiates car model from scriptable object and get attachments transforms
    /// </summary>
    /// <param name="scriptableObject"></param>
    /// <returns></returns>
    public GameObject SpawnCar(Car scriptableObject)
    {
        GameObject car;
        if (scriptableObject)
        {
            car = Instantiate(scriptableObject.carPrefab, transform.position, transform.rotation, transform);
            modelTransforms = car.GetComponent<WeaponTransforms>();
            frontAttachment = modelTransforms.frontAttachment;
            leftAttachment = modelTransforms.leftAttachment;
            rightAttachment = modelTransforms.rightAttachment;
            rearAttachment = modelTransforms.rearAttachment;
            return car;
        }
        else
        {
            return car = null;
        }
    }

    public void FireWeapon(CarWeapon.WeaponType weaponType, Transform muzzleTransform)
    {

        switch (weaponType)
        {
            case CarWeapon.WeaponType.MachineGun:
            case CarWeapon.WeaponType.SniperRifle:
                GetBullet("Bullet");
                break;

            case CarWeapon.WeaponType.FlameThrower:
            case CarWeapon.WeaponType.FreezingRay:
            case CarWeapon.WeaponType.GrapplingHook:
                //TODO: RAY ATTACK
                rayModel.SetActive(true);
                break;

            case CarWeapon.WeaponType.Bomb:
                GetBullet("Bomb");
                break;
            case CarWeapon.WeaponType.OilDropper:
                GetBullet("OilDrop");
                break;
        }

        void GetBullet(string tag)
        {

            GameObject bullet = objectPool.GetPooledObject(tag);

            bullet.transform.position = muzzleTransform.position;
            bullet.transform.rotation = muzzleTransform.rotation;
            // bullet.transform.parent = null;
            bullet.SetActive(true);
        }


    }

}
