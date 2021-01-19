using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m4_BaseWaterLevel : MonoBehaviour
{
    private bool WaterLevel = false;
    private bool PreviousWaterLevel = false;

    private bool PlayerInside = false;
    private bool PreviousPlayerInside = false;

    public float TriggerDistance = 35f;

    private float CooldownMax = 10f;
    private float CooldownTimer = 10f;

    private void FixedUpdate()
    {
        CooldownTimer += Time.deltaTime;

        PreviousPlayerInside = PlayerInside;

        if ((gameObject.transform.position - ServiceProvider.Instance.PlayerAircraft.MainCockpitPosition).magnitude < TriggerDistance)
        {
            PlayerInside = true;
        }
        else
        {
            PlayerInside = false;
        }

        if (PlayerInside && !PreviousPlayerInside && CooldownTimer > CooldownMax)    // Detect rising edge
        {
            WaterLevel = !WaterLevel;

            if (WaterLevel != PreviousWaterLevel)
            {
                if (WaterLevel)
                {
                    ServiceProvider.Instance.GameWorld.SeaLevel = null;
                    TriggerDistance = 200f;
                    CooldownTimer = 0f;
                }
                else
                {
                    ServiceProvider.Instance.GameWorld.SeaLevel = 0f;
                    TriggerDistance = 40f;
                    CooldownTimer = 0f;
                }

                PreviousWaterLevel = WaterLevel;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, TriggerDistance);
    }
}
