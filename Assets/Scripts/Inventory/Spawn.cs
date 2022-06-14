using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Slot slot;
    public Character character;
    public GameObject item;
    private Transform player;      
    private void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {   
        
        if (Input.GetButtonDown("Heal")) Heal();
        if (Input.GetButtonDown("Force")) ForcePower();
        if (Input.GetButtonDown("Potion")) SpeedBoost();
        if (Input.GetKeyDown(KeyCode.Q)) SpawnDroppedItem();        
    }
    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + 0.5f, player.position.y + 0.2f);
        Instantiate(item, playerPos, Quaternion.identity);
        Destroy(gameObject);
    }
    public void Heal() //лечение
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
    public void SpeedBoost() //баф на скорость
    {
        if (gameObject.tag.Equals("SpeedPotion"))
        {            
            character.speed += 2f;
            Destroy(gameObject);
            character.SpeedTimer();
        }       
    }
    public void JumpBoost() //баф на прыжок
    {
        if(gameObject.tag.Equals("JumpForce"))
        {

        }
    }

    public void ForcePower() //баф на прыжок
    {
        if (gameObject.tag.Equals("ForcePower"))
        {
            if (character.damage == 1)
            {
                character.damage += 1;
                Destroy(gameObject);
                character.ForceDamageTimer();
            }
        }
    }



}
