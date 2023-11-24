using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float x = 0f;
    [SerializeField] float speed = 1;
    [SerializeField] float amplitude = 4f;
    [SerializeField] Transform rotationCenter;
    Vector3 vectorPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        x += Time.deltaTime;
        vectorPosition= new Vector3(Mathf.Sin(x * speed) * amplitude, 0, Mathf.Cos(x * speed) * amplitude) + rotationCenter.position;
        //transform.rotation = Quaternion.Euler(new Vector3(0,(x*amplitude)%360,0));
        transform.position = vectorPosition;
        //transform.RotateAround(vectorPosition,0);
        
    }
}
