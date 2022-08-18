using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public GameObject characterPrefab;
    void Awake()
    {
        Instance = this;
        Instantiate(characterPrefab, transform.position, Quaternion.identity);
    }

    public void NextCharacter()
    {
        Instantiate(characterPrefab, transform.position, Quaternion.identity);
    }
}
