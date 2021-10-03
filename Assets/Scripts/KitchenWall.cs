using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenWall : MonoBehaviour
{
    public Vector3 pos;
    public Vector3 rot;
    public float size;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void setVars()
    {
        transform.position = pos;
        transform.localScale = new Vector3(size, 1, 1);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
