using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
	Vector3 launchPoint, targetPoint, launchVelocity;

	bool initialized;

	public void Initialize(Vector3 launchPoint, Vector3 targetPoint, Vector3 launchVelocity)
	{
		this.launchPoint = launchPoint;
		this.targetPoint = targetPoint;
		this.launchVelocity = launchVelocity;

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
}
