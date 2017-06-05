using UnityEngine;
using System.Collections;

public class tower_3_logic : towerLogic

    {

    private void Awake()
        {
        codeName = "tflame1";

        lightObject = new GameObject("The Light");
        lightComp = lightObject.AddComponent<Light>();
        lightComp.intensity = 5;
        lightComp.color = Color.yellow;
        lightObject.transform.position = transform.position;
        }

    private GameObject FlameParticles;

    public override void ParticularTurretMethod()
        {
        Destroy(FlameParticles);
        }

    public override void RotateToEnemyPoint(Vector3 enemyPosition)
        {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(enemyPosition-transform.position), Time.deltaTime * turnRate);
        }

    public override void Shoot()
        {
        if (!isFireing)
            {
            isFireing = true;
            Vector3 spawn_point = this.transform.position + new Vector3(0, 0, 0);
            FlameParticles = Instantiate(Resources.Load("game_units/enemies/FlamethrowerFlame"), spawn_point, transform.rotation) as GameObject;
            }
        else
            {
            FlameParticles.transform.rotation = this.transform.rotation;
            }

        Invoke("dealDelayedDamage", 0.5f);
        }
        
    void dealDelayedDamage()
        {
        try
            {
            enemy_logic enemy_logic = target_enemy_go.GetComponent<enemy_logic>();
            enemy_logic.changeHealth(-damage, "Tower 3 Flame");
            }

        catch (UnityException)
            {
            // Enemy is dead
            }
        }
    
    }

