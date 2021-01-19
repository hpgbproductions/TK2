/*
 *  Deprecated: Integrated into ScoreMonitor
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    // Values to take
    private int score;
    private int bonus;

    [SerializeField]
    private GUISkin Skin;

    [SerializeField]
    private EnemyTargetMonitor etm;

    [SerializeField]
    private ScoreMonitor sm;

    private GUIStyle ScoreStyle;
    private GUIStyle BonusStyle;
    private GUIStyle BgStyle;

    private void Start()
    {
        BgStyle = Skin.box;
        ScoreStyle = Skin.label;
        BonusStyle = Skin.customStyles[0];
    }

    private void Update()
    {
        if (!etm.AllTgtDestroyed)
        {
            score = sm.DestScore;
            bonus = sm.TimeScore;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 250, 0, 250, 100), "", BgStyle);

        GUI.Label(new Rect(Screen.width - 130, 15, 110, 50), string.Format("{0}", score), ScoreStyle);

        GUI.Label(new Rect(Screen.width - 130, 60, 110, 30), string.Format("BONUS: {0}", bonus), BonusStyle);
    }
}
