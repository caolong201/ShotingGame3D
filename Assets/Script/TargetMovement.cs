using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{

    public bool shoudlMove, shouldRotate;
    public float moveSheep, rotateShepp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shoudlMove)
        {
            transform.position += new Vector3(moveSheep, 0f, 0f) * Time.deltaTime;
        }
        if (shouldRotate)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotateShepp * Time.deltaTime, 0f ));
        }
    }


}
