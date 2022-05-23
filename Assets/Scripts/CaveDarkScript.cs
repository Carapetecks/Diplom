using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveDarkScript : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    Character character;
    public float fadeTime = 5;
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
            SceneManager.sceneLoaded += this.OnLoadCallback;
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
    public void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        CharacterData data = SaveSystem.LoadCharacterOnSecondLocation();
        lifes = data.lifes;
        Vector2 position;
        position.x = data.position[0];
        position.y = data.position[1];
        transform.position = position;
        Debug.Log("load data 1 loc");
        SceneManager.sceneLoaded -= this.OnLoadCallback;
    }
}
