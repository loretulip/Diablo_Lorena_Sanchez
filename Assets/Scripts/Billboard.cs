using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // para que la barra pueda mirar a cámara (solo para cámara ortográfica)
        transform.forward = -cam.transform.forward;
    }
}
