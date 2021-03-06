using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debilus : MonoBehaviour
{
    public GameObject deb;
    public float hap;
    public float mon;
    
        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deb.transform.Translate(5*Time.deltaTime, 0, -5*Time.deltaTime);
        
    }
}
