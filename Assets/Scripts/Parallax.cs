using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private float Layer;
    private Transform mainCamera;
    private Vector3 lastCamPos;
    private Vector3 deltaMovement;

    private void Awake() 
    {
        mainCamera = Camera.main.transform;
        lastCamPos = mainCamera.position;    
    }

    private void Update() 
    {
        Vector3 deltaMovement = mainCamera.position - lastCamPos;
        transform.position += deltaMovement * Layer;
        lastCamPos = mainCamera.position;
    }
}
