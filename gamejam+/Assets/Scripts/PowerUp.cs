﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Health,
    Super_Edginess,
    Bullet,
    Points,
    Shield
}

public class PowerUp : MonoBehaviour
{
    public static float health;

    public static PowerUpType GetPowerUp()
    {
        int random = Random.Range(0, 5);

        return (PowerUpType)random;
    }

    public static string PowerUpName(PowerUpType powerUp)
    {
        switch (powerUp)
        {
            case PowerUpType.Health:
                return "pill";
            case PowerUpType.Super_Edginess:
                return "blue_pill";
            case PowerUpType.Bullet:
                return "ammo";
            case PowerUpType.Points:
                return "dollar_sign";
            case PowerUpType.Shield:
                return "shield";
            default:
                return "pill";
        }
    }

    public PowerUpType type;
    private HealthHandler healthHandler;
    private EdginessHandler edginessHandler;
    private PolyShooter polyShooter;

    private PowerUpManager powerUpManager;

    void Start()
    {
        switch (type)
        {
            case PowerUpType.Health:
            case PowerUpType.Shield:
                healthHandler = GameObject.FindObjectOfType<HealthHandler>();
                break;
            case PowerUpType.Super_Edginess:
            case PowerUpType.Points:
                edginessHandler = GameObject.FindObjectOfType<EdginessHandler>();
                break;
            case PowerUpType.Bullet:
                polyShooter = GameObject.FindObjectOfType<PolyShooter>();
                break;
            default:
                Destroy(this);
                break;
        }
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();

        Invoke("DestroySelf", 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!powerUpManager.isPowerUpActive() && collision.gameObject.CompareTag("Polygon"))
        {
            switch (type)
            {
                case PowerUpType.Health:
                    healthHandler.changeHealth(health);
                    break;
                case PowerUpType.Super_Edginess:
                    edginessHandler.StartEdginessShield();
                    powerUpManager.activatePowerUp(type);
                    break;
                case PowerUpType.Bullet:
                    polyShooter.StartBiggerBullets();
                    powerUpManager.activatePowerUp(type);
                    break;
                case PowerUpType.Points:
                    edginessHandler.StartDuplicatePoints();
                    powerUpManager.activatePowerUp(type);
                    break;
                case PowerUpType.Shield:
                    healthHandler.StartShield();
                    powerUpManager.activatePowerUp(type);
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
        }
    }

    private void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
