using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{
    [SerializeField] private float maxLifeTime;
    [SerializeField] private float damage;

    [SerializeField]
   
    void Start()
    {
        transform.eulerAngles = transform.eulerAngles.ChangeVector(z: Random.Range(0, 360));
        Destroy(gameObject, maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.GetComponent<IDamagable>()?.TakeDamage(damage);
        Destroy(gameObject);
    }
}
