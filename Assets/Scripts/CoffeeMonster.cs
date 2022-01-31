using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using System;
using UnityEngine.Events;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class CoffeeMonster : MonoBehaviour
{
    #region Serialize Fields
    [SerializeField] private float zoomSeconds;
    [SerializeField] Transform muzzlePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] UnityEvent onDied;

    [SerializeField] private float disappearDuration = 4f;
    [SerializeField] private float maxHealth;
    [SerializeField] private float degreePerSecond;
    [SerializeField] private Transform LeftPoint, RightPoint;

    [SerializeField] private float seconds;

    #endregion

    #region Private Fields

    private float currentHealth;
    private Transform player;
    private int currentPos;
    private TweenerCore<Vector3, Vector3, VectorOptions> rollAttackTween;


    #endregion

    #region Public Properties
    public bool CanAttack { get; private set; }
    public Vector2 ShootDir => (player.position - muzzlePoint.position).normalized;

    public bool IsRight => transform.localScale.y < 0;
    public float Health
    {
        get
        {
            return currentHealth;
        }
        private set
        {
            currentHealth = value;
            if (currentHealth < 0)
                Die();
        }
    }


    #endregion

    #region Dependencies


    #endregion




    private void Awake()
    {
        CanAttack = false;
        player = FindObjectOfType<CharacterController>().transform;
        currentHealth = maxHealth;
        currentPos = 1;
    }

    private void Update()
    {
        if (rollAttackTween == null)
            return;
        if (rollAttackTween.active)
        {
            var z = transform.eulerAngles.z;
            z += degreePerSecond * Time.deltaTime * currentPos;
            transform.eulerAngles = transform.eulerAngles.ChangeVector(z: z);
        }
        else
        {
            // transform.eulerAngles = transform.eulerAngles.ChangeVector(z: 0);
        }
    }
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

    [NaughtyAttributes.Button]
    private void RollAttack()
    {
        // transform.DORotate(Vector3.zero.ChangeVector(z: degreePerSecond), 1f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
        if (currentPos == 1)
            rollAttackTween = transform.DOMoveX(LeftPoint.position.x, seconds).SetEase(Ease.Linear);
        if (currentPos == -1)
            rollAttackTween = transform.DOMoveX(RightPoint.position.x, seconds).SetEase(Ease.Linear);
        rollAttackTween.onComplete += rollAttackFinished;

    }

    private void rollAttackFinished()
    {
        transform.eulerAngles = Vector3.zero;
        var newX = -transform.localScale.x;
        transform.localScale = transform.localScale.ChangeVector(x: newX);
        currentPos *= -1;
    }

    private void Die()
    {
        onDied?.Invoke();
        Destroy(gameObject);
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
            RollAttack();
            yield return new WaitForSeconds(2f);
        }

    }


}
