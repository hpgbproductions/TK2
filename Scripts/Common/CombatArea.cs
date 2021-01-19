using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatArea : MonoBehaviour
{
    [SerializeField]
    private Vector3 LimitStart;

    [SerializeField]
    private Vector3 LimitEnd;

    [SerializeField]
    private Vector3 WarningStart;

    [SerializeField]
    private Vector3 WarningEnd;

    [SerializeField]
    private float CountdownLength = 20f;

    private Vector3 _LimitStart;
    private Vector3 _LimitEnd;
    private Vector3 _WarningStart;
    private Vector3 _WarningEnd;

    private Vector3 OriginOffset;
    private Vector3 PlayerPosition;

    private float Countdown;

    public bool LeftArea = false;

    private void Start()
    {
        Countdown = CountdownLength;
    }

    private void Update()
    {
        OriginOffset = ServiceProvider.Instance.GameWorld.FloatingOriginOffset;
        PlayerPosition = ServiceProvider.Instance.PlayerAircraft.MainCockpitPosition;

        // Calculate bounding box with floating origin offset
        _LimitStart = LimitStart - OriginOffset;
        _LimitEnd = LimitEnd - OriginOffset;
        _WarningStart = WarningStart - OriginOffset;
        _WarningEnd = WarningEnd - OriginOffset;

        if (!LeftArea)
        {
            // Outside combat area
            if (PlayerPosition.x < _LimitStart.x | PlayerPosition.x > _LimitEnd.x |
                PlayerPosition.y < _LimitStart.y | PlayerPosition.y > _LimitEnd.y |
                PlayerPosition.z < _LimitStart.z | PlayerPosition.z > _LimitEnd.z)
            {
                Countdown -= Time.deltaTime;
                ServiceProvider.Instance.GameWorld.ShowStatusMessage(string.Format("Return to the battlefield! ({0:F1})", Countdown));
            }
            // Outside warning area
            else if (PlayerPosition.x < _WarningStart.x | PlayerPosition.x > _WarningEnd.x |
                PlayerPosition.y < _WarningStart.y | PlayerPosition.y > _WarningEnd.y |
                PlayerPosition.z < _WarningStart.z | PlayerPosition.z > _WarningEnd.z)
            {
                ServiceProvider.Instance.GameWorld.ShowStatusMessage("Return to the battlefield!");
            }
            // In combat area
            else
            {
                Countdown = CountdownLength;
            }

            if (Countdown <= 0f)
            {
                LeftArea = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((LimitStart + LimitEnd) / 2, LimitEnd - LimitStart);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((WarningStart + WarningEnd) / 2, WarningEnd - WarningStart);
    }
}
