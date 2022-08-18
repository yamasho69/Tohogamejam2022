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
    public GameObject dotPrefab;
    private GameObject[] _DotObjects = new GameObject[20];
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider2d;//スタート地点のボックスコライダー
    public CircleCollider2D circleCollider2;
    private GameObject start;
    public float DotTimeInterval = 0.05f;

    void Start()
    {
        start = GameObject.Find("Start");
        boxCollider2d = start.GetComponent<BoxCollider2D>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.isKinematic = true;
        _StartPosition = transform.position;

        for(int i = 0;i< _DotObjects.Length; i++) {
            _DotObjects[i] = Instantiate(dotPrefab);
            _DotObjects[i].transform.localScale = _DotObjects[i].transform.localScale * (1 - 0.03f * i);
            _DotObjects[i].transform.parent = transform;
            _DotObjects[i].SetActive(false);
        }
    }

    private void OnMouseDrag() {
        boxCollider2d.enabled = false;
        bones.SetActive(false);//ボーンを無効にする。有効なままだと、ボーンのRigidbody2Dはキネマティックではないので、落下する。
        circleCollider2.enabled = true;
        Vector2 Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(_StartPosition, Position) > MaxPullDistance)
            Position = (Position - _StartPosition).normalized * MaxPullDistance + _StartPosition;

        if (Position.x > _StartPosition.x)
            Position.x = _StartPosition.x;

        transform.position = Position;

        UpdateDotObjects();

    }

    private void OnMouseUp() {
        var Force = (_StartPosition - (Vector2)transform.position) * flyforce;
        rigidbody2d.isKinematic = false;
        rigidbody2d.AddForce(Force, ForceMode2D.Impulse);
        for (int i = 0; i < _DotObjects.Length; i++) {
            _DotObjects[i].SetActive(false);
        }
        Invoke("StartActive", 0.5f);
    }

    private void BoneActive() {
        bones.SetActive(true);

    }

    private void StartActive() {
        boxCollider2d.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        circleCollider2.enabled = false;
        BoneActive();
        Invoke("NextCharacter", 6f);
        Destroy(gameObject, 6f);
    }

    private void NextCharacter() {
        LevelManager.Instance.NextCharacter();
    }

    private void UpdateDotObjects() {
        var Force = (_StartPosition - (Vector2)transform.position) * flyforce;
        var CurrenTime = DotTimeInterval;
        for(int i = 0; i < _DotObjects.Length; i++) {
            _DotObjects[i].SetActive(true);
            var Position = new Vector2();
            Position.x = (transform.position.x + Force.x * CurrenTime);
            Position.y = (transform.position.y + Force.y * CurrenTime) - (Physics2D.gravity.magnitude * CurrenTime*CurrenTime)/2;
            _DotObjects[i].transform.position = Position;
            CurrenTime += DotTimeInterval;
        }
    }
}
