using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m4_PlayerTracker : MonoBehaviour
{
    private void FixedUpdate()
    {
        gameObject.transform.position = ServiceProvider.Instance.PlayerAircraft.MainCockpitPosition;
    }
}
