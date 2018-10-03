using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public CameraControl camera;
	public GameObject ScoreText;
	public GameObject GameText;
	public GameObject Sound;
	public int Resporn; 
	private Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);
	private Vector3 accel = new Vector3(0.002f, -0.002f, 0.0f);
	private bool invincible = false;
	private float maxSpeed = 0.12f;
	public int Score=0;
	public float Pitch;//奥行き
	private float depth = 0.0f;
	private long lastTimeStamp;
	// Use this for initialization
	void Start () {
		OSCHandler.Instance.Init();
		accel.x = 0.002f;
		accel.y = -0.002f;
		Score = 0;
		ScoreText.gameObject.GetComponent<Text>().text = "Score : " + Score;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ScoreText.gameObject.GetComponent<Text>().text = "Score : " + Score;
		if (Resporn < transform.position.x)
		{
			transform.position = new Vector3
				(-10.0f,
				transform.position.y,
				depth);
		} 
		Pitch = Input.GetAxis("Horizontal");
		Pitch = Pitch / 2.0f + 0.5f;
		//if (Input.GetKey(KeyCode.Space))
		//{
		//	Jump();
		//}

		if (Input.GetKey(KeyCode.A))
		{
			KnockBuck();
		}
		OscReceived();
		depth = -Pitch + 0.5f;
		depth *= 5.0f;
		speed.y += accel.y;
		if (speed.x < maxSpeed)
		{
			speed.x += accel.x;
		}
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			depth);
		transform.Translate(speed.x, speed.y, 0);
		if (transform.position.y < -1.7f)
		{
			transform.position = new Vector3(
				transform.position.x,
				-1.7f,
				depth);
		}
		if (speed.y > 0.0f)
		{
			speed.y += accel.y;
		}
	}

	//OSCメッセージの受信
	private void OscReceived()
	{
		OSCHandler.Instance.UpdateLogs();
		foreach (KeyValuePair<string, ServerLog> item in OSCHandler.Instance.Servers) {
			for (int i=0; i < item.Value.packets.Count; i++) {
				if (lastTimeStamp < item.Value.packets[i].TimeStamp) {
					lastTimeStamp = item.Value.packets[i].TimeStamp;
					//  アドレスパターン（文字列）
					string address = item.Value.packets[i].Address;
					//  引数（とりあえず最初の引数のみ）
					var arg0 = item.Value.packets[i].Data[0];
					//Pitch = (float) arg0;
					//Debug.Log(address + ":" + arg0);
				}
			}
		}
	}
	//isTrigger false
/*
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Biscuit"))
		{
			GameObject.Destroy(collision.gameObject, 0.2f);
		}

		if (collision.gameObject.CompareTag("Enemy"))
		{
			KnockBuck();
		}
	}
	*/
	
	//isTrigger True
	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.CompareTag("Biscuit"))
		{
			GameObject.Destroy(collision.gameObject, 0.1f);
			ScoreChange(1000);
			Sound.GetComponent<SoundManager>().Yatta();
			//score += 1000;
		}

		if (collision.gameObject.CompareTag("Enemy"))
		{
			ScoreChange(-500);
			KnockBuck();
			Sound.GetComponent<SoundManager>().Ita();
		}
		if (collision.gameObject.CompareTag("Goal"))
		{
			GameText.gameObject.GetComponent<Text>().text = "Game Clear!\nScore:"+Score;
			Sound.GetComponent<SoundManager>().Goal();	
		}
		if (collision.gameObject.CompareTag("Jump"))
		{
			Jump();
		}
		
	}

	private void KnockBuck()
	{
		camera.Shake(0.25f, 0.2f);
		speed.x = -0.1f;
		//score -= 500;
		if (Math.Abs(speed.y) > 0.0f)
		{
			speed.y = 0.1f;
		}
	}
	
	//ジャンプ
	private void Jump()
	{
		if (Math.Abs(speed.y) > 0.0f)
		{
			speed.y = 0.16f;	
		}
		
	}
	
	private void ScoreChange(int sc)
	{
		if (Score + sc > 0)
		{
			Score += sc;
		}
		ScoreText.gameObject.GetComponent<Text>().text = "Score : " + Score;
	}
	
	//無敵状態
	private void InvincibleTime()
	{
		
	}
}
