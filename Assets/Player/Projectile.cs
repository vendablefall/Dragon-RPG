using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	[SerializeField] float damageCaused = 10f;
	void OnTriggerEnter (Collider collider)
	{
		print ("proj hit " + collider.gameObject);

		Component damageableComponent = collider.gameObject.GetComponent(typeof(IDamagable));
		if (damageableComponent)
		{
			(damageableComponent as IDamagable).TakeDamage(damageCaused);
		}
	}
}
