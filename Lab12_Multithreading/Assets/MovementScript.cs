﻿using UnityEngine;
using System.Collections;
using System.Threading;
using System;

public class MovementScript : MonoBehaviour {

	public enum Directions
	{
		NORTH,
		EAST,
		SOUTH,
		WEST
	};

	public int damage = 1;

	public float moveSpeed = 1f;
	float moveX = 0;
	float moveZ = 0;

	public Directions direction = 0;

	public float directionChangeTime = 5f;

	// Use this for initialization
	void Start () {
		ThreadStart moveThread = new ThreadStart (MoveObject);

		Thread thread = new Thread (moveThread);

		thread.Start ();

		InvokeRepeating ("DirectionChange", directionChangeTime, directionChangeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			other.GetComponent<HealthScript>().UpdateUIHealth(damage);
		}
	}

	void MoveObject()
	{
		while (true) {
			transform.Translate(moveX * Time.deltaTime, 0, moveZ * Time.deltaTime);
		}
	}

	void DirectionChange()
	{
		int enumMax = Enum.GetValues (typeof(Directions)).Length;
		int next = UnityEngine.Random.Range (0, enumMax);

		if (direction == (Directions)next) {
			next = (next + 1) % enumMax;
		}

		moveX = moveZ = 0;

		switch ((Directions)next) {
			case Directions.NORTH:
				moveX += moveSpeed;
				break;
			case Directions.EAST:
				moveZ += moveSpeed;
				break;
			case Directions.SOUTH:
				moveX -= moveSpeed;
				break;
			case Directions.WEST:
				moveZ -= moveSpeed;
				break;
			default:
				break;
		}
	}
}
