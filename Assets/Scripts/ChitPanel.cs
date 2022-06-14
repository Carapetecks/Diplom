using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChitPanel : MonoBehaviour
{
    public Character character;
    public PlayerAttack characterAttackDamage;
    public Monster[] monsters;
    public GameObject endPoint;
    public GameObject monster;
    public Text jumpForceCounter;
    public Text SpeedCounter;
    public Text DamageCounter;
    public static float JumpForce;
    public static float Speed;
    public static float Damage;


    private void Start()
    {
        endPoint = GameObject.FindGameObjectWithTag("EndPoint");
        monster = GameObject.Find("Monsters");
   
        JumpForce = character.jumpForce;
        Speed = character.speed;
        Damage = characterAttackDamage.damage;

    }
    private void Update()
    {
        jumpForceCounter.text = JumpForce.ToString();
        SpeedCounter.text = Speed.ToString();
        DamageCounter.text = Damage.ToString();
        JumpForce = character.jumpForce;
        Speed = character.speed;
        Damage = characterAttackDamage.damage;
    }

    public void TeleportToEnd()
    {
        character.transform.position = endPoint.gameObject.transform.position;
    }

    public void TeleportToCave()
    {
        SceneManager.LoadScene("PixelLvl");
    }   
    public void TeleportToFirstLoc()
    {
        SceneManager.LoadScene("FirstScene");
    }
    public void GetScore()
    {
        ScoreText.Score += 500;
    }

    public void KillMonsters()
    {
        //monsters.SetActive(false);

        //for(int i=0; i<monsters.Length; i++)
        //{
        //    monsters[i].GetComponent<Monster>().reciveDamage(500);
        //}
        Destroy(monster);


    }
    public void ReviveMonsters()
    {
        //monsters.SetActive(true);
    }

    public void HPBust()
    {
        if (character.lifes < 5)
            character.lifes += 1;
    }

    public void TakeDamage()
    {
        character.lifes -= 1;
    }

    public void BustJumpForce()
    {
        character.jumpForce += 1;
    }
    public void DebuffJumpForce()
    {
        if (character.jumpForce > 5.5)
        {
            character.jumpForce -= 1;          
        }
            
    }

    public void BustSpeed()
    {
        character.speed += 1;
    }

    public void DebuffSpeed()
    {
        if (character.speed > 1)
            character.speed -= 1;
    }

    public void BustDamage()
    {
        characterAttackDamage.damage += 1;
    }

    public void DebuffDamage()
    {
        if (characterAttackDamage.damage > 0)
            characterAttackDamage.damage -= 1;
    }

}
