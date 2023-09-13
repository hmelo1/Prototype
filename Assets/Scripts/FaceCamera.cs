using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera Camera;
    private void Awake()
    {
        if(!Camera)
        {
            Camera = Camera.main;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.transform.rotation * Vector3.forward, Camera.transform.rotation * Vector3.up);
    }
}