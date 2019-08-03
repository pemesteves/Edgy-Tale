﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Girlfriend : MonoBehaviour
{
    public float speed = 5f;
    public float reflectSpeed = 5f;
    public float stopDistance = 1f;

    private bool movement_lock = false;
    private Transform player;

    private bool trueEnding = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        InvokeRepeating("increaseSpeed", 2f, 2f);

        movement_lock = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= stopDistance)
        {
            if (trueEnding)
                TrueEnding();
            else
                EndGame();
        }
        if (!movement_lock)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void setEnding(bool ending)
    {
        trueEnding = ending;
    }

    private void EndGame()
    {
        movement_lock = true;
        foreach(Transform obj in GameObject.Find("GirlfriendMsgLose").transform)
        {
            obj.gameObject.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().enabled = false;
        if (player.GetComponentInChildren<PolyShooter>())
            player.GetComponentInChildren<PolyShooter>().enabled = false;

        //Invoke game over screen
    }

    private void TrueEnding()
    {
        movement_lock = true;
        foreach (Transform obj in GameObject.Find("GirlfriendMsgWin").transform)
        {
            obj.gameObject.SetActive(true);
        }
        player.GetComponent<PlayerMovement>().enabled = false;
    }
    
    private void increaseSpeed()
    {
        speed += 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet)
        {
            Vector3 dir = (transform.position - collision.transform.position).normalized;
            bullet.setDirection(-dir);
            bullet.setSpeed(reflectSpeed);
            
        }
    }
}
