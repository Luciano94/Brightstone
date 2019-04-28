﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject sword;

    private void Update() {
        Movement();
        Rotation();
    }

    private void Rotation() {

        Vector3 diff = player.position - transform.position;

        Vector3 dir = diff.normalized;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        sword.transform.rotation = Quaternion.Euler(0, 0, angle + 90.0f);
    }

    private void Movement() {

        Vector3 diff = player.position - transform.position;
        float dist = diff.magnitude;
        if(dist > 1.5f)
            transform.Translate(diff.normalized * speed * Time.deltaTime); 
    }
}