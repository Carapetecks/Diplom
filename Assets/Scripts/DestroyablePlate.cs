using System.Collections;
using UnityEngine;

public class DestroyablePlate : MonoBehaviour
{
    Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if(unit is Character)
        {
            StartCoroutine(Fall());          
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.3f);
        rigidbody.isKinematic = false;
        Destroy(gameObject, 0.5f);       
    }
}
