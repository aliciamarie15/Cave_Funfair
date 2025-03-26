using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeScript : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider sc; 
    private TrailRenderer tr;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sc = GetComponent<SphereCollider>();    
        tr = GetComponent<TrailRenderer>();
        
    }


    void Update()
{
    try
    {
        if (Input.GetMouseButtonDown(0))
        {
            tr.enabled = true;
            sc.enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            tr.enabled = false;
            sc.enabled = false;
        }
        BladeFollowMouse();
    }
    catch (System.Exception e)
    {
        Debug.LogWarning("Mouse input not available, using keyboard instead.");
        BladeFollowKeyboard();
    }
}

    private void BladeFollowKeyboard()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        rb.position += movement * Time.deltaTime * 5f;
    }


    private void BladeFollowMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1.7f;
        Camera camera = Camera.main; 
        rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    
}

