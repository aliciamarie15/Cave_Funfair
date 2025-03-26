using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    public GameObject slicedFruit;
    public GameObject fruitJuice;
    private float rotationForce = 200;
    private Rigidbody rb;
    public int scorePoints;
    private GameManager gm;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>(); //!!!!! nochmal anschauen 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector2.right*Time.deltaTime*rotationForce);
    }

    private void InstanciateSlicedFruite()
    {
        GameObject instantiatedFruit = Instantiate(slicedFruit, transform.position, transform.rotation);
        GameObject instantiatedJuice = Instantiate(fruitJuice, new Vector3(transform.position.x, transform.position.y, 2f), fruitJuice.transform.rotation);
        // Z Position für fruit Juice eventuell ändern, da Wand nicht auf z=0 liegt.

        Rigidbody[] slicedRb = instantiatedFruit.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody srb in slicedRb)
        {
            srb.AddExplosionForce(140f, transform.position, 10);
            srb.velocity = rb.velocity*1.2f;
        }
        Destroy(instantiatedFruit, 5);
        Destroy(instantiatedJuice, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Blade")
        {
            gm.UpdateTheScore(scorePoints);
            Destroy(gameObject);
            InstanciateSlicedFruite();
        }
        if (other.tag == "BottomTrigger")
        {
            gm.UpdateLives();
        }
    }

    
}
