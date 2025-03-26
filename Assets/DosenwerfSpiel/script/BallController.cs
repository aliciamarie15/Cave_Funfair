using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject Ball;
    public GameObject Emitter;
    public float Force = 3000f;
    
    private bool isThrowing = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
        {
            ThrowBall();
        }
    }

    public void ThrowBall()
    {
        if (isThrowing) return;
        isThrowing = true;

        // Ball erstellen
        GameObject BallInstance = Instantiate(Ball, Emitter.transform.position, Quaternion.identity);
        Rigidbody rb = BallInstance.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Richtung berechnen (Emitter -> nach vorne & leicht nach oben)
            Vector3 dir = (Emitter.transform.forward + Emitter.transform.up * 0.5f).normalized;
            rb.AddForce(dir * Force);
        }

        // Reset für nächsten Wurf
        StartCoroutine(ResetThrow());

        Debug.Log("Ball geworfen.");
    }

    IEnumerator ResetThrow()
    {
        // Wurf für 0,5 Sekunden sperren
        yield return new WaitForSeconds(0.5f);
        isThrowing = false;
    }
}