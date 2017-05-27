using UnityEngine;
using System.Collections;

public class tower2_neck_logic : MonoBehaviour {
    private GameObject target_enemy;
    private Quaternion default_rotation;
    private tower2_head_logic head_logic;
    private float turn_rate = 3f;
	// Use this for initialization
	void Start () {
        default_rotation = transform.rotation;
        head_logic = transform.FindChild("tower_head").GetComponent<tower2_head_logic>();
	}
	
	// Update is called once per frame
	void Update () {
        target_enemy = head_logic.target_enemy_go;
        Rotate();
	}
    void Rotate()
        {
        if (target_enemy)
            {
            Vector3 target_look_vector = target_enemy.transform.position - transform.position;
            
            target_look_vector.y = 0;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target_look_vector), Time.deltaTime * turn_rate);
            }
        else
            {
            transform.rotation = Quaternion.Lerp(transform.rotation, default_rotation, Time.deltaTime * turn_rate);
            }
        }
}
