using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    //Attributes
    public float dragSpeed = 200;
    public float lowerVelocityBy = 0.025f;

    private bool isEnding, zoomActive;
    private Vector3 velocity, lerp;
    private Vector2 lastposition;
    private float touchdistance, slideCounter;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        isEnding = false;
        zoomActive = false;
        velocity = new Vector3();
        lerp = new Vector3();
        lastposition = new Vector2();
        slideCounter = 0;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Touch touch;

        switch (Input.touchCount)
        {
            case 1:

                // Get Input
                touch = Input.GetTouch(0);

                // reset isEnding variable so slide stops
                if (isEnding) isEnding = false;

                switch (touch.phase)
                {
                    case TouchPhase.Began:

                        //Get Lastposition
                        lastposition = touch.position;

                        break;
                    case TouchPhase.Moved:

                        //Move Camera
                        Vector3 distanceTraveled = Camera.main.ScreenToViewportPoint((lastposition - touch.position) * -1);
                        lastposition = touch.position;

                        distanceTraveled = -distanceTraveled * Time.deltaTime * dragSpeed;

                        // Move
                        //transform.Translate(new Vector3(distanceTraveled.x, 0, distanceTraveled.z), Space.World);
                        controller.Move(new Vector3(distanceTraveled.x, 0, distanceTraveled.y));

                        // Get Velocity
                        velocity = GetVelocity(distanceTraveled.x, distanceTraveled.y, Time.deltaTime);

                        break;
                    case TouchPhase.Stationary:
                        break;
                    case TouchPhase.Ended:

                        isEnding = true;
                        velocity *= 10;
                        lerp = Vector3.Lerp(Vector3.zero, velocity, lowerVelocityBy);

                        break;
                    case TouchPhase.Canceled:
                        break;
                    default:
                        break;
                }

                break;

            case 2:

                Touch[] touches = new Touch[2] { Input.GetTouch(0), Input.GetTouch(1) };

                if (touches[0].phase == TouchPhase.Began || touches[1].phase == TouchPhase.Began)
                {
                    touchdistance = Vector2.Distance(touches[0].position, touches[1].position);
                }

                else if (touches[0].phase == TouchPhase.Moved || touches[1].phase == TouchPhase.Moved)
                {
                    zoomActive = true;
                }

                if (zoomActive)
                {
                    float zoomamount = Vector2.Distance(touches[0].position, touches[1].position) - touchdistance;

                    //transform.Translate(new Vector3(0, 0, zoomamount / 100), Space.Self);
                    //controller.Move(new Vector3(0, (-zoomamount/100)/2, (zoomamount / 100)/2));
                    controller.Move(new Vector3(0, -zoomamount / 100, 0));

                    touchdistance = Vector2.Distance(touches[0].position, touches[1].position);
                }

                break;
        }

        // !Not Working! Fix
        if (isEnding)
        {
            if (slideCounter < 1 / lowerVelocityBy)
            {
                Debug.Log("SlideCounter: " + slideCounter.ToString());
                Debug.Log("1 / lowerVelocityBy: " + (1 / lowerVelocityBy).ToString());
                Debug.Log("Velocity: " + velocity.ToString());
                controller.Move(velocity);
                velocity -= lerp;
                slideCounter++;
            }
            else
            {
                slideCounter = 0;
                isEnding = false;
            }
        }
    }

    private Vector3 GetVelocity(float xdistance, float zdistance, float time)
    {
        Vector3 returnvalue = new Vector3();

        //x velocity
        returnvalue.x = xdistance * time;

        //z velocity
        returnvalue.z = zdistance * time;

        return returnvalue;
    }
}
