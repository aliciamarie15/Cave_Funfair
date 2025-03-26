using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBall : MonoBehaviour
{
    public Image progressCircle;
    public BallController throwBall;

    private bool isCharging = false;

    // Start is called before the first frame update
    void Start()
    {
        EmitterController emitter = FindObjectOfType<EmitterController>();
        if (emitter != null)
        {
            emitter.OnHandStill += StartCharging;
        }
    }

    void StartCharging()
    {
        if (!isCharging)
        StartCoroutine(ChargeThrow());
    }

    IEnumerator ChargeThrow()
    {
        isCharging = true;
        float chargeTime = 1.5f;
        float timer = 0f;

        while (timer < chargeTime)
        {
            timer += Time.deltaTime;
            progressCircle.fillAmount = timer / chargeTime;
            yield return null;
        }

        throwBall.ThrowBall();
        isCharging = false;
        progressCircle.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
