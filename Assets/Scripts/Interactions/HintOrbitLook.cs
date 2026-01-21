using UnityEngine;

public class HintCameraOrbit : MonoBehaviour
{
    [Header("References")]
    public Transform target;

    [Header("Orbit Settings")]
    public float radius = 0.5f;
    public float height = 1.6f;

    void LateUpdate()
    {
        if (!target || !Camera.main)
            return;

        // Direction from object to camera
        Vector3 dir = Camera.main.transform.position - target.position;
        dir.y = 0f;
        dir.Normalize();

        // Position on orbit facing the camera
        Vector3 orbitPos = target.position + dir * radius;
        orbitPos.y = target.position.y + height;

        transform.position = orbitPos;

        FaceCamera();
    }

    void FaceCamera()
    {
        Vector3 lookDir = transform.position - Camera.main.transform.position;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}