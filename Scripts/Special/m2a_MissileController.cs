using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m2a_MissileController : MonoBehaviour
{
    [SerializeField]
    private m2a_MissileMovement[] Missiles;

    [SerializeField]
    private float[] LaunchAtTime;

    [SerializeField]
    private SpecialFailSetting SpecialFail;

    private bool LaunchProcessStarted = false;
    private float LaunchTimer = 0f;

    private bool LaunchWarning = false;

    private bool AllMissilesLaunched;

    private float FailZ = -50000f;

    private void Update()
    {
        if (!LaunchProcessStarted)
        {
            if ((ServiceProvider.Instance.PlayerAircraft.MainCockpitPosition - gameObject.transform.position).magnitude < 16000)
            {
                ServiceProvider.Instance.GameWorld.ShowStatusMessage("Detecting heat signature spikes.");
                LaunchProcessStarted = true;
            }
        }
        else
        {
            if (!AllMissilesLaunched)
            {
                int CountMissilesLaunched = 0;

                LaunchTimer += Time.deltaTime;

                for (int i = 0; i < Missiles.Length; i++)
                {
                    if (LaunchTimer > LaunchAtTime[i])
                    {
                        Missiles[i].MissileActive = true;
                        CountMissilesLaunched++;
                    }
                }

                if (CountMissilesLaunched >= 1 && !LaunchWarning)
                {
                    ServiceProvider.Instance.GameWorld.ShowStatusMessage("Detecting launch of anti-ship missiles! Intercept them now!");
                    LaunchWarning = true;
                }

                if (CountMissilesLaunched == Missiles.Length)
                {
                    AllMissilesLaunched = true;
                }
            }

            for (int i = 0; i < Missiles.Length; i++)
            {
                if ((Missiles[i].gameObject.transform.position.z + ServiceProvider.Instance.GameWorld.FloatingOriginOffset.z) < FailZ)
                {
                    SpecialFail.SpecialFail = true;
                }
            }
        }
    }
}
