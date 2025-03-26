using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Scrips.Interaction
{
    public class KeyWordRecognition : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] 
        private string[] keywords;

        private KeywordRecognizer keywordRecognizer;

        public event Action<PhraseRecognizedEventArgs> OnKeywordRecognized;
        void Start()
        {
            keywordRecognizer = new(keywords);
            keywordRecognizer.OnPhraseRecognized += HandlePhraseRecognized;
            keywordRecognizer.Start();

        }

        private void HandlePhraseRecognized(PhraseRecognizedEventArgs args)
        {
            Debug.Log($"Wort wurde erkannt {args.text}");
            OnKeywordRecognized?.Invoke( args );
        }
    }
}
