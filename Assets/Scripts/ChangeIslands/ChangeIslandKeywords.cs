using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class ChangeIslandKeywords : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeIslandKeywords(UnityEngine.Windows.Speech.PhraseRecognizedEventArgs obj)
    {
        Debug.Log($"Keyword erkannt als {obj.text}");
        
        if(!obj.text.Equals("eins") && !obj.text.Equals("zwei") && !obj.text.Equals("drei")) return;
        
        Debug.Log($"Keyword erkannt als {obj.text}");
        
        if(obj.text.Equals("eins"))
        {
            
        } 
        if(obj.text.Equals("zwei"))
        {
            
        } 
        if(obj.text.Equals("drei"))
        {
            
        } 
    }
}
