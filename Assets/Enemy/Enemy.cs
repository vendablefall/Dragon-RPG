using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IDamagable {
	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float attackRadius = 2f;
	[SerializeField] float sightRadius = 4f;
	[SerializeField] float chaseStopRadius = 6f;
	[SerializeField] float damagePerShot = 9f;
	[SerializeField] float secondsBetweenShots = 0.5f;
	[SerializeField] GameObject projectileToUse;
	[SerializeField] GameObject projectileSocket;
	GameObject shot = null;
	bool isAttacking = false;
	float currentHealthPoints = 100f;
	float distanceToPlayer;
	AICharacterControl aiCharicterControl = null;
	NavMeshAgent navMeshAgent = null;
	GameObject player;
	public float healthAsPercentage { get { return currentHealthPoints / (float)maxHealthPoints; } }
	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
		if (currentHealthPoints <= 0) {Destroy(gameObject);};
	}
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		aiCharicterControl = GetComponent<AICharacterControl>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.stoppingDistance = attackRadius;
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

		if (distanceToPlayer <= attackRadius && !isAttacking)
		{
			//print("enemy will attack, once you imlpement attacking.......");
			// TODO slow this projectile down 
			isAttacking = true;
			InvokeRepeating("SpawnProjectile", 0f , secondsBetweenShots); // TODO swicth to co-routines
			//SpawnProjectile();
		}

		if (distanceToPlayer >= attackRadius && isAttacking)
		{
			isAttacking = false;
			CancelInvoke("SpawnProjectile");
		}

	}

	void SpawnProjectile()
	{
		GameObject newProjectile = Instantiate(projectileToUse, projectileSocket.transform.position,Quaternion.identity);
		Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
		projectileComponent.SetDamage(damagePerShot) ;// damageCaused = damagePerShot;
		Vector3 unitVectorToPlayer = (player.transform.position - projectileSocket.transform.position).normalized;
		newProjectile.GetComponent<Rigidbody>().velocity = unitVectorToPlayer * projectileComponent.projecttileSpeed;
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
