using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterData
{
    public int lifes;
    public float[] position;    
    
    public CharacterData(Character character)
    {     
        lifes = character.lifes;
        position = new float[2];
        position[0] = character.transform.position.x;
        position[1] = character.transform.position.y;               
    }
    
}
