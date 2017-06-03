using UnityEngine;
using System.Collections;
using System;
public class tower_2_rocket_logic : MonoBehaviour {
    public GameObject target_enemy;
    private Vector3 direction;
    public float speed;
    private float dist_per_frame;
    public float damage;
    private float stage2_radius = 0;
    private float start_distance;
    // Use this for initialization
    void Start()
        {
        if (target_enemy)
            {
            stage2_radius = (target_enemy.transform.position - transform.position).magnitude / 3;
            }
        }

    // Update is called once per frame
    void Update()
        {
        dist_per_frame = speed * Time.deltaTime;
        try
            {
            CalculateDirection();
            }
        catch (Exception)
            {
            // target disappeared
            Destroy(gameObject);
            }
        transform.Translate(direction.normalized * dist_per_frame, Space.World);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2);
        if (direction.magnitude <= dist_per_frame)
            {
            Hit();
            }
        }
    void CalculateDirection()
        {
        Vector3 vector_to_enemy = target_enemy.transform.position - transform.position;
        float distanse_to_target = vector_to_enemy.magnitude;
        if (distanse_to_target >= stage2_radius)
            {
            // stage 1
            direction = vector_to_enemy + new Vector3(0, stage2_radius * 0.7f, 0);
            }
        else
            {
            // stage 2
            direction = vector_to_enemy;
            }
        }
    void Hit()
        {
        if (target_enemy)
            {
            enemy_logic enemy_logic = target_enemy.GetComponent<enemy_logic>();
            enemy_logic.changeHealth(-damage, "Turret 2 Rocket");
            Destroy(gameObject);
            Instantiate(Resources.Load("game_units/towers/rocket_explosion"), transform.position, transform.rotation);
            }
        else
            {
            Destroy(gameObject);
            }
        }
    }
