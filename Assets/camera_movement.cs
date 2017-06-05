using UnityEngine;
using System.Collections;

public class camera_movement : MonoBehaviour {
    float speedWithScreenSidesTouches = 1f;
    int reactingZonesWide = 10;
    int cameraRotationSpeed = 2;
    float height = 50f;

    private Vector3 cameraMovementVector = Vector3.zero;
    private float[] positionLimitBox = new float[2];

    private Camera cameraComp;
    private Vector3 dragOrigin;
    private Vector3 oldDragPosition;
    private bool isDragging = false;

 
	// Use this for initialization
	void Start () {
        setLimitBox();
        cameraComp = GetComponent<Camera>();
        }
    public void setLimitBox(float x = 150, float y = 150)
        {
        positionLimitBox[0] = x;
        positionLimitBox[1] = y;
        }
    private void Update()
        {
        cameraMovementVector = Vector3.zero;
        RotateCamera();
        //if (Input.GetMouseButtonDown(0))
        //    {
        //    dragOrigin = Input.mousePosition;
        //    oldDragPosition = transform.position;
        //    isDragging = true;
        //    }
        //else if (Input.GetMouseButtonUp(0))
        //    {
        //    isDragging = false;
        //    }

        //if (Input.GetMouseButton(0))
        //    {
        //    Vector3 pos = cameraComp.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        //    transform.position = oldDragPosition - pos * 10;
        //    }
        

        checkScreenSidesTouches();
        if (cameraMovementVector != Vector3.zero)
            {
            AdjustMovementVectorToRotation();
            MoveCamera();
            
            }
        ConsiderLimitBox();

        
        }

    private void checkScreenSidesTouches()
        { 
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
        }

    private void ConsiderLimitBox()
        {
        if (transform.position.x < -(positionLimitBox[0]-100))
            {
            transform.position = new Vector3(-(positionLimitBox[0] - 100), transform.position.y, transform.position.z);
            }
        else if (transform.position.x > positionLimitBox[0])
            {
            transform.position = new Vector3(positionLimitBox[0], transform.position.y, transform.position.z);
            }
        if (transform.position.z < -(positionLimitBox[1] - 100))
            {
            transform.position = new Vector3(transform.position.x, transform.position.y, -(positionLimitBox[1] - 100));
            }
        else if (transform.position.z > positionLimitBox[1])
            {
            transform.position = new Vector3(transform.position.x, transform.position.y, positionLimitBox[1]);
            }
        }

    }
