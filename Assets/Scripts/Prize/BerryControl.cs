using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryControl : MonoBehaviour
{
    public int berryCount = 0; 
    public float time = 90f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (berryCount < 10)
        {
            time = 90f;
        }
        
        if (berryCount >= 10 && berryCount < 30)
        {
            time = 30f;
        }
    }
}
