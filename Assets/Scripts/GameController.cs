using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject projectileEyePrefab;
    [SerializeField]
    private Transform corePos;
    [SerializeField]
    private Transform coreParentPos;
    [SerializeField]
    private float laserSpawnTime;
    [SerializeField]
    private float projectileEyeSpawnTime;
    [SerializeField]
    private float coreLifeTime;
    
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine("LaserSpawn");
        StartCoroutine("ProjectileEyeSpawn");

        // Invoke Core Destruction function after life time is over
        Invoke("DestroyCore", coreLifeTime);
    }

    IEnumerator LaserSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserSpawnTime);

            Instantiate(laserPrefab, corePos);
        }
    }

    IEnumerator ProjectileEyeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(projectileEyeSpawnTime);

            Instantiate(projectileEyePrefab, coreParentPos);
        }
    }

    public void DestroyCore()
    {
        // Destroy Function
    }
}
