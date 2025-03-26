using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    public StoneGameManager1 stoneManager; // Referenz zum StoneGameManager1
    public string buttonColor;             // Farbe des Buttons (z. B. "red", "blue")
    public double toggleCooldown = 1.0;    // Abklingzeit, um Spam zu verhindern
    private double _lastInteractionTimestamp;

    void OnTriggerEnter(Collider other)
    {
        // Prüfe, ob die Abklingzeit eingehalten wurde
        if (Time.time - _lastInteractionTimestamp < toggleCooldown)
        {
            return;
        }

        // Aktualisiere den Zeitstempel für die letzte Interaktion
        _lastInteractionTimestamp = Time.time;

        // Entferne nur den untersten Stein der angegebenen Farbe
        stoneManager.RemoveStonesByColorAndHeight(buttonColor);
    }
}
