using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jundroo.SimplePlanes.ModTools;
using Jundroo.SimplePlanes.ModTools.PrefabProxies;
using UnityEngine;

/// <summary>
/// A base class for assault type levels.
/// </summary>
/// <seealso cref="Jundroo.SimplePlanes.ModTools.Level" />
public abstract class AssaultLevelBase : Level
{
    /// <summary>
    /// The name of the root game object for the level so that it can be loaded from the mod.
    /// </summary>
    private string _levelGameObjectName;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssaultLevelBase"/> class.
    /// </summary>
    /// <param name="levelName">The name of the level.</param>
    /// <param name="levelDescription">The level description.</param>
    /// <param name="levelGameObjectName">The name of the root game object for the level so that it can be loaded from the mod.</param>
    public AssaultLevelBase(string levelName, string levelMap, string levelDescription, string levelGameObjectName)
       : base(levelName, levelMap, levelDescription)
    {
        this._levelGameObjectName = levelGameObjectName;
    }

    /// <summary>
    /// Gets the end level dialog message for when the player suffers critical damage.
    /// </summary>
    /// <value>
    /// The end level dialog message for when the player suffers critical damage.
    /// </value>
    protected virtual string CriticalDamageMessage
    {
        get
        {
            return "Your aircraft has been critically damaged.";
        }
    }

    /// <summary>
    /// Gets the enemy destruction monitor script.
    /// </summary>
    /// <value>
    /// The enemy destruction monitor script.
    /// </value>
    protected EnemyTargetMonitor EnemyMonitor { get; set; }
    protected ScoreMonitor ScoreMonitor { get; set; }
    protected CombatArea CombatArea { get; set; }
    protected ApplyLevelStats ApplyLevelStats { get; set; }

    /// <summary>
    /// Gets a value indicating whether or not the level has been initialized.
    /// </summary>
    /// <value>
    /// A value indicating whether or not the level has been initialized.
    /// </value>
    protected bool? Initialized { get; set; }

    /// <summary>
    /// Gets the message displayed to the player when starting the level.
    /// </summary>
    /// <value>
    /// The message displayed to the player when starting the level.
    /// </value>
    protected virtual string StartMessage
    {
        get
        {
            return null;
        }
    }

    /// <summary>
    /// Gets the success message for the end level dialog when the player destroys all targets.
    /// </summary>
    /// <value>
    /// The success message for the end level dialog when the player destroys all targets.
    /// </value>
    protected virtual string SuccessMessage
    {
        get
        {
            return "Mission Accomplished";
        }
    }

    protected virtual string LeftCombatAreaMessage
    {
        get
        {
            return "You left combat airspace.";
        }
    }

    protected virtual string SpecialFailMessage
    {
        get
        {
            return "Mission Failed";
        }
    }

    protected override bool StartTimerWithThrottle
    {
        get
        {
            return false;
        }
    }

    protected virtual float TimeOfDay
    {
        get
        {
            return 12f;
        }
    }

    protected virtual WeatherPreset Weather
    {
        get
        {
            return WeatherPreset.Clear;
        }
    }

    protected bool LevelWon = false;

    protected override string FormatScore(float score)
    {
        return LevelWon ? string.Format("{0}", score) : string.Format("{0}/{1} destroyed", score, EnemyMonitor.TotalTgt);
    }

    /// <summary>
    /// Attempts to initialize the level.
    /// </summary>
    protected virtual void Initialize()
    {
        // Only try to initialize if we have not already failed.
        if (!this.Initialized.HasValue)
        {
            try
            {
                var obj = ServiceProvider.Instance.ResourceLoader.LoadGameObject(this._levelGameObjectName);
                if (obj == null)
                {
                    Debug.LogErrorFormat("Unable to instantiate game object: {0}", this._levelGameObjectName);
                    this.Initialized = false;
                    return;
                }

                this.EnemyMonitor = obj.GetComponent<EnemyTargetMonitor>();
                if (this.EnemyMonitor == null)
                {
                    Debug.LogErrorFormat("Unable to get the enemy destruction monitor script from the level game object '{0}'", this._levelGameObjectName);
                    this.Initialized = false;
                    return;
                }

                this.ScoreMonitor = obj.GetComponent<ScoreMonitor>();
                if (this.ScoreMonitor == null)
                {
                    Debug.LogErrorFormat("Unable to get the score monitor script from the level game object '{0}'", this._levelGameObjectName);
                    this.Initialized = false;
                    return;
                }

                this.CombatArea = obj.GetComponent<CombatArea>();
                if (this.CombatArea == null)
                {
                    Debug.LogErrorFormat("Unable to get the combat area monitor script from the level game object '{0}'", this._levelGameObjectName);
                    this.Initialized = false;
                    return;
                }

                this.ApplyLevelStats = obj.GetComponent<ApplyLevelStats>();
                if (this.ApplyLevelStats == null)
                {
                    Debug.LogErrorFormat("Unable to get the level stats handler script from the level game object '{0}'", this._levelGameObjectName);
                    this.Initialized = false;
                    return;
                }

                ServiceProvider.Instance.EnvironmentManager.UpdateTimeOfDay(TimeOfDay, 0, false, true);
                ServiceProvider.Instance.EnvironmentManager.UpdateWeather(Weather, 0, true);

                if (!string.IsNullOrEmpty(this.StartMessage))
                {
                    ServiceProvider.Instance.GameWorld.ShowStatusMessage(this.StartMessage);
                }

                this.Initialized = true;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                this.Initialized = false;
            }
        }
    }

    /// <summary>
    /// Update is called every frame.
    /// </summary>
    protected override void Update()
    {
        base.Update();

        // Initialize the level if not yet initialized.
        // If initialization failed, skip the update.
        if (!(this.Initialized ?? false))
        {
            this.Initialize();
            return;
        }

        // Check for win/lose conditions and end the level if needed.
        if (EnemyMonitor.AllTgtDestroyed)
        {
            LevelWon = true;
            ApplyLevelStats.LevelState = ApplyLevelStats.LevelStates.Won;
            this.EndLevel(true, this.SuccessMessage, ScoreMonitor.DestScore + ScoreMonitor.TimeScore);
        }
        else if (ServiceProvider.Instance.PlayerAircraft.CriticallyDamaged)
        {
            ApplyLevelStats.LevelState = ApplyLevelStats.LevelStates.Lost;
            this.EndLevel(false, this.CriticalDamageMessage, EnemyMonitor.DestroyedTgt);
        }
        else if (CombatArea.LeftArea)
        {
            ApplyLevelStats.LevelState = ApplyLevelStats.LevelStates.Lost;
            this.EndLevel(false, this.LeftCombatAreaMessage, EnemyMonitor.DestroyedTgt);
        }
        else if (EnemyMonitor.SpecialFail)
        {
            ApplyLevelStats.LevelState = ApplyLevelStats.LevelStates.Lost;
            this.EndLevel(false, this.SpecialFailMessage, EnemyMonitor.DestroyedTgt);
        }
    }
}
