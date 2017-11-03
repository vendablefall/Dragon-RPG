using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    public static bool isInDirectMode = false;

    [SerializeField] float walkMoveStopRadius = 1f;
    [SerializeField] float attackMoveStopRadius = 3f;

    ThirdPersonCharacter thirdPersonCharicter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;


        
    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharicter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G)) //g for gamepad TODO allow player to remap later
        {
            isInDirectMode = !isInDirectMode;
            currentDestination = transform.position;
            Cursor.visible = !Cursor.visible;
        }
        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            // calculate camera relative direction to move:
            Vector3 moveForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 movement = v*moveForward + h*Camera.main.transform.right;
             thirdPersonCharicter.Move(movement, false, false);
    }

    private void ProcessMouseMovement()
    {
           if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;
              // So not set in default case
           switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                currentDestination = ShortDistance(clickPoint,walkMoveStopRadius);
                //currentDestination = clickPoint;
                break;

                case Layer.Enemy:
                print("moving to enemy");
                currentDestination = ShortDistance(clickPoint,attackMoveStopRadius);
                break;

                default:
                print("Unexpected layer found");
                break;
            }
        }

        WalkToDestination();

    }

    void WalkToDestination()
    {       
        var playToclickPoint =  currentDestination - transform.position ;

        if (playToclickPoint.magnitude >= 0.2)
        {
            thirdPersonCharicter.Move(playToclickPoint, false, false);
        }
        else
        {
            thirdPersonCharicter.Move(Vector3.zero, false, false);
        }
        
    }


    Vector3 ShortDistance(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        //draw movement spheres and lines
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position,currentDestination); 
        Gizmos.DrawSphere(clickPoint,0.15f);
        Gizmos.DrawSphere(currentDestination,0.22f);

        //draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, attackMoveStopRadius);
        
    }
}

