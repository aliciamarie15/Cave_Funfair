using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using HTW.CAVE.Kinect;
using Windows.Kinect;

public class EmitterController : MonoBehaviour
{
    public GameObject emitter;
    public KinectTracker kinectTracker;
    private KinectActor trackedActor;

    public event Action OnHandStill;

    private Vector3 previousPosition;
    private float stillTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (kinectTracker == null)
        {
            kinectTracker = FindObjectOfType<KinectTracker>();
            if (kinectTracker == null)
            {
                Debug.LogError("Kein KinectTracker gefunden!");
                return;
            }
        }

        kinectTracker.onCreateActor += OnCreateActorHandler;
        kinectTracker.onDestroyActor += OnDestroyActorHandler;
    }

     private void OnCreateActorHandler(KinectActor actor)
    {
        if (trackedActor == null)
        {
            trackedActor = actor;
            Debug.Log("Neuer KinectActor erkannt.");
        }
    }

    private void OnDestroyActorHandler(KinectActor actor)
    {
        if (trackedActor == actor)
        {
            Debug.Log("KinectActor verloren.");
            trackedActor = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trackedActor == null)
        {
            Debug.LogWarning("Kein KinectActor vorhanden.");
            return;
        }

        if (emitter == null)
        {
            Debug.LogWarning("Kein Emitter vorhanden.");
            return;
        }

        // Handgelenk abrufen
        KinectJoint handJoint = trackedActor.GetJoint(JointType.HandRight);

        if (handJoint.trackingState == TrackingState.NotTracked)
        {
            Debug.LogWarning("Hand wird nicht getrackt.");
            return;
        }

        // Emitter an Handposition setzen
        emitter.transform.position = handJoint.position;
        Debug.Log($"Hand-Position aktualisiert: X={handJoint.position.x}, Y={handJoint.position.y}, Z={handJoint.position.z}");

        if (Vector3.Distance(previousPosition, handJoint.position) < 0.02f)
        {
            stillTime += Time.deltaTime;
            if (stillTime >= 0.5f)
            {
                // Event auslösen
                OnHandStill?.Invoke();
            }
        }
        else
        {
            stillTime = 0f;
        }

        previousPosition = handJoint.position;
    }
}