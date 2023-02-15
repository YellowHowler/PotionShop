using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type
    {
        Plant = 0,
    };

    public Type type;

    private Rigidbody2D rb;
    private Camera cam;

    private string subType;

    private Vector3 mousePos;
    private bool isDragging;

    private string enter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
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
        rb.velocity = Vector2.zero;

        isDragging = true;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //rb.position = new Vector3(mousePos.x, mousePos.y, 0);
        rb.AddForce((mousePos-transform.position)*15, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        InteractableCrafting otherScript = other.gameObject.GetComponent<InteractableCrafting>();

        if(otherScript!= null)
        {
            otherScript.ChangeSprite(true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        InteractableCrafting otherScript = other.gameObject.GetComponent<InteractableCrafting>();

        if(otherScript!= null)
        {
            if(!isDragging)
            {
                if(other.gameObject.tag == "Grinder" && type == Type.Plant)
                //rb.position = other.transform.position - new Vector3(0, 0.1f, 0);
                //rb.bodyType = RigidbodyType2D.Static;
                otherScript.AddItem(gameObject, subType);
                Destroy(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        InteractableCrafting otherScript = other.gameObject.GetComponent<InteractableCrafting>();

        if(otherScript!= null)
        {
            otherScript.ChangeSprite(false);
        }
    }
}
