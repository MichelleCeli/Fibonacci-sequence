using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Vector3 startPosition;

    Quaternion startRotation;

    Vector3 moveView;

    Vector3 resetView;

    Vector3 rotation;
   

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rotation = new Vector3(10, 0, 0);
    }

    public void MoveCamera(int fn){

        

        moveView = new Vector3(1, fn, 2);

        transform.position = transform.position + moveView;

        transform.Rotate(rotation);

    }

    public void ResetCamera(){
        transform.position = startPosition;
        transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.time);
    }

  
}
