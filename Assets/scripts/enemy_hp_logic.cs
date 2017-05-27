using UnityEngine;
using System.Collections;

public class enemy_hp_logic : MonoBehaviour {
    private Transform camera_transform;

	// Update is called once per frame
	void Update () {
        camera_transform = GameObject.Find("Camera").transform;
        transform.rotation = camera_transform.rotation;
	}
}
