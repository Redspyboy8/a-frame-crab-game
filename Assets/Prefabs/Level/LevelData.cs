using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area", menuName = "Data/Area")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public GameObject backgroundPrefab;
    
    [Header("Music")]
    public AudioClip beginningMusic;
    public AudioClip endingMusic;

    [TextArea] public string songTitle;
    
    [Header("Obstacles")]
    public List<GameObject> obstacles = new List<GameObject>();
}