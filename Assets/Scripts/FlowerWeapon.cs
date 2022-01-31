using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerWeapon : MonoBehaviour
{
    [SerializeField] GameObject flowerBulletPrefab;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform character;

    [SerializeField] private float shootPerSecond;

    private void Start()
    {
        Invoke(nameof(StartShooting), 2f);
    }


    [NaughtyAttributes.Button]
    private void StartShooting()
    {
        InvokeRepeating(nameof(Shoot), 0f, 1 / shootPerSecond);
    }
    [NaughtyAttributes.Button]
    private void StopShooting()
    {
        CancelInvoke(nameof(Shoot));
    }


    public void Shoot()
    {
        var spawnPoint = muzzlePoint.position;
        var bulletTransform = Instantiate(flowerBulletPrefab, spawnPoint, Quaternion.identity);
        int dir = -character.transform.localScale.x.Sign();
        bulletTransform.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed * dir;
    }

    private void OnDisable()
    {
        StopShooting();
    }
}
