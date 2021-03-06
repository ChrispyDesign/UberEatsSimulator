﻿// Main Author - Afridi Rahim
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BCameraController : MonoBehaviour
{

    public GameObject Empty;
    public GameObject Player;
    public float speed = 1.0f;
    public Camera Main;

    private Vector3 Vel = Vector3.zero;
    void Awake()
    {
        Main = Camera.main;
    }

    void FixedUpdate()
    {
		float useThis = 1 / speed;

		Main.transform.LookAt(Player.transform);
        Main.transform.position = Vector3.SmoothDamp(Main.transform.position, Empty.transform.position, ref Vel, useThis);
        //Main.transform.rotation = Empty.transform.rotation;
    }
}
