using UnityEngine;

public class Item : MonoBehaviour
{    
    private Inventory inventory;
    private bool CheckEnter = false;
    public GameObject slotButton;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void Update()
    {
       //if(Input.GetButtonDown("Heal")) Heal();
        Take();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            CheckEnter = true;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            CheckEnter = false;
    }

    private void Take()
    {
        if (CheckEnter && Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;
                    Instantiate(slotButton, inventory.slots[i].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    

    

}//СДЕЛАТЬ КОРОБКУ ТИПО ИДТИ ЗА НЕЙ МОЖНО(ХАЙП)
