﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, rotateSpeed = 5f;

    private bool dissipating = false;

    private Vector3 direction = Vector3.zero;

    public AudioClip soundEffect;
    private AudioSource soundSource;

    private bool piercing = false;

    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        soundSource.clip = soundEffect;
        Invoke("dissipate", 4);
        Invoke("selfDestruct", 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
        transform.position += direction * speed * Time.deltaTime;
        if (dissipating)
        {
            Color tmp = GetComponent<SpriteRenderer>().color;
            if(tmp.a < Time.deltaTime)
            {
                tmp.a = 0;
            }
            else
            {
                tmp.a -= Time.deltaTime;
            }
            GetComponent<SpriteRenderer>().color = tmp;
        }
    }

    public void dissipate()
    {
        dissipating = true;
    }

    public void setDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public void setPiercing(bool piercing)
    {
        this.piercing = piercing;
    }

    private void selfDestruct()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            soundSource.Play();
            if (!piercing)
            {
                this.GetComponent<CircleCollider2D>().enabled = false;
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<TrailRenderer>().enabled = false;
                Invoke("selfDestruct", 1);
            }
        }
    }
}
