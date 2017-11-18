using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour , IDamagable {
	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 2f;
	[SerializeField] float sightRadius = 4f;
	[SerializeField] float chaseStopRadius = 6f;
	float currentHealthPoints = 100f;
	float distanceToPlayer;
	AICharacterControl aiCharicterControl = null;
	GameObject player;
	public float healthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }
	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
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

		if (aiCharicterControl.target == player.transform && distanceToPlayer >= chaseStopRadius)
		{
			aiCharicterControl.SetTarget(transform);
		}


		if (distanceToPlayer <= sightRadius)
		{
			aiCharicterControl.SetTarget(player.transform);
		}
		// else 
		// {
		// 	aiCharicterControl.SetTarget(transform);
		// }

		if (distanceToPlayer <= attackRadius)
		{
			print("enemy will attack, once you imlpement attacking.......");
			//aiCharicterControl.SetTarget(player.transform);
			// TODO spawn projectile
		}

	}

	void OnDrawGizmos()
	{

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRadius);

		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, sightRadius);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, chaseStopRadius);


	}


}
