using System;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private GameObject aimLaserPrefab;
    [SerializeField]
    private float laserFireDelay;
    [SerializeField]
    private float laserDestroyTime;
    [SerializeField]
    private Transform endPointParentPos;

    [HideInInspector]
    public Vector3 fireDirection;

    private Animator animator;
    private RaycastHit2D hit;
    private Vector3 _playerPos;
    private bool laserOn = false;

    // On Awake, Instantiate aim laser
    private void Awake() 
    {
        var obj = Instantiate(aimLaserPrefab, transform.position, Quaternion.identity);
        obj.GetComponent<AimLaser>().laserScript = this;
        animator = GetComponent<Animator>();
    }

    private void Update() 
    {
        // Will only call raycast when laser is fired from aim script (saves performance)
        if (laserOn)
        {
            hit = Physics2D.Raycast(transform.position, fireDirection, 30, 3); 

            // If object is hit, and its tag is player, get its health components and call damage function
            if (hit && hit.transform.gameObject.CompareTag("Player"))
            {
                hit.transform.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            }
        }
    }

    // After time ends for aim laser, then player angle is returned and laser is fire
    public void FireLaser(Vector3 playerPos)
    {
        fireDirection = playerPos - transform.position;
        _playerPos = playerPos;

        Invoke("CastLaser", laserFireDelay);
    }

    public void CastLaser()
    {
        laserOn = true;

        // First rotate the end point to correctly point the end point pos
        // Calculates the proper angle given two points (object pos, and player pos), converts from radian to degrees for proper angle value
        endPointParentPos.eulerAngles = new Vector3(0,0, Mathf.Atan2(_playerPos.y - transform.position.y, _playerPos.x - transform.position.x) * Mathf.Rad2Deg);

        // Set animator trigger/ bool
        animator.SetBool("isFire", true);
        animator.SetTrigger("fireTrigger");

        Invoke("DestroyObj", laserDestroyTime);
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
