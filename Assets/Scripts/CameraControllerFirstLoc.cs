using UnityEngine;

public class CameraControllerFirstLoc : MonoBehaviour
{
    [SerializeField]  private float speed = 4.0f;
    [SerializeField]  private Transform target;
    [SerializeField]  float bottomLimit;
    [SerializeField]  float rightLimit;
    [SerializeField]  float leftLimit;
    [SerializeField]  float upperLimit;
    float dirY;

    private void Start()
    {
        dirY = Input.GetAxisRaw("Vertical");

    }
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

    
        if (dirY == 1)
        {
            CameraUp();
        }

        if (dirY == -1)
        {
            CameraDown();

        }

    }

    private void CameraUp()
    {
        Vector3 position = target.position;
        position.z = -10.0f;
        position.y += 2f;
    }

    private void CameraDown()
    {
        Vector3 position = target.position;
        position.z = -10.0f;
        position.y -= 2f;
    }
}

