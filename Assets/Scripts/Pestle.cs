using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    private string subType;
    private float speed;

    private Vector3 mousePos;
    private bool isDragging;

    private Vector3 prevPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        prevPos = transform.position;

        rb.centerOfMass = Vector3.right*0.9f;
    }

    void Update()
    {
        speed = (transform.position - prevPos).magnitude / Time.deltaTime;

       if(Input.GetMouseButtonUp(0) || Vector2.Distance(mousePos, rb.position) > 2 && isDragging)
       {
        isDragging = false;
        //rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1;

        rb.velocity = Vector2.zero;
       } 
       if(isDragging)
       {
        rb.gravityScale = 0;
        //rb.bodyType = RigidbodyType2D.Kinematic;
       }
    }

    private void OnMouseDrag()
    {
        //transform.rotation = Quaternion.Euler(Vector3.forward * 90);
        rb.velocity = Vector2.zero;

        isDragging = true;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //rb.position = new Vector3(mousePos.x, mousePos.y, 0);
        rb.AddForce((mousePos-transform.position)*15, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Item" && speed > 0.5f && other.gameObject.GetComponent<Item>() == null)
        {
            other.gameObject.GetComponent<Explodable>().explode();
        }
    }
}
