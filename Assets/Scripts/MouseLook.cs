using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    public float sensitivity = 5;
    [SerializeField]
    public float smoothing = 2.0f;

    public float YAxisAngleLock = 90f;

    public GameObject character;

    private Vector2 look;

    private Vector2 smoothv;

    // Start is called before the first frame update
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(!enabled)
        {
            return;
        }
        var delta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        delta = Vector2.Scale(delta, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothv.x = Mathf.Lerp(smoothv.x, delta.x, 1f / smoothing);
        smoothv.y = Mathf.Lerp(smoothv.y, delta.y, 1f / smoothing);

        look += smoothv;



        transform.localRotation = LockCameraMovement(Quaternion.AngleAxis(-look.y, Vector3.right));
        character.transform.localRotation = LockCameraMovement(Quaternion.AngleAxis(look.x, character.transform.up));
    }

    private Quaternion LockCameraMovement(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        var angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -YAxisAngleLock + 15, YAxisAngleLock);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
