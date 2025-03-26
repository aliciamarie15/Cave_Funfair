using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    // Spieler
    public Transform player;


    // Update is called once per frame
    void Update()
    {   
        if (player != null)
        {
            // Synchronisiere die Position und Rotation der Kamera mit der des Spielers
            transform.position = player.position;
            transform.rotation = player.rotation;
        }
    }
}
