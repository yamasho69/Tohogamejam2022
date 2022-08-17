using UnityEngine;

public class FlipperMove : MonoBehaviour {

    private float angle;

    private void Start() {
        angle = 0;
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            angle += 0.7f;
            transform.eulerAngles += new Vector3(0, 0, angle);
        }
    }
}