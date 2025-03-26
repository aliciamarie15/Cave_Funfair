using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;


public class SpeechController : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    public BallController ballController;

    // Start is called before the first frame update
    void Start()
    {
        ballController = GetComponent<BallController>();

        if (ballController == null)
        {
            Debug.LogError("BallController nicht gefunden!");
        }
        
        keywordRecognizer = new KeywordRecognizer(new string[] { "Wurf" });
        keywordRecognizer.OnPhraseRecognized += OnKeywordRecognized;
        keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        string keyword = args.text;
        Debug.Log($"Keyword erkannt: {keyword}");

        switch (keyword)
        {
            case "Wurf":
                Debug.Log("Ball wird geworfen!");
                ballController.ThrowBall();
                break;
        }
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null && keywordRecognizer.IsRunning)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
