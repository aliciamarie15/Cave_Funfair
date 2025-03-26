using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoneGameManager1 : MonoBehaviour
{
    [System.Serializable]
    public class Stone
    {
        public GameObject stoneObject; // Referenz zum Stein-Objekt
        public string color;           // Farbe des Steins (z. B. "red", "blue", "orange")
    }

    public List<Stone> stones; // Liste aller Steine im Spiel
    public KeyCode redKey = KeyCode.R;    // Taste für "rot"
    public KeyCode blueKey = KeyCode.B;   // Taste für "blau"
    public KeyCode greenKey = KeyCode.G;  // Taste für "grün"
    public KeyCode orangeKey = KeyCode.O; // Taste für "orange"

    public float requiredHeight = 0.20f; // Höhe, die der Stein erreichen muss, um entfernt zu werden
    public float heightTolerance = 0.5f;  // Toleranz für die Höhenüberprüfung (kannst du noch anpassen)

    private float startTime;  // Zeit, zu der das Spiel startet
    private bool timerRunning = true; // Gibt an, ob der Timer läuft
    public TMP_Text timerText;    // UI-Text zur Anzeige der Zeit (in Canvas)

    void Start()
    {
        // Starte den Timer, wenn das Spiel beginnt
        startTime = Time.time;
    }

    void Update()
    {
        // Aktualisiere die Stoppuhr-Anzeige
        if (timerRunning)
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimerDisplay(elapsedTime);
        }

        // Überprüfe Tasten und entferne die entsprechenden Steine nur, wenn die Höhe passt
        // für Anwendung an PC
        if (Input.GetKeyDown(redKey))
        {
            RemoveStonesByColorAndHeight("red");
        }
        if (Input.GetKeyDown(blueKey))
        {
            RemoveStonesByColorAndHeight("blue");
        }
        if (Input.GetKeyDown(greenKey))
        {
            RemoveStonesByColorAndHeight("green");
        }
        if (Input.GetKeyDown(orangeKey))
        {
            RemoveStonesByColorAndHeight("orange");
        }

        // Überprüfe, ob das Spiel gewonnen wurde (alle Steine entfernt)
        if (stones.Count == 0 && timerRunning)
        {
            timerRunning = false;   //Stoppt den Timer
            float finalTime = Time.time - startTime;
            Debug.Log($"You Win! Time taken: {finalTime:F2} seconds");
            this.DisplayFinalTime(finalTime);
        }
    }

    // Entfernt Steine basierend auf der Farbe und Höhe
    public void RemoveStonesByColorAndHeight(string color)
    {
        bool stoneRemoved = false;

        for (int i = stones.Count - 1; i >= 0; i--)
        {
            // Hole die aktuelle Position des Steins
            Vector3 stonePosition = stones[i].stoneObject.transform.position;

            // Berechne den Abstand zur gewünschten Höhe
            float heightDifference = Mathf.Abs(stonePosition.y - requiredHeight);

            // Debugging: Logge den Abstand zur Höhe und die Farbe
            // Debug.Log($"Checking stone: {stones[i].stoneObject.name}, Color: {stones[i].color}, " +
            //           $"Position: {stonePosition}, Required Height: {requiredHeight}, " +
            //           $"Height Difference: {heightDifference}");

            // Überprüfe, ob die Farbe und die Höhe innerhalb der Toleranz sind
            if (stones[i].color == color && heightDifference <= heightTolerance)
            {
                Debug.Log($"Removing stone: {stones[i].stoneObject.name} at position {stonePosition}");
                Destroy(stones[i].stoneObject);  // Entferne das Stein-Objekt
                stones.RemoveAt(i);             // Entferne es aus der Liste
                stoneRemoved = true;
            }
            // else
            // {
            //     // Debugging: Der Stein ist entweder nicht an der richtigen Position oder nicht die richtige Farbe
            //     Debug.Log($"Stone {stones[i].stoneObject.name} is not at the correct height or color.");
            // }
        }

        if (!stoneRemoved)
        {
            Debug.LogWarning($"No stones with color {color} found at the correct height.");
        }
    }
    
    // Aktualisiert die Timer-Anzeige während des Spiels
    void UpdateTimerDisplay(float time)
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText is null. Make sure it is assigned in the Inspector.");
            return;
        }

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = $"Time: {minutes:00}:{seconds:00}"; // Anzeige im Format "MM:SS"
    }

    // Zeigt die Endzeit an, wenn das Spiel beendet ist
    public void DisplayFinalTime(float finalTime) // Methode public machen, um sie korrekt aufzurufen
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText is null. Make sure it is assigned in the Inspector.");
            return;
        }

        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        timerText.text = $"Final Time: {minutes:00}:{seconds:00}"; // Anzeige der Endzeit
    }
}