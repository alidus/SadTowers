using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower4projectileLogic : MonoBehaviour {
    public GameObject targetObject;
    private float speed = 15f;
    public float damage;
    private Vector3 vectorToTarget;

	
	// Update is called once per frame
	void Update () {
        if (targetObject != null)
            {
            vectorToTarget = transform.position - targetObject.transform.position;
            transform.position = transform.position - (vectorToTarget).normalized * speed * Time.deltaTime;
            CheckContact();
            }
	}

    void CheckContact()
        {
        if (vectorToTarget.magnitude < 1)
            {
            targetObject.GetComponent<enemy_logic>().changeHealth(-damage, "Tower 4 projectile");
            Destroy(this.gameObject);
            }
        }
}
