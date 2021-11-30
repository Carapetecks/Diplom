using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerFirstLoc : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private Transform target;
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;

    }
    private void Update()
    {
        Vector3 position = target.position;
        position.z = -10.0f;
        position.y += 1f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }

}

