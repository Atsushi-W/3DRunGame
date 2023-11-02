using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
