using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyer : MonoBehaviour
{
    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        if (gc == null)
        {
            Debug.LogError("Kein GameController gefunden!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gc != null && other.gameObject.tag.Equals("Becher"))
        {
            gc.Punkte++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Ball"))
        {
            gc.Baelle++;
        }
    }
}
