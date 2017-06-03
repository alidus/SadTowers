using UnityEngine;
using System.Collections;

public class tower1_logic : towerLogic {
    [SerializeField]
    GameObject bullet_pf;

    private void Awake()
        {
        codeName = "tsimple1";
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
        Vector3 spawn_point = this.transform.FindChild("Cylinder").transform.position;
        bullet_1_logic bullet = Instantiate(bullet_pf, spawn_point, this.transform.rotation).GetComponent<bullet_1_logic>();
        bullet.target_enemy = target_enemy_go;
        bullet.damage = damage;
        }
}
