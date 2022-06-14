using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToFirstLocation : MonoBehaviour
{
   
    [SerializeField] public SpriteRenderer sprite;
    [SerializeField] public Animator animator;    
    [SerializeField] public float fadeTime = 5;
    public Character character;
    public float[] position;
    private int lifes;

    private void Awake()
    {        
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();        
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
        SaveSystem.SaveCharacterOnSecondLocation(character);
        animator.SetBool("fade", true);
        animator.SetBool("light", false);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("FirstScene");
        

    }
    public void OnLevelWasLoaded(int level)
    {
            CharacterData data = SaveSystem.LoadCharacterOnSecondLocation();            
            character.lifes = data.lifesForrest;
            Vector2 position;
            position.x = data.position[0];
            position.y = data.position[1];
            character.transform.position = position;
            Debug.Log("load data 2 loc");
    }    
}
