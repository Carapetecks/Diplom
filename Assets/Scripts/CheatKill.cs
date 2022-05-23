using UnityEngine;

public class CheatKill : MonoBehaviour
{
   
    
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            KillAll();
        }
    }
 

    public void KillAll()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();   
        foreach (Monster monster in monsters) Destroy(monster);
        Bullet[] bullets = FindObjectsOfType<Bullet>();   
        foreach (Bullet bullet in bullets) Destroy(bullet);       
    }
}
