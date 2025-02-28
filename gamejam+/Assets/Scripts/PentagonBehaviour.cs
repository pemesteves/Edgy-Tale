﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagonBehaviour : Enemy
{
    public AudioClip soundEffectShot;

    private AudioSource soundSourceShot;

    private GameObject bulletPrefab;

    bool shotLock = true;

    private List<GameObject> enemyBullets;

    protected override void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("EnemyBullet");
        soundSourceShot = GetComponent<AudioSource>();
        soundSourceShot.clip = soundEffectShot;
        Invoke("toggleShotLock", 1);
        enemyBullets = new List<GameObject>();
        base.Start();
    }

    protected override void Update()
    {
        if (shotLock)
        {
            toggleShotLock();
            spawnBullets();
            Invoke("toggleShotLock", 3);
        }
        base.Update();
    }
    void spawnBullets()
    {
        soundSource.Play();
        Vector3 bulletDir = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        enemyBullets.Add(bullet);
        bullet.GetComponent<EnemyBullet>().setDirection(bulletDir);
    }

    private void toggleShotLock()
    {
        shotLock = !shotLock;
    }
}
