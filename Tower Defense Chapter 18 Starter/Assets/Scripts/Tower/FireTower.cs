using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This script is for the fire tower prefab
 * This script  inherits from the tower.cs script
 * 
 * gets enemies that are in range and attacks them calling to the attackenemy() method in tower
 * spawns new fire particles
 * 
 * 
 * the object this tower shoots can be changed when linked to fireParticlePrefab setting in the firetower prefab
 * 
 */

public class FireTower : Tower {

    public GameObject fireParticlesPrefab;

    protected override void AttackEnemy()
    {

        base.AttackEnemy();
        Instantiate(fireParticlesPrefab, transform.position + new Vector3(0, .5f), fireParticlesPrefab.transform.rotation);
        // Scale fire particle radius with the aggro radius
        //particles.transform.localScale *= aggroRadius / 10f;
        foreach (Enemy enemy in EnemyManager.Instance.GetEnemiesInRange(transform.position, aggroRadius))
        {
            enemy.TakeDamage(attackPower);
        }
    }
}
