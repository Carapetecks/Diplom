using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Slot slot;
    public Character character;
    public GameObject item;
    private Transform player;
    bool speedboost;
   
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {      
        if (Input.GetButtonDown("Heal")) Heal();
        if (Input.GetButtonDown("Potion")) SpeedBoost();        
        if (Input.GetKeyDown(KeyCode.Q)) SpawnDroppedItem();
    }
    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + 0.5f, player.position.y + 0.2f);
        Instantiate(item, playerPos, Quaternion.identity);
        Destroy(gameObject);
    }
    public void Heal()
    {        
        if(gameObject.tag.Equals("HealingPotion"))
        {
            if(character.lifes < 5)
            {
                character.lifes++;
                Destroy(gameObject);
            }         
        }        
        
    }
    public void SpeedBoost()
    {
        if (gameObject.tag.Equals("SpeedPotion"))
        {
            speedboost = true;
            character.speed += 2f;
            Destroy(gameObject);
            StartCoroutine(Speed());            
        }
    }
    IEnumerator Speed()
    {
        
        yield return new WaitForSeconds(4f);
        Debug.Log("speed-");
        character.speed -= 2f;
        speedboost = false;
    }
}
