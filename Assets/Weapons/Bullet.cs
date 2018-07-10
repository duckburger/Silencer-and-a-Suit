using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    [SerializeField] float speed;
    [SerializeField] float despawnTime = 3f;
    [SerializeField] LayerMask hitMask;

    Vector3 posLastFrame = Vector3.zero;
    public Vector3 direction;

    private void Awake()
    {
        RaycastHit2D bulletFlightRaycast = Physics2D.Raycast(this.transform.position, direction, 0.1f, hitMask);
        if (bulletFlightRaycast.collider != null)
        {
            Debug.Log("Bullet hit something: " + bulletFlightRaycast.collider.gameObject.name);
            IDamageable damageableEntity = bulletFlightRaycast.collider.GetComponent<IDamageable>();
            if (damageableEntity != null)
            {
                damageableEntity.GetDamaged();
            }
            Destroy(gameObject);
            }
        }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(TimerForDespawn());  
    }

    IEnumerator TimerForDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        FlyAndCheckForCollisions();
    }

    void FlyAndCheckForCollisions()
    {
        this.transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        RaycastHit2D bulletFlightRaycast = Physics2D.Raycast(this.transform.position, direction, 0.2f, hitMask);
        Physics2D.Linecast(this.transform.position, direction * Time.deltaTime * 0.2f, hitMask);
        if (bulletFlightRaycast.collider != null)
        {
            Debug.Log("Bullet hit something: " + bulletFlightRaycast.collider.gameObject.name);
            IDamageable damageableEntity = bulletFlightRaycast.collider.GetComponent<IDamageable>();
            if (damageableEntity != null)
            {
                damageableEntity.GetDamaged();
            }
            DestroyImmediate(gameObject);

        }
    }
}
