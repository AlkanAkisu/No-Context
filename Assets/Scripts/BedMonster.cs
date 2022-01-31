using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class BedMonster : MonoBehaviour, IDamagable
{
    [SerializeField] private float zoomSeconds;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] UnityEvent onDied;

    [SerializeField, Foldout("Fall Attack")] private float localRotLeft;
    [SerializeField, Foldout("Fall Attack")] private float localRotRight;
    [SerializeField, Foldout("Fall Attack")] private Vector3 localPos;
    [SerializeField, Foldout("Fall Attack")] private float durationOfFallAttack;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float pushForce;
    private Transform _player;
    [SerializeField] private Ease ease;
    private Vector3 _startPos;
    [SerializeField] private float disappearDuration = 4f;

    [SerializeField] private float maxHealth;
    [ReadOnly] private float _currentHealth;

    private bool CanAttack { get; set; }
    private Vector2 ShootDir => (_player.position - muzzlePoint.position).normalized;

    private bool IsRight => transform.localScale.y < 0;

    private float Health
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth < 0)
                Die();
        }
    }

    private void Die()
    {
        onDied?.Invoke();
        Destroy(gameObject);
    }

    private void Awake()
    {

        CanAttack = false;
        _player = FindObjectOfType<CharacterController>().transform;
        _currentHealth = maxHealth;
    }
    [NaughtyAttributes.Button]
    public void ZoomInBedMonster()
    {
        if (CanAttack)
            return;
        FindObjectOfType<CameraChanger>().ChangeToSelected(CameraTypes.BedMonster);
        Invoke(nameof(BackToNormal), zoomSeconds);
    }
    private void BackToNormal()
    {
        FindObjectOfType<CameraChanger>().ChangeToSelected(CameraTypes.Forest1);
        CanAttack = true;
        StartCoroutine(AttackCycle());
    }
    [NaughtyAttributes.Button]
    private void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.identity);
        bullet.transform.GetComponent<Rigidbody2D>().velocity = bulletSpeed * ShootDir;
        var color = Color.white;
        color.a = 0.3f;
        bullet.transform.GetComponent<SpriteRenderer>().DOColor(color, disappearDuration);
        Destroy(bullet, disappearDuration);

    }
    [Button]
    private void FallAttack()
    {
        var localRot = Vector3.zero;
        localRot.z = IsRight ? localRotRight : localRotLeft;
        transform.DORotate(localRot, durationOfFallAttack).SetEase(curve);

        localPos = localPos.ChangeVector(z: localPos.x * (IsRight ? -1 : 1));
        _startPos = transform.position;
        DOTween.To(
            () => transform.position - _startPos,
            (vect) => transform.position = _startPos + vect,
            localPos,
            durationOfFallAttack
        )
        .SetEase(curve)
        .onComplete += ApplyFallAttackEffects;
    }

    private void ApplyFallAttackEffects()
    {
        if (FindObjectOfType<FallAttack>().IsplayerInArea)
        {
            var dir = IsRight ? 1 : -1;
            _player.GetComponent<Rigidbody2D>().AddForce(Vector2.right * pushForce * dir);
        }
        AudioManager.i.Play("FallAttack");
    }

    [Button]
    private void Reset()
    {
        transform.DOMove(_startPos, 0.2f);
        transform.DORotate(Vector3.zero.ChangeVector(z: 90), 0.2f);
    }

    private void Update()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        // throw new NotImplementedException();
    }

    IEnumerator AttackCycle()
    {
        yield return new WaitForSeconds(3f);
        int numberOfShoot = 4;
        while (CanAttack)
        {

            for (int i = 0; i < numberOfShoot; i++)
            {
                Shoot();
                yield return new WaitForSeconds(3f);
            }
            FallAttack();
            yield return new WaitForSeconds(2f);
            Reset();
            yield return new WaitForSeconds(2f);
        }



    }

    public void TakeDamage(float damage)
    {
        AudioManager.i.Play("Hit");

        Health -= damage;
    }
}
