using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMarkers : MonoBehaviour
{
    [SerializeField]
    private ShipProxy[] AlliedShipProxies;

    // AceRadar - place AceRadarBackend Component on the same GameObject!
    private AceRadarBackend Backend;
    private int InitWaitFrames = 50;
    private int InitCountFrames = 0;

    private void Start()
    {
        // AceRadar
        Backend = gameObject.GetComponent<AceRadarBackend>();
        if (Backend.Initialized)
        {
            // Get the initialization frame delay
            InitWaitFrames = Backend.GetCheckInterval();
        }
    }

    private void Update()
    {
        // BEGIN (Set the blip style of targets, after InitWaitFrames frames)

        if (InitCountFrames == InitWaitFrames && Backend.Initialized)
        {
            foreach (ShipProxy shipProxy in AlliedShipProxies)
            {
                Backend.FindAndModifyTargetBlip(shipProxy, AceRadarBackend.AceRadarSprites.Ground, AceRadarBackend.AceRadarColors.Blue);
            }
        }

        InitCountFrames++;

        // END (Set the blip style of targets, after InitWaitFrames frames)
    }
}
