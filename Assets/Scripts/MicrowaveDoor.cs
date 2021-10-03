using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveDoor : MonoBehaviour
{
    public Transform Hinge;

    private bool isOpen;
    private Vector3 hingeAxis;


    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        hingeAxis = transform.position + new Vector3(.35f, 0f, 0f);
    }

    public bool opened()
    {
        return isOpen;
    }
    // Update is called once per frame
    public void activateDoor()
    {
        if (isOpen)
        {
            unhingeDoor();
            isOpen = false;
        } else
        {
            hingeDoor();
            isOpen = true;
        }
    }

    private void hingeDoor()
    {
        
        transform.RotateAround(hingeAxis, Vector3.up, 90 );
    }
    private void unhingeDoor()
    {
        transform.RotateAround(hingeAxis, Vector3.up, -90);
    }

}
