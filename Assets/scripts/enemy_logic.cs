using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemy_logic : MonoBehaviour {
    // variables
    private float speed;
    private float health;
    public int bounty;
    private float turn_rate = 1.5f;
    private int target_waypoint_index = -1;
    private GameObject waypoints;
    private Transform target_waypoint_transform;
    private Vector3 dir_vector;
    private float dist_for_frame;
    private Text textcomponent;
    private game_logic game_logic;
    // Use this for initialization
    void Start () {
        textcomponent = gameObject.transform.FindChild("hp_text_canva").transform.FindChild("Text").GetComponent<Text>();
        waypoints = GameObject.Find("Waypoints");
        targetNextWaypoint();
        game_logic = GameObject.Find("GameLogic").GetComponent<game_logic>();
        speed = 5f + game_logic.time_passed/90;
	}
	void targetNextWaypoint()
        {
        target_waypoint_index += 1;
        if (waypoints.transform.childCount > target_waypoint_index)
            {
            target_waypoint_transform = waypoints.transform.GetChild(target_waypoint_index);
            }
        else
            {
            game_logic.SubstractLifes(1);
            Destroy(gameObject);
            }
        }

    public void changeHealth(float value, string info)
        {
        health += value;
        print("Now health is "+ health);
        }

	// Update is called once per frame
	void Update () {
        dir_vector = target_waypoint_transform.position - this.transform.localPosition;
        dist_for_frame = Time.deltaTime * speed;
        dir_vector.y = 0;
        if (dir_vector.magnitude <= dist_for_frame)
            {
            targetNextWaypoint();
            }
        else
            {
            transform.Translate(dir_vector.normalized * dist_for_frame, Space.World);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(dir_vector), Time.deltaTime * turn_rate);
            }
        if (health <= 0)
            {
            Destroy(gameObject);
            GameObject.Find("GameLogic").GetComponent<game_logic>().AddMoney(bounty);
            Instantiate(Resources.Load("game_units/enemies/enemy_explosion"), transform.position, transform.rotation);
            }
        textcomponent.text = health.ToString();
       
        }
}
