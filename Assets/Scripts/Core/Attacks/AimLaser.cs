using UnityEngine;

public class AimLaser : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;
    [SerializeField]
    private float aimTime;

    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public Laser laserScript;

    private void Awake() 
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Invoke("DestroyObj", aimTime);
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
