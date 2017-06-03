using UnityEngine;
using System.Collections;
using System;

public class bullet_1_logic : MonoBehaviour {
    public GameObject target_enemy;
    private Vector3 direction;
    private float speed = 70f;
    private float dist_per_frame;
    public float damage;

    // Update is called once per frame
    void Update() {
        dist_per_frame = speed * Time.deltaTime;
        try
            {
            direction = target_enemy.transform.position - transform.position;
            }
        catch (Exception)
            {
            Destroy(gameObject);
            }
        transform.Translate(direction.normalized * dist_per_frame, Space.World);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3);
        if (direction.magnitude <= dist_per_frame)
            {
            Hit();
            }
        }
    void Hit()
        {
        if (target_enemy)
            {
            enemy_logic enemy_logic = target_enemy.GetComponent<enemy_logic>();
            enemy_logic.changeHealth(-damage, "Turrent 1 bullet");
            Destroy(gameObject);
            Instantiate(Resources.Load("game_units/towers/bullet_explosion"), transform.position, transform.rotation);
            }
        else
            {
            Destroy(gameObject);
            }
        }
}
