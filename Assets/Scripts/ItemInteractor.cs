using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractor : MonoBehaviour
{
    public SpringJoint springJoint;

    public int layerMask = 6;
    public Camera playerCam;
    public float pickupRange = 2;

    //public Rigidbody potatoHack;
    private Rigidbody pickedUp;

    public MicrowaveDoor door;
    private bool doorState = true;

    private GameManager gm;
    void Start()
    {
        gm = (GameManager) GameObject.FindObjectOfType(typeof(GameManager));
        layerMask = 1 << layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        //drop potato 
        if (springJoint.connectedBody != null && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("unhit");
            pickedUp.useGravity = true;
            springJoint.connectedBody = null;
            pickedUp = null;
        }
        
        //eat potato
        if (pickedUp != null && Input.GetKeyDown(KeyCode.F))
        {
            potato pot = pickedUp.GetComponentInParent<potato>();
            if(pot != null)
            {
                Debug.Log("potato eat");
                pot.eat();
                springJoint.connectedBody = null;
                pickedUp = null;
            }
            
        }

        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.E) && Physics.Raycast(playerCam.transform.position, playerCam.transform.TransformDirection(Vector3.forward), out hit, pickupRange, layerMask))
        {
            Debug.Log("hit " + hit.collider.gameObject.name);
            if(hit.collider.gameObject.name.Contains("potato"))
            {
                pickedUp = hit.collider.attachedRigidbody;
                springJoint.connectedBody = pickedUp;
                //springJoint.connectedBody = potatoHack;
                pickedUp.useGravity = false;
            }

            if(hit.collider.gameObject.name == "OpenButton")
            {
                door.activateDoor();
                doorState = door.opened();
            }

            if(hit.collider.gameObject.name == "PotatoSpawner")
            {
                gm.makePotato();
            }
        }
    }

    private void clearHeld()
    {
    }

    void FixedUpdate()
    {
        transform.position = playerCam.ViewportToWorldPoint(new Vector3(.5f, .5f, pickupRange));
    }
}
