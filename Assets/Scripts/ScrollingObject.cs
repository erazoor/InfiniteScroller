using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public float scrollSpeed;
    
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
    }
    
    public void SetSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
