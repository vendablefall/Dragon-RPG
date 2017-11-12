using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (AICharacterControl))]
public class PlayerMovement : MonoBehaviour
{
    public static bool isInDirectMode = false;
    ThirdPersonCharacter thirdPersonCharicter = null;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster = null;
    Vector3 currentDestination, clickPoint;
    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;
    [SerializeField] const int walkableLayerNumber = 8;
	[SerializeField] const int enemyLayerNumber = 9;
    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharicter = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
        aiCharacterControl = GetComponent<AICharacterControl>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;
        walkTarget = new GameObject("walkTarget");
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayerNumber:
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayerNumber:
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                print("walkign to target!!!");
                break;
            default:
                Debug.Log("Dont know how to process this movement!!");
                return;
        }
    }




    // TODO make this accessable again.
    // void ProcessDirectMovement()
    // {
    //         float h = Input.GetAxis("Horizontal");
    //         float v = Input.GetAxis("Vertical");

    //         // calculate camera relative direction to move:
    //         Vector3 moveForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
    //         Vector3 movement = v*moveForward + h*Camera.main.transform.right;
    //          thirdPersonCharicter.Move(movement, false, false);
    // }


}