using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
	private Vector3 launchPoint, targetPoint, launchVelocity;

	private bool initialized;

	private float damage;

	private int layer;

	public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity, float damage, int layer)
	{
		this.launchPoint = launchPoint;
		this.targetPoint = targetPoint;
		this.launchVelocity = launchVelocity;
		this.layer = layer;
		this.damage = damage;

		initialized = true;
	}

	float age;

	private void Update()
	{
		if (initialized)
		{
			age += Time.deltaTime;
			Vector3 p = launchPoint + launchVelocity * age;
			p.y -= 0.5f * 9.81f * age * age;

			Vector3 d = launchVelocity;
			d.y -= 9.81f * age;
			transform.localRotation = Quaternion.LookRotation(d);

			transform.localPosition = p;
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == layer)
		{
			other.GetComponent<Entity>().Health.Damage(damage);
		}

		Destroy(gameObject);
	}
}
