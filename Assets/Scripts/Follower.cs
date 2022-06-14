using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed;
    public Transform character;
    public Transform lightPointFollower;
    private void Start()
    {
        transform.position = lightPointFollower.transform.position;
    }
    private void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, lightPointFollower.transform.position, speed * Time.deltaTime);

    }
   
    
}
