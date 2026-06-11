using UnityEngine;
using UnityEditor;

public class turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretTurnPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 2;
    [SerializeField] private float rotationSpeed = 100;
    [SerializeField] private float bps = 1f; // bullets per second

    private Quaternion defaultRotation;
    private Transform target;
    private float timeUntilFire;

    private void Start()
    {
        defaultRotation = turretTurnPoint.rotation;
    }

    private void Update()
    {
        FindTarget();

        if (target != null)
        {
            RotateTowardsTarget();

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }

            if (!CheckTargetIsInRange())
            {
                target = null;
            }
        }
        else
        {
            ReturnToDefaultRotation();
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(
            transform.position,
            targetingRange,
            (Vector2)transform.position,
            0f,
            enemyMask
        );

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(
            target.position.y - transform.position.y,
            target.position.x - transform.position.x
        ) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        turretTurnPoint.rotation = Quaternion.RotateTowards(
            turretTurnPoint.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void ReturnToDefaultRotation()
    {
        turretTurnPoint.rotation = Quaternion.RotateTowards(
            turretTurnPoint.rotation,
            defaultRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}