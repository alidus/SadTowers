using UnityEngine;
using System.Collections;

public class camera_movement : MonoBehaviour {
    float speedWithScreenSidesTouches = 1f;
    int reactingZonesWide = 10;
    int cameraRotationSpeed = 2;
    float height = 50f;

    private Vector3 cameraMovementVector = new Vector3(0, 0, 0);
    private int[] positionLimitBox = new int[2];

    private Vector3 dragOrigin;
    public float dragSpeed = 2;
 
	// Use this for initialization
	void Start () {
        setLimitBox();
        }
    public void setLimitBox(int x = 50, int y = 50)
        {
        positionLimitBox[0] = x;
        positionLimitBox[1] = y;
        }
    private void Update()
        {
        checkScreenSidesTouches();
        if (cameraMovementVector != Vector3.zero)
            {
            AdjustMovementVectorToRotation();
            MoveCamera();
            ConsiderLimitBox();
            }
        RotateCamera();
        }

    private void checkScreenSidesTouches()
        {
        cameraMovementVector = Vector3.zero;
        if (Input.mousePosition.x <= reactingZonesWide)
            {
            cameraMovementVector = new Vector3(-speedWithScreenSidesTouches, 0, 0);
            }
        else if (Input.mousePosition.x >= Screen.width - reactingZonesWide)
            {
            cameraMovementVector = new Vector3(speedWithScreenSidesTouches, 0, 0);
            }
        if (Input.mousePosition.y <= reactingZonesWide)
            {
            cameraMovementVector = new Vector3(0, 0, -speedWithScreenSidesTouches);
            }
        else if (Input.mousePosition.y >= Screen.height - reactingZonesWide)
            {
            cameraMovementVector = new Vector3(0, 0, speedWithScreenSidesTouches);
            }
        }

    private void AdjustMovementVectorToRotation()
        {
        cameraMovementVector = Quaternion.Euler(0, transform.eulerAngles.y, 0) * cameraMovementVector;
        }

    private void MoveCamera()
        {

        transform.Translate(cameraMovementVector, Space.World);
        }

    private void RotateCamera()
        {
        if (Input.GetKey(KeyCode.Q))
            {
            transform.Rotate(new Vector3(0, -cameraRotationSpeed, 0), Space.World);
            }
        if (Input.GetKey(KeyCode.E))
            {
            transform.Rotate(new Vector3(0, cameraRotationSpeed, 0), Space.World);
            }
        print(transform.eulerAngles);
        }

    private void ConsiderLimitBox()
        {
        if (transform.position.x < -positionLimitBox[0])
            {
            transform.position = new Vector3(-positionLimitBox[0], transform.position.y, transform.position.z);
            }
        else if (transform.position.x > positionLimitBox[0])
            {
            transform.position = new Vector3(positionLimitBox[0], transform.position.y, transform.position.z);
            }
        if (transform.position.z < -positionLimitBox[1])
            {
            transform.position = new Vector3(transform.position.x, transform.position.y,  -positionLimitBox[1]);
            }
        else if (transform.position.z > positionLimitBox[1])
            {
            transform.position = new Vector3(transform.position.x, transform.position.y, positionLimitBox[1]);
            }
        }

    }
