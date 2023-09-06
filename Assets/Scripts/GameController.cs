using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private Transform corePos;
    [SerializeField]
    private float laserSpawnTime;
    
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine("LaserSpawn");
    }

    IEnumerator LaserSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserSpawnTime);

            Instantiate(laserPrefab, corePos);
        }
    }
}
