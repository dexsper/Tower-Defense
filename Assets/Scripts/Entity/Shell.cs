using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
	private Trajectory trajectory;

	private bool initialized;

	private float damage;

	private int layer;

	public void Initialize(Trajectory trajectory, float damage, int layer)
	{
		this.trajectory = trajectory;
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
			Vector3 p = trajectory.LaunchPoint + trajectory.Velocity * age;
			p.y -= 0.5f * 9.81f * age * age;

			Vector3 d = trajectory.Velocity;
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
