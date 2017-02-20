using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionController : MonoBehaviour
{
    public GameObject Globe;
    public GameObject Clouds;
    public float RotationSpeed = 0.025f;
    public float lowerByGlide = 0.01f;
    public float cloudSpeed = 1;
    public float rotateSpeed = 5;
    private bool isRotating = false;
    private Vector2 lastPos;
    private Vector2 Velocity;
    private Vector2 lowerByVector;
    private int lowerCounter;
    private float screenRelation;


    private void Awake()
    {
        screenRelation = Mathf.Abs(Screen.height / Screen.width) *-1;
    }

    void Update()
    {
        //if only one finger is on the screen
        if (Input.touchCount == 1)
        {

            Touch touch = Input.GetTouch(0);

            Vector2 deltaPos = (touch.position - lastPos) *-1;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isRotating = true;
                    lastPos = touch.position;
                    break;
                case TouchPhase.Moved:
                    //may  not be necessary
                    if (isRotating)
                    {
                        //Rotate the World
                        Globe.transform.Rotate(new Vector3(deltaPos.y * -RotationSpeed, deltaPos.x * RotationSpeed) * Time.deltaTime * rotateSpeed, Space.World);
                        lastPos = touch.position;

                    }
                    break;
                case TouchPhase.Ended:
                    //set velocity for glide movement
                    //Velocity = new Vector2(-deltaPos.y, deltaPos.x) * 0.1f;
                    Vector3 relativeVelocity = Camera.main.ScreenToViewportPoint(deltaPos);
                    Velocity = new Vector2(relativeVelocity.y* screenRelation, relativeVelocity.x) * 100f;

                    Debug.Log(deltaPos);
                    //this is the value that needs to be subracted from the velocity every frame
                    lowerByVector = Vector2.Lerp(Vector2.zero, Velocity, lowerByGlide);
                    lowerCounter = 0;


                    //if tabbed on tower -> load level
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit[] hits = Physics.RaycastAll(ray);
                    foreach (var hit in hits)
                    {
                        Tower tower = hit.collider.gameObject.GetComponent<Tower>();
                        if (tower == null)
                        {
                            continue;
                        }
                        Debug.Log(tower.LevelName);
                        //SceneManager.LoadScene(tower.LevelName);
                        break;
                    }
                    isRotating = false;
                    break;
                case TouchPhase.Canceled:
                    isRotating = false;
                    break;
                default:
                    break;
            }
        }

        //Gliding 
        if (!isRotating && Velocity != Vector2.zero && lowerCounter < 1 / lowerByGlide)
        {
            Globe.transform.Rotate(new Vector3(Velocity.x, Velocity.y, 0), Space.World);
            Velocity -= lowerByVector;

            lowerCounter++;
        }

        //Cloud movement
        Clouds.transform.Rotate(Vector3.one * Time.deltaTime * cloudSpeed);
    }


    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
