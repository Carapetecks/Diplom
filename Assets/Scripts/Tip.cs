using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{   
    public float searchRange;
    public LayerMask item;
    private Character mainCharacter;
    public Transform searchDot;
    private bool TipOn;

    private void Start()
    {
        TipOn = false;
    }
    private void Update()
    {
        Search();
    }

    public void Search()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(searchDot.position, searchRange, item);
        if (items.Length == 1)
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].GetComponent<Item>();
                Debug.Log("Предмет найден");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(searchDot.position, searchRange);
    }
}
