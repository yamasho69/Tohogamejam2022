using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public float DieVelocity = 5;
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.relativeVelocity.sqrMagnitude > DieVelocity)
            Destroy(gameObject);
    }
}
