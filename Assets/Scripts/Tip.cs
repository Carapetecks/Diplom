using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    public Transform searchDot;
    public float searchRange;
    public LayerMask item;
    private Character mainCharacter;

    private void Update()
    {
        Search();
    }

    public void Search()
    {
        Collider2D[] items = Physics2D.OverlapCircleAll(searchDot.position, searchRange, item);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].GetComponent<Item>();
            Debug.Log("калак найден");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(searchDot.position, searchRange);
    }
}
