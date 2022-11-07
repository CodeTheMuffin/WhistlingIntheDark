using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    /* 
     * ClickAndDragWithRigidbody (Kinematic)
     * Taken from: https://gamedevbeginner.com/how-to-move-an-object-with-the-mouse-in-unity-in-2d/
     */
    public Rigidbody2D selectedObject;
    public float MAX_SPEED = 10f;

    Vector3 offset;
    Vector3 mousePosition;
    Vector3 lastPosition;
    Vector2 mouseForce;
    

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (selectedObject)
        {
            mouseForce = (mousePosition - lastPosition) / Time.deltaTime;
            mouseForce = Vector2.ClampMagnitude(mouseForce, MAX_SPEED);
            lastPosition = mousePosition;
        }

        // select
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

            if (targetObject && targetObject.CompareTag(GameConstants.Tags.SELECTABLE_OBJECT))
            {
                selectedObject = targetObject.transform.gameObject.GetComponent<Rigidbody2D>();
                offset = selectedObject.transform.position - mousePosition;
            }
        }

        // deselect
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            selectedObject.velocity = Vector2.zero;
            //selectedObject.AddForce(mouseForce, ForceMode2D.Impulse); // use with selectable objects with Dynamic rigidbody! 
            selectedObject = null;
        }
    }

    void FixedUpdate()
    {
        if (selectedObject)
        {
            selectedObject.MovePosition(mousePosition + offset);
            
        }
    }
}
