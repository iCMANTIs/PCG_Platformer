using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
	Rigidbody2D rigidBody;
	BoxCollider2D box;
	Vector3 originalPosition;
	Vector3 velocity =Vector3.zero;
	void Start()
	{
		rigidBody=GetComponent<Rigidbody2D>();
		originalPosition=rigidBody.position;

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			rigidBody.isKinematic=false;
			rigidBody.gravityScale=2f;
		} 
	}

}
