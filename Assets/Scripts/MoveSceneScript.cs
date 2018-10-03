using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneScript : MonoBehaviour
{
	
	public Scene MoveScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space))
		{
			if (SceneManager.GetActiveScene().name == "GameScene")
			{
				GoStartScene();
			}
			else
			{
				GoGameScene();
			}
		}
	}
	
	public void GoGameScene()
	{
		SceneManager.LoadScene("GameScene");
	}
	
	public void GoStartScene()
	{
		SceneManager.LoadScene("TitleScene");
	}
}
