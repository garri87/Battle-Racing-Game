
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab; //Model

    public enum BulletType
    {
        bullet,
        ray,
        drop,
    }
    public BulletType bulletType;
    public float bulletSpeed = 5f; //Velocidad de desplazamiento del objeto
    public float maxRayLenght = 5f; //el eje Z del gameobject determina el largo del rayo
    private float currentRayLenght; //largo actual del rayo
    public float bulletLifeTime = 2f;//tiempo de vida de la bala antes de desactivarse
    private float bulletLifeTimer;
    public float damage; //Daño del objeto
    private SphereCollider _collider;
    private TrailRenderer _trailRenderer;
    private Transform objectPoolTransform; //Referencia a la posición del pool de objetos

    public int maxRayPenetration = 4; //Numero máximo de objetos que puede penetrar el rayo
    private RaycastHit[] rayHits; //Arreglo de impactos del rayo
    public LayerMask hitLayers; //Capas que detecta el objeto a lanzar


    private void Awake()
    {
        rayHits = new RaycastHit[maxRayPenetration];
        objectPoolTransform = GameManager.Instance.objectPool.transform;
        _trailRenderer = GetComponent<TrailRenderer>();

    }

    private void OnEnable()
    {

        if (bulletType == BulletType.ray)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0.1f); // Seteamos el rayo a su tamaño inicial al activarse
        }
        if (bulletType == BulletType.drop)
        {
            transform.parent = null;
        }
        _trailRenderer.enabled = true;
        _trailRenderer.Clear();
    }

    private void Start()
    {

    }

    private void Update()
    {
        switch (bulletType)
        {
            case BulletType.bullet:
                bulletLifeTimer -= Time.deltaTime;

                if (bulletLifeTimer <= 0)
                {

                    this.gameObject.SetActive(false);

                    transform.parent = objectPoolTransform;

                }
                else
                {
                    Vector3 direction = Vector3.forward;
                    ShotBullet(direction, bulletSpeed);
                }
                break;

            case BulletType.ray:

                currentRayLenght = transform.localScale.z;

                while (currentRayLenght < maxRayLenght) //alargar el rayo hasta su punto maximo
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + bulletSpeed * Time.deltaTime);
                }
                if (currentRayLenght > maxRayLenght)//Mantener el largo maximo si se excede del limite
                {
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, maxRayLenght);
                }

                Ray ray = new Ray(transform.position, transform.forward * transform.localScale.z);
                Debug.DrawRay(ray.origin, ray.direction, Color.red);

                if (Physics.RaycastNonAlloc(ray.origin, ray.direction * transform.localScale.z, rayHits, transform.localScale.z, hitLayers) > 0)
                {
                    if (rayHits != null)
                    {
                        foreach (RaycastHit hit in rayHits)
                        {
                            CheckHit(hit.collider.tag);
                        }
                    }
                }


                break;
            case BulletType.drop:
                bulletLifeTimer -= Time.deltaTime;

                if (bulletLifeTimer <= 0)
                {
                    this.gameObject.SetActive(false);

                }
                break;
            default:

                break;
        };

    }

    private void OnDisable()
    {
        _trailRenderer.enabled = false;
        transform.position = objectPoolTransform.position;
        bulletLifeTimer = bulletLifeTime;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 0);
    }

    private void ShotBullet(Vector3 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckHit(string tag)
    {
        switch (tag)
        {
            case "Default":

                break;

            case "Player":

                break;

            case "Destructible":

                break;

            default:
                break;
        }

        switch (bulletType)
        {
            case BulletType.bullet:

                break;

            case BulletType.ray:

                break;

            case BulletType.drop:

                break;


            default:
                break;
        }

        if (bulletType == BulletType.bullet || bulletType == BulletType.drop)
        {
            gameObject.SetActive(false);
        }
    }

    private void Explode(float radius)
    {

    }

}
