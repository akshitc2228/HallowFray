using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    //reference of the boss:
    public Transform boss;

    //for object detection within camera bounds:
    private float viewportWidth;
    private float viewportHeight;

    //specifying camera bounds:
    [SerializeField]
    private float minX = -296.8521f, maxX = 282.3f;

    [SerializeField]
    private float xOffset = 32.7f, yOffset = 34.8f;

    private Camera mainCamera;

    private bool cameraStateChanged = false;

    private enum CameraState
    {
        FollowPlayer,
        Frozen
    }

    private CameraState cameraState = CameraState.FollowPlayer;

    private void SetCameraState(CameraState newState)
    {
        if (newState != cameraState)
        {
            cameraState = newState;
            cameraStateChanged = true;
        }
    }

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        viewportHeight = 2f * mainCamera.orthographicSize; //Since the orthographicSize represents half the vertical size,
                                                                //multiplying it by 2 gives you the full vertical size of the camera's view.
        viewportWidth = viewportHeight * mainCamera.aspect; //The aspect property of the camera component represents the aspect ratio (width/height) of the camera's view.
                                                                 //Multiplying the viewportHeight by the aspect gives you the corresponding width of the camera's view.
    }

    // Start is called before the first frame update
    void Start()
    {
        //find player
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (!player)
        {
            return;
        }

        if (cameraState == CameraState.FollowPlayer)
        {
            tempPos = transform.position;
            tempPos.x = player.position.x + xOffset;
            tempPos.y = player.position.y + yOffset;

            if (tempPos.x < minX)
            {
                tempPos.x = minX;
            }

            if (tempPos.x > maxX)
            {
                tempPos.x = maxX;
            }

            transform.position = tempPos;
        }

        if(cameraState == CameraState.Frozen)
        {
            tempPos = transform.position;
            tempPos.x = Mathf.Clamp(player.position.x + xOffset, minX, maxX);
            tempPos.y = player.position.y + yOffset;

            // Clamp the player's position within the camera bounds
            player.position = new Vector3(tempPos.x - xOffset, tempPos.y - yOffset, player.position.z);

            transform.position = tempPos;
        }

        // Check if the boss is visible in the camera's viewport
        if (boss)
        {
            Vector3 bossViewportPos = mainCamera.WorldToViewportPoint(boss.position);
            if (bossViewportPos.x >= 0 && bossViewportPos.x <= 1 && bossViewportPos.y >= 0 && bossViewportPos.y <= 1)
            {
                SetCameraState(CameraState.Frozen);
                DialogueDelegateEvent.Instance.SetBossInSight();
                //from this point camera should freeze until phase-3 which is collapsing platforms or if you want it simple, until the fight is over
                //and the boss moves freely
            }
        }
    }
}
