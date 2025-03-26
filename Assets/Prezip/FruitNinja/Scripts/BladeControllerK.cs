using UnityEngine;
using Windows.Kinect;

public class BladeController : MonoBehaviour
{
    private KinectSensor sensor;
    private BodyFrameReader bodyFrameReader;
    private Body[] bodies;
    private bool isKinectTrackingAvailable = false;

    // Blade Move Speed
    public float moveSpeed = 10f;

    private void Start()
    {
        // Initialisiere Kinect
        sensor = KinectSensor.GetDefault();
        if (sensor != null && sensor.IsAvailable)
        {
            bodyFrameReader = sensor.BodyFrameSource.OpenReader();
            bodies = new Body[sensor.BodyFrameSource.BodyCount];
            isKinectTrackingAvailable = true;
        }
        else
        {
            isKinectTrackingAvailable = false;
        }
    }

    private void Update()
    {
        try
        {
            if (isKinectTrackingAvailable)
            {
                // Kinect-Tracking aktiv
                TrackHandWithKinect();
            }
            else
            {
                // Kinect-Tracking nicht verfügbar, benutze Maus oder Pfeiltasten
                if (Input.mousePresent)
                {
                    // Steuerung über die Maus
                    ControlBladeWithMouse();
                }
                else
                {
                    // Steuerung über die Pfeiltasten
                    ControlBladeWithArrowKeys();
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Fehler bei der Kinect-Tracking-Überprüfung: " + ex.Message);
            // Fallback zur Maussteuerung
            ControlBladeWithMouse();
        }
    }

    // Methode, um die Hand mit Kinect zu verfolgen
    private void TrackHandWithKinect()
    {
        BodyFrame bodyFrame = bodyFrameReader.AcquireLatestFrame();

        if (bodyFrame != null)
        {
            bodyFrame.GetAndRefreshBodyData(bodies);
            foreach (var body in bodies)
            {
                if (body.IsTracked)
                {
                    // Hole die rechte Handposition
                    var handRight = body.Joints[JointType.HandRight];

                    // Wenn die rechte Hand verfolgt wird
                    if (handRight.TrackingState == TrackingState.Tracked)
                    {
                        Vector3 handPosition = new Vector3(handRight.Position.X, handRight.Position.Y, handRight.Position.Z);
                        MoveBladeWithHand(handPosition);
                    }
                }
            }
        }
    }

    // Methode, um die Blade basierend auf der Handposition zu bewegen
    private void MoveBladeWithHand(Vector3 handPosition)
    {
        // Hier kannst du die Blade basierend auf der Handposition bewegen.
        // Zum Beispiel eine einfache Steuerung entlang der X- und Y-Achse:
        transform.position = new Vector3(handPosition.x, transform.position.y, transform.position.z);
    }

    // Fallback-Methode für die Steuerung mit der Maus
    private void ControlBladeWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z; // Behalte die Z-Position der Blade bei
        transform.position = mousePosition;
    }

    // Fallback-Methode für die Steuerung mit den Pfeiltasten
    private void ControlBladeWithArrowKeys()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    // Kinect beim Beenden der Anwendung schließen
    private void OnApplicationQuit()
    {
        if (sensor != null)
        {
            if (bodyFrameReader != null)
            {
                bodyFrameReader.Dispose();
            }

            sensor.Close();
        }
    }
}

