using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using DG.Tweening;

public class Character : MonoBehaviour
{
    public GameObject bones;
    private Vector2 _StartPosition;
    public float MaxPullDistance = 1;
    public float flyforce = 10;
    private Rigidbody2D rigidbody2d;
    public BoxCollider2D boxCollider2;//スタート地点のボックスコライダー
    public CircleCollider2D circleCollider2;
    

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.isKinematic = true;
        _StartPosition = transform.position;
    }

    private void OnMouseDrag() {
        boxCollider2.enabled = false;
        bones.SetActive(false);//ボーンを無効にする。有効なままだと、ボーンのRigidbody2Dはキネマティックではないので、落下する。
        circleCollider2.enabled = true;
        Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(_StartPosition, Position) > MaxPullDistance)
            Position = (Position - _StartPosition).normalized * MaxPullDistance + _StartPosition;

        if (Position.x > _StartPosition.x)
            Position.x = _StartPosition.x;

        transform.position = Position;

    }

    private void OnMouseUp() {
        var Force = (_StartPosition - (Vector2)transform.position) * flyforce;
        rigidbody2d.isKinematic = false;
        rigidbody2d.AddForce(Force, ForceMode2D.Impulse);
    }

    private void BoneActive() {
        bones.SetActive(true);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        circleCollider2.enabled = false;
        BoneActive();
    }
}
