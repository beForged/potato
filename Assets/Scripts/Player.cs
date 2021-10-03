using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 5;
    public float gravity = 20f;

    private CharacterController cc;

    private Vector3 move = Vector3.zero;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO: reset to menu
            //StartCoroutine(ResetToMenu(.3f));
        }
        // jumping code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cc.isGrounded)
            {
                move.y = jumpHeight;
            }
        }

        // add gravity
        if (!cc.isGrounded)
        {
            move.y -= gravity * Time.deltaTime;
        }

        float strafe = Input.GetAxis("Horizontal");
        float forward = Input.GetAxis("Vertical");

        // moving WASD
        move.z = speed * forward;
        move.x = speed * strafe;

        // actually move the player
        // float light = lightlevel();
        cc.Move(transform.TransformDirection(move * Time.deltaTime));
    }
}
