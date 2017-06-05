using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_4_logic : towerLogic
    {
    private GameObject projectileModel;
    private float preshootDelay = 5f;
    private float _preshootDelayCurrent = 5f;

    private void Awake()
        {
        codeName = "tdark1";
        projectileModel = Resources.Load("game_units/towers/ammo/tower_4_projectile") as GameObject;
        lightObject = new GameObject("The Light");
        lightComp = lightObject.AddComponent<Light>();
        lightComp.intensity = 1;
        lightComp.color = Color.magenta;
        lightObject.transform.position = transform.position;
        }


    public override void ParticularTurretMethod()
        {

        }

    public override void RotateToEnemyPoint(Vector3 enemyPosition)
        {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(enemyPosition - transform.position), Time.deltaTime * turnRate);
        }

    public override void Shoot()
        {
        Invoke("ShootProjectile", 3f);
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

    void ShootProjectile ()
        {
        print("Projectile shooted!");
        GameObject projectile = Instantiate(projectileModel, transform.position, transform.rotation);
        var projectileLogicComp = projectile.GetComponent<tower4projectileLogic>();
        projectileLogicComp.targetObject = target_enemy_go;
        projectileLogicComp.damage = damage;
        }

    }

