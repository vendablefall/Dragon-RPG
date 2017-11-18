using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable {
	[SerializeField] float maxHealthPoints = 100f;
	[SerializeField] float damagePerHit = 15f;
	[SerializeField] float maxAttackRange = 2f;
	[SerializeField] float minTimeBetweenHits = 0.5f;
	[SerializeField] int enemyLayer = 9;
	GameObject currentTarget;
	float currentHealthPoints;
	CameraRaycaster cameraRaycaster;
	float lastHitTIme = 0f;
	public float healthAsPercentage
	{ get { return currentHealthPoints / (float)maxHealthPoints; } }

	void Start()
	{
		cameraRaycaster = FindObjectOfType<CameraRaycaster>();
		cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
		currentHealthPoints = maxHealthPoints;
	}

	void OnMouseClick (RaycastHit raycastHit, int layerHit)
	{
		if (layerHit == enemyLayer)
		{
			var enemy = raycastHit.collider.gameObject;
			//print("Clicked enemy: " + enemy);
			if ((enemy.transform.position - transform.position).magnitude > maxAttackRange)
			{
				return;
			}

			currentTarget = enemy;
			var enemyComponent = enemy.GetComponent<Enemy>();
			if (Time.time - lastHitTIme > minTimeBetweenHits)
			{
				enemyComponent.TakeDamage(damagePerHit);
				lastHitTIme = Time.time;
			}
			
		}
		
	}

	public void TakeDamage(float damage)
	{
		currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
		//if (currentHealthPoints <= 0) {Destroy(gameObject);};
	}

}
