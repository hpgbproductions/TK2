/*
 *  Updates stats when a level is started or completed.
 *  Goes on level object.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyLevelStats : MonoBehaviour
{
    public enum LevelStates { Active, Lost, Won }
    public LevelStates LevelState = LevelStates.Active;

    private bool StatsApplied = false;

    private StatHolder StatHolder;
    private ScoreMonitor ScoreMonitor;

    [SerializeField]
    private int LevelId;

    private void Start()
    {
        StatHolder = FindObjectOfType<StatHolder>();
        ScoreMonitor = gameObject.GetComponent<ScoreMonitor>();

        StatHolder.AttemptCount[LevelId]++;
        StatHolder.UpdateWinRate(LevelId);
    }

    private void FixedUpdate()
    {
        // Save stats if the level is won and stats have not been saved yet
        if (LevelState == LevelStates.Won && !StatsApplied)
        {
            StatHolder.TotalRecords[LevelId] = Math.Max(ScoreMonitor.DestScore + ScoreMonitor.TimeScore, StatHolder.TotalRecords[LevelId]);
            StatHolder.DestRecords[LevelId] = Math.Max(ScoreMonitor.DestScore, StatHolder.DestRecords[LevelId]);

            StatHolder.TimeRecords[LevelId] = Mathf.Min(Time.timeSinceLevelLoad, StatHolder.TimeRecords[LevelId]);

            StatHolder.WinCount[LevelId]++;
            StatHolder.UpdateWinRate(LevelId);

            StatsApplied = true;
        }
    }
}
