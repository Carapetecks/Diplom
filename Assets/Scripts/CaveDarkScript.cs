using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveDarkScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public Character character;
    public float fadeTime = 5;
    public float[] position;
    private int lifes;

    private void Awake()
    {       
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();        
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (collision != null && unit && unit is Character)
        {            
            StartCoroutine(LoadScene(character.GetComponent<Character>()));
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit unit = collision.GetComponent<Unit>();
        if (collision != null && unit && unit is Character)
        {
            animator.SetBool("light", true);
            animator.SetBool("fade", false);
        }
    }

    IEnumerator LoadScene(Character character)
    {
        SaveSystem.SaveCharacterOnFirstLocation(character);
        animator.SetBool("fade", true);
        animator.SetBool("light", false);
        yield return new WaitForSeconds(3);             
        SceneManager.LoadScene("PixelLvl");
        
    }    
    public void OnLevelWasLoaded(int level)
    {   
            CharacterData data = SaveSystem.LoadCharacterOnFirstLocation();
            character.lifes = data.lifesForrest;
            Vector2 position;
            position.x = data.position[0];
            position.y = data.position[1];
            character.transform.position = position;
            Debug.Log("load data 1 loc");
    }
}
