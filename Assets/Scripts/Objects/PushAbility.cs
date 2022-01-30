﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbility : MonoBehaviour
{
    private float pushForce;
    private float pushDuration;
    private float currentDuration;
    public float PushForce { set => pushForce = value; }
    public float PushDuration { set => pushDuration = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Character>() != null)
        {
            GameObject enemyObject = collision.gameObject.GetComponent<Character>().gameObject;
            Character enemy = enemyObject.GetComponent<Character>();

            Vector2 direction = enemyObject.transform.position - transform.position;

            Debug.Log("Push triggered. Pushing away in direction: " + direction + " at power: " + pushForce);
            enemy.PushForce = pushForce;
            enemy.PushDirection = direction;
            enemy.IsPushed = true;
        }
    }

    private void Update()
    {
        DisableGameObject();
    }

    private void DisableGameObject()
    {
        currentDuration -= Time.deltaTime;

        if (currentDuration <= 0)
            gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        currentDuration = pushDuration;
    }

    private void OnEnable()
    {
        currentDuration = pushDuration;
    }
}