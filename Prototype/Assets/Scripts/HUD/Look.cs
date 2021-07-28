using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform cam;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 vec = new Vector3(cam.forward.x, 0, cam.forward.z);
        transform.LookAt(cam);
    }
}
