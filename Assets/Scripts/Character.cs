using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

//[CreateAssetMenu(fileName = "New Character", menuName = "Unit Level 1")]
public class Character
{
    public string Type;
    public int Level;

    public Character(string type, int level) 
    {
        Type = type;
        Level = level;
    }
}
