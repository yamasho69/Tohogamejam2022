using UnityEngine;
using DG.Tweening;

public class FlipperMove : MonoBehaviour {
    private Vector3 vector3;

    private void Start() {
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            transform.DORotate(
                new Vector3(0, 0, 30), // 終了時のRotation
                0.2f                     // 演出時間
            );
        }
    }
}