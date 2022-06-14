using UnityEngine;

public class Slot : Spawn
{
    private Inventory inventory;    
    public int i;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && i == 0) Slot1();
        if (Input.GetKeyDown(KeyCode.Alpha2) && i == 1) Slot2();
        if (Input.GetKeyDown(KeyCode.Alpha3) && i == 2) Slot3();
        if (Input.GetKeyDown(KeyCode.Alpha4) && i == 3) Slot4();
        if (Input.GetKeyDown(KeyCode.Alpha5) && i == 4) Slot5();
        if (transform.childCount<=0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
    public void Slot1()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().Heal();
        }
        foreach (Transform child in transform)
        {          
            child.GetComponent<Spawn>().SpeedBoost();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().ForcePower();
        }
    }
    public void Slot2()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().Heal();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpeedBoost();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().ForcePower();
        }
    }
    public void Slot3()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().Heal();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpeedBoost();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().ForcePower();
        }
    }
    public void Slot4()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().Heal();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpeedBoost();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().ForcePower();
        }
    }
    public void Slot5()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().Heal();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpeedBoost();
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().ForcePower();
        }
    }
}
