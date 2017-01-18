﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private Vector2 velocity;
	public float smoothTimeX, smoothTimeY;
	private GameObject player;

	private float minCamPosX, maxCamPosX, minCamPosY, maxCamPosY;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");

		float boundLeft = GameObject.FindGameObjectWithTag("BoundLeft").transform.position.x;
		float boundRight = GameObject.FindGameObjectWithTag("BoundRight").transform.position.x;
		float boundTop = GameObject.FindGameObjectWithTag("BoundTop").transform.position.y;
		float boundBottom = GameObject.FindGameObjectWithTag("BoundBottom").transform.position.y;

		float camVertExtent = Camera.main.orthographicSize;
		float camHorizExtent = camVertExtent * Screen.width / Screen.height;

		maxCamPosX = boundRight - camHorizExtent;
		minCamPosX = boundLeft + camHorizExtent;
		maxCamPosY = boundTop - camVertExtent;
		minCamPosY = boundBottom + camVertExtent;
	}
	
	void LateUpdate () {
		float camPosX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float camPosY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(camPosX, camPosY, transform.position.z);

		transform.position = new Vector3(	Mathf.Clamp(transform.position.x, minCamPosX, maxCamPosX), 
											Mathf.Clamp(transform.position.y, minCamPosY, maxCamPosY),
											transform.position.z);
	}
}
