using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m2a_MissileMovement : MonoBehaviour
{
    [SerializeField]
    private RotatingMissileLauncherProxy TargetObject;

    [SerializeField]
    private ParticleSystem MissileParticles;

    public bool MissileActive = false;

    private bool MissileLaunched = false;

    private float Speed = 0f;

    private float MaxAccel = 50f;

    private float StartSpeed = 150f;
    private float MaxSpeed = 400f;

    private float HorizontalAngularSpeed = 20f;
    private float VerticalAngularSpeed = 10f;

    private float TargetHeading = 190f;

    // Update is called once per frame
    void Update()
    {
        if (!MissileActive)
        {
            return;
        }

        if (!MissileLaunched)
        {
            Speed = StartSpeed;
            MissileParticles.Play();
            MissileLaunched = true;
            return;
        }

        if (TargetObject.IsDisabled)
        {
            gameObject.SetActive(false);
            return;
        }

        Speed = Mathf.Min(Speed + MaxAccel * Time.deltaTime, MaxSpeed);
        gameObject.transform.Translate(new Vector3(0f, 0f, Speed * Time.deltaTime));

        gameObject.transform.eulerAngles = new Vector3
            (
            Mathf.MoveTowardsAngle(gameObject.transform.eulerAngles.x, 0f, VerticalAngularSpeed),
            Mathf.MoveTowardsAngle(gameObject.transform.eulerAngles.y, TargetHeading, HorizontalAngularSpeed),
            0f
            );
    }
}
