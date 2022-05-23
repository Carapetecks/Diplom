using UnityEngine;

public class CameraControllerFirstLoc : MonoBehaviour
{
    [SerializeField]  private float speed = 5.0f;
    [SerializeField]  private Transform target;
    [SerializeField]  float bottomLimit;
    [SerializeField]  float rightLimit;
    [SerializeField]  float leftLimit;
    [SerializeField]  float upperLimit;
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;

    }
    private void Update()
    {
        if (!target) return;
        Vector3 position = target.position;
        position.z = -10.0f;
        position.y +=  0.5f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, upperLimit, bottomLimit),
            transform.position.z);
    }

}

