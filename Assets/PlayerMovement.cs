using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float sensX;
    public Rigidbody rb;
    public float speed = 1f;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        yRotation += mouseX;

        Vector3 move = transform.right * x + transform.forward * z;
        rb.rotation = Quaternion.Euler(0, yRotation, 0);

        rb.velocity = move;
    }
}
