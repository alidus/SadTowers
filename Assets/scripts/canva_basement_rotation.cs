using UnityEngine;
using System.Collections;

public class canva_basement_rotation : MonoBehaviour {
    Transform camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Camera").transform;
        transform.rotation = Quaternion.LookRotation(-(camera.position - transform.position));
        gameObject.GetComponent<Canvas>().worldCamera = camera.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (gameObject.GetComponent<Canvas>().enabled)
            {
            transform.rotation = Quaternion.LookRotation (transform.position - camera.position);
            }
	}
}
