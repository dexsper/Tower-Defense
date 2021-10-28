using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : BaseEnemy
{
    [SerializeField]
    private Transform mortar;

    [SerializeField]
    private Transform arm;

    [SerializeField]
    private Shell shellPrefab;

    [SerializeField]
    private float shellSpeed;

    [SerializeField]
    private float minAngle = 0f;

    [SerializeField]
    private float maxAngle = 90f;

    [SerializeField]
    [Range(1f, 5f)]
    private float shotsPerSeconds = 1f;

    public float ShotsPerSeconds => shotsPerSeconds;

    public bool canFire { get; protected set; } = true;

    protected override void InitializeComponents()
    {
        base.InitializeComponents();

        stateMachine = new StateMachine(this);

        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.RegisterState(new EnemyRunState());
        stateMachine.RegisterState(new EnemyChaseState());
        stateMachine.RegisterState(new MortarAttackState());
        stateMachine.RegisterState(new EnemyDeathState());

        stateMachine.ChangeState(initialState);

        float x = config.AttackDistance + 0.25001f;
        float y = -mortar.position.y;

        shellSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
    }
    public IEnumerator RotateArm(float startRotation, float endRotation, float duration)
    {
        float t = 0.0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            float xRotation = Mathf.Lerp(startRotation, endRotation, t / duration) % 360.0f;

            arm.localEulerAngles = new Vector3(xRotation, arm.localEulerAngles.y, arm.localEulerAngles.z);

            yield return null;
        }
    }
    public IEnumerator Fire()
    {
        canFire = false;

        float duration = 1 / shotsPerSeconds;

        StartCoroutine(RotateArm(minAngle, maxAngle, duration));

        yield return new WaitForSeconds(duration / 2);

        Shell shell = Instantiate(shellPrefab, mortar.transform.position, Quaternion.identity);
        shell.Initialize(Trajectory.CalculateTrajectory(mortar.position, target.transform.position, shellSpeed), config.Damage, target.gameObject.layer);

        yield return new WaitForSeconds(duration / 2);

        StartCoroutine(RotateArm(maxAngle, minAngle, duration));

        yield return new WaitForSeconds(duration);

        canFire = true;

        yield return null;
    }

}
