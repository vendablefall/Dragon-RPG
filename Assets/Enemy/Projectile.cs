using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float projecttileSpeed;

	float damageCaused;

	public void SetDamage (float damage)
	{
		damageCaused = damage;
	}
	void OnTriggerEnter (Collider collider)
	{
		//print ("proj hit " + collider.gameObject);

		Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamagable));
		if (damageableComponent)
		{
			(damageableComponent as IDamagable).TakeDamage(damageCaused);
		}
	}
}
