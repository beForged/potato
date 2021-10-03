using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class potato : MonoBehaviour
{

    [SerializeField] public PotatoEatEvent potatoEat;
    public UnityEvent potatoExplode;

    public float bakeSpeed = .1f;
    public float bakeAmt = 0f;

    public bool isBaking;

    public AudioClip boom;
    private MicrowaveDoor door;
    private bool end;
    void Start()
    {
        end = false;
        isBaking = false;
    }

    void FixedUpdate()
    {
        if (isBaking && !door.opened())
        {
            bakeAmt += bakeSpeed;
        }
        if(bakeAmt > 110 && !end)
        {
            end = true;
            AudioSource.PlayClipAtPoint(boom, transform.position);
            //explode
            ParticleSystem exp = GetComponent<ParticleSystem>();
            exp.Play();
            Debug.Log("potato explode");
            potatoExplode.Invoke();
            gameObject.SetActive(false);
            Destroy(gameObject, exp.main.duration);
        }
    }


    public void eat()
    {
        potatoEat.Invoke(bakeAmt);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("potato ontriggerenter " + other);
        if (other.name == "Microwave")
        {
            isBaking = true;
            door = other.GetComponentInChildren<MicrowaveDoor>();
            Debug.Log(door);

        }


    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Microwave")
        {
            isBaking = false;
        }

    }

}

[System.Serializable] 
public class PotatoEatEvent : UnityEvent<float>
{

}
