using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(transform.GetChild(0).gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
