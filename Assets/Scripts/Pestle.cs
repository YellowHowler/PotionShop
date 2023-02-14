using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pestle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    private string subType;

    private Vector3 mousePos;
    private bool isDragging;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
       if(Input.GetMouseButtonUp(0))
       {
        isDragging = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
       } 
       if(isDragging)
       {
        rb.bodyType = RigidbodyType2D.Kinematic;
       }
    }

    private void OnMouseDrag()
    {
        isDragging = true;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }
}
