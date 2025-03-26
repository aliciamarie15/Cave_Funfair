using System;
using Scrips.Interaction;
using UnityEngine;
using HTW.CAVE.Kinect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

//using UnityEngine.Windows.Speech;

public class Teleport : MonoBehaviour
{
    public GameObject Zentrale;
 
    public String mainScene;
    public String scene1;
    public String scene2;
    public String scene3;
    
    private bool sceneActive = false;
    private String lastLoadedScene;
    
    public LayerMask teleportLayers;
    public KeyWordRecognition recognition;
    
    private void Start()
    {
        if (recognition != null)
        {
            recognition.OnKeywordRecognized += TeleportPlayer;
        }
    }

    private void TeleportPlayer(UnityEngine.Windows.Speech.PhraseRecognizedEventArgs obj)
    {
        String keyword = obj.text;
        Debug.Log($"Keyword erkannt: {keyword}");

        switch (keyword)
        {
            case "stein":
                if (!sceneActive)
                {
                    SceneManager.LoadScene(scene1, LoadSceneMode.Additive);
                    //SceneManager.UnloadSceneAsync(mainScene);
                    TransFormObjectAway(Zentrale);
                    sceneActive = true;
                    lastLoadedScene = scene1;
                }
                //kinectTracker.gameObject.transform.parent.position = Insel1.transform.position;
                break;
            case "frucht":
                if (!sceneActive)
                {
                    SceneManager.LoadScene(scene2, LoadSceneMode.Additive);
                    TransFormObjectAway(Zentrale);
                    sceneActive = true;
                    lastLoadedScene = scene2;
                }
                //kinectTracker.gameObject.transform.parent.position  = Insel2.transform.position;
                break;
            case "dose":
                if (!sceneActive)
                {
                    SceneManager.LoadScene(scene3, LoadSceneMode.Additive);
                    TransFormObjectAway(Zentrale);
                    sceneActive = true;
                    lastLoadedScene = scene3;
                }
                //kinectTracker.gameObject.transform.parent.position  = Insel3.transform.position;
                break;
            case "stop":
                if (sceneActive)
                {
                    Scene loadedScene = SceneManager.GetSceneByName(lastLoadedScene);
                    SceneManager.UnloadSceneAsync(loadedScene);
                    TransFormObjectBack(Zentrale);
                    sceneActive = false;
                }
                //kinectTracker.gameObject.transform.parent.position  = Zentrale.transform.position;
                break;
        }
    }

    void TransFormObjectAway(GameObject go)
    {
        go.transform.position += posChange; 
    }

    private Vector3 posChange = new Vector3(0f, -10f, 0f);
    void TransFormObjectBack(GameObject go)
    {
        go.transform.position -= posChange;
    }
}