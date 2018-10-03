using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public GameObject player;
	public int SceneNum;
	private float posX = -10000.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (posX < player.transform.position.x)
		{
			posX = player.transform.position.x;
		}

		if (SceneNum == 1)
		{
			posX = player.transform.position.x;
		}
		transform.position = new Vector3(posX+5, 2, -10);
	}
	
	public void Shake( float duration, float magnitude )
	{
		StartCoroutine( DoShake( duration, magnitude ) );
	}

	private IEnumerator DoShake( float duration, float magnitude )
	{
		var pos = transform.localPosition;

		var elapsed = 0f;

		while ( elapsed < duration )
		{
			var x = pos.x + Random.Range( -1f, 1f ) * magnitude;
			var y = pos.y + Random.Range( -1f, 1f ) * magnitude;

			transform.localPosition = new Vector3( x, y, pos.z );

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = pos;
	}
}
