using UnityEngine;

public class AimLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    public float aimTime;

    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public Laser laserScript;

    private void Awake() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        // Random number for aim time
        Invoke("DestroyObj", Random.Range(1.5f, aimTime));
    }

    // Creates a line pointing at the player
    private void Update() 
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, Player.transform.position);
    }

    // Returns angle data to laser script
    public void DestroyObj()
    {
        laserScript.FireLaser(Player.transform.position);

        Destroy(gameObject);
    }
}
