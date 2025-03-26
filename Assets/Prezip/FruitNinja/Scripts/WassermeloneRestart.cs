using UnityEngine;
using UnityEngine.SceneManagement;  // Für das Neuladen der Szene
using System.Collections;  // Für die Verwendung von Coroutines

public class Watermelon : MonoBehaviour
{
    public GameObject blade; // Referenz zur Blade
    public GameObject slicedWatermelon; // Prefab für die geschnittene Wassermelone
    public float rotationSpeed = 200f; // Geschwindigkeit der Drehung
    private bool isSliced = false; // Um zu prüfen, ob die Melone schon geschnitten wurde

    private void Start()
    {
        // Initiale Drehung der Melone
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.angularVelocity = Random.insideUnitSphere * rotationSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfen, ob die Blade mit der Wassermelone in Kontakt kommt
        if (other.gameObject == blade && !isSliced)
        {
            Debug.Log("Blade hat die Wassermelone getroffen!");
            isSliced = true;
            SliceWatermelon();
        }
    }

    private  void SliceWatermelon()
    {
        Destroy(gameObject);
        // Wassermelone teilen (slicedPrefab wird erzeugt)
        Instantiate(slicedWatermelon, transform.position, transform.rotation);
        
        RestartGame();
       
    }


    private void RestartGame()
    {
        Debug.Log("Spiel wird neu gestartet...");
        // Sicherstellen, dass die Szene korrekt neu geladen wird
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}