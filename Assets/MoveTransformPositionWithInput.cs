using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTransformPositionWithInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position += Vector3.right;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            transform.position += Vector3.left;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position += Vector3.forward;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            transform.position += Vector3.back;

        if (Input.GetKeyDown(KeyCode.Space))
            transform.position = new Vector3(0, 0.5f, 0);
    }
}
