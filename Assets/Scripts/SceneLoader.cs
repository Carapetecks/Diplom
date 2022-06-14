using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int levelToLoad;
    public VectorValue CharacterPosition;
    public Vector3 position;
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.L)) OnFadeComplete();
    }
    public void OnFadeComplete()
    {
        CharacterPosition.initialValue = position;
        SceneManager.LoadScene(levelToLoad);
    }
}
