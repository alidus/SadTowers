using UnityEngine;
using System.Collections;

public class camera_movement : MonoBehaviour {
    float speed = 1f;
    Vector3 looking_point;
    float camera_height = 50f;
    float angle;
    Vector3 default_looking_point;
    float sensitivity = 5;
    float radius = 50;
    float max_center_shift = 40;
    float grads_to_rads(float grads)
        {
        return grads * Mathf.PI / 180;
        }
	// Use this for initialization
	void Start () {
        default_looking_point = new Vector3(50, 0, 50);
        looking_point = default_looking_point;
        transform.position = looking_point + new Vector3(0, camera_height, 0);    
        angle = 0;
        }
	void SetNewCamPos()
        {
        transform.position = looking_point + new Vector3(Mathf.Cos(grads_to_rads(angle)) * radius, 0, Mathf.Sin(grads_to_rads(angle)) * radius) + new Vector3(0, camera_height, 0);
        }
    void SetNewCamRot()
        {
        transform.rotation = Quaternion.LookRotation(looking_point - transform.position);
        }
    void TurnCamera(int dir)    // 0 - left, 1 - right
        {
        if (dir ==  0)
            {
            if (angle >= 0)
                {
                angle -= speed;
                }
            else
                {
                angle = 360;
                }
            }
        else if (dir == 1)
            {
            if (angle <= 360)
                {
                angle += speed;
                }
            else
                {
                angle = 0;
                }
            }
        
        }
    void MoveCameraCenterPoint(int dir) //0 - left 1 - top 2 - right 3 - bottom
        {
        Vector3 vec = (looking_point - transform.position).normalized;
        vec.y = 0;
        switch (dir)
            {
            case 0:
                vec = Quaternion.Euler(0, -90, 0) * vec;
                looking_point += vec * speed;
                break;
            case 1:
                looking_point += vec * speed;
                break;
            case 2:
                vec = Quaternion.Euler(0, 90, 0) * vec;
                looking_point += vec * speed;
                break;
            case 3:
                looking_point -= vec * speed;
                break;
            default:
                break;
            }
        if (looking_point.x < default_looking_point.x - max_center_shift)
            {
            looking_point.x = default_looking_point.x - max_center_shift;
            }
        else if (looking_point.x > default_looking_point.x + max_center_shift)
            {
            looking_point.x = default_looking_point.x + max_center_shift;
            }
        if (looking_point.z < default_looking_point.z - max_center_shift)
            {
            looking_point.z = default_looking_point.z - max_center_shift;
            }
        else if (looking_point.z > default_looking_point.z + max_center_shift)
            {
            looking_point.z = default_looking_point.z + max_center_shift;
            }
        SetNewCamPos();
        }
    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Q))
            {
            TurnCamera(0);
            }
        if (Input.GetKey(KeyCode.E))
            {
            TurnCamera(1);
            }
        if (Input.mousePosition.y >= Screen.height - sensitivity)
            {
            MoveCameraCenterPoint(1);
            }
        else if (Input.mousePosition.y <= sensitivity)
            {
            MoveCameraCenterPoint(3);
            }
        if (Input.mousePosition.x >= Screen.width - sensitivity)
            {
            MoveCameraCenterPoint(2);
            }
        else if (Input.mousePosition.x <= sensitivity)
            {
            MoveCameraCenterPoint(0);
            }
        //if (Input.mousePosition.x >= Screen.width - sensitivity)
        //    {

        //    }
        //else if (Input.mousePosition.x <= sensitivity)
        //    {

        //    }
        SetNewCamPos();
        SetNewCamRot();
	}
}
