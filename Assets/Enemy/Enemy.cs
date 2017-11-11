using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour {

	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 4f;

	float currentHealthPoints = 100f;

	float distanceToPlayer;
	AICharacterControl aiCharicterControl = null;

	GameObject player;

	public float healthAsPercentage
	{
		get
		{
			return currentHealthPoints / (float)maxHealthPoints;
		}
	}

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiCharicterControl = GetComponent<AICharacterControl>();
	}

	void Update()
	{
		distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
		//Debug.Log(distanceToPlayer);

		if (distanceToPlayer <= attackRadius)
		{
			aiCharicterControl.SetTarget(player.transform);
		}
		else 
		{
			aiCharicterControl.SetTarget(transform);
		}
	}

}
