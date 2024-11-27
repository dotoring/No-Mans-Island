using UnityEngine;

public class CanvasToCamera : MonoBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
