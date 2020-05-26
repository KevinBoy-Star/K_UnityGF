using System.Collections.Generic;
using UnityEngine;

public class BucketRotate : MonoBehaviour
{
    public float rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}
