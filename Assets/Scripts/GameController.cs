using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private GameObject projectileEyePrefab;
    [SerializeField]
    private GameObject walkingEyePrefab;
    [SerializeField]
    private Transform corePos;
    [SerializeField]
    private Transform coreParentPos;
    [SerializeField]
    private Transform projectileSpawnPos;
    [SerializeField]
    private float laserSpawnTime;
    [SerializeField]
    private float projectileEyeSpawnTime;
    [SerializeField]
    private float walkingEyeSpawnTime;
    [SerializeField]
    private CoreController coreController;

    private Camera mainCamera;
    private CameraFollow cameraFollowScript;

    private float currentGameTime;
    private int currentStage;

    private void Awake() 
    {
        Application.targetFrameRate = 60;

        mainCamera = Camera.main;
        cameraFollowScript = mainCamera.GetComponent<CameraFollow>();
        
        StartCoroutine("GameCountdown");  
    }

    // Evaulates the stage every second, increments game time var
    // More efficient than update loop ??
    IEnumerator GameCountdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            currentGameTime++;

            // Stage one 30 seconds (ensure doesnt start multiple caroutines)
            if (currentGameTime >= 0 && currentStage == 0)
            {
                Debug.Log("Stage 1");
                currentStage++;

                StartCoroutine("LaserSpawn");
            }
            // Stage two 30 seconds
            else if (currentGameTime >= 30  && currentStage == 1)
            {
                Debug.Log("Stage 2");
                currentStage++;

                laserSpawnTime--;

                StartCoroutine("ProjectileEyeSpawn");
                StartCoroutine("WalkingEyeSpawn");  
            }
            // Stage Three 30 seconds
            else if (currentGameTime >= 60 && currentStage == 2)
            {
                Debug.Log("Final Stage");
                currentStage++;

                projectileEyeSpawnTime--;
                walkingEyeSpawnTime--;

                StartCoroutine("ProjectileEyeSpawn");
                StartCoroutine("LaserSpawn");
            }
            // END
            else if (currentGameTime >= 90 && currentStage == 3)
            {
                Debug.Log("END");
                currentStage++;

                DestroyCore();
            }
        }
    }

    IEnumerator LaserSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(laserSpawnTime);

            Instantiate(laserPrefab, coreParentPos);
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

    IEnumerator WalkingEyeSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(walkingEyeSpawnTime);

            Instantiate(walkingEyePrefab, projectileSpawnPos);
        }
    }

    public void DestroyCore()
    {
        // Destroy Function
        StopAllCoroutines();

        // Move camera target to core
        cameraFollowScript.target = corePos; 

        // Stop core random animations, start death animation
        coreController.DeathAnimation();
    }
}
