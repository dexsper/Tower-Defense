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
    private float startSpeed;

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

        startSpeed = Mathf.Sqrt(9.81f * (y + Mathf.Sqrt(x * x + y * y)));
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
        shell.Initialize(mortar.position, target.transform.position, CalculateTrajectory(mortar.position, target.transform.position));

        yield return new WaitForSeconds(duration / 2);

        StartCoroutine(RotateArm(maxAngle, minAngle, duration));

        yield return new WaitForSeconds(duration);

        canFire = true;

        yield return null;
    }

    private Vector3 CalculateTrajectory(Vector3 launchPoint, Vector3 targetPoint)
    {
        targetPoint.y = 0f;

        Vector2 dir;
        dir.x = targetPoint.x - launchPoint.x;
        dir.y = targetPoint.z - launchPoint.z;

        float x = dir.magnitude;
        float y = -launchPoint.y;
        dir /= x;

        float g = 9.81f;
        float s = startSpeed;
        float s2 = s * s;

        float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
        float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
        float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
        float sinTheta = cosTheta * tanTheta;

        Vector3 prev = launchPoint, next;
        for (int i = 1; i <= 10; i++)
        {
            float t = i / 10f;
            float dx = s * cosTheta * t;
            float dy = s * sinTheta * t - 0.5f * g * t * t;
            next = launchPoint + new Vector3(dir.x * dx, dy, dir.y * dx);
            Debug.DrawLine(prev, next, Color.blue);
            prev = next;
        }

        Debug.DrawLine(launchPoint, targetPoint, Color.yellow);

        Debug.DrawLine(
            new Vector3(launchPoint.x, 0.01f, launchPoint.z),
            new Vector3(
                launchPoint.x + dir.x * x, 0.01f, launchPoint.z + dir.y * x
            ),
            Color.white
        );

        return new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y);

    }
}
