using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    /// <summary>
    /// A canvasgroup for the healthbar
    /// </summary>
    [SerializeField]
    private CanvasGroup healthGroup;

    /// <summary>
    /// When the enemy is selected
    /// </summary>
    /// <returns></returns>
    public override Transform Select()
    {
        //Shows the health bar
        healthGroup.alpha = 1;

        return base.Select();
    }

    /// <summary>
    /// When we deselect our enemy
    /// </summary>
    public override void DeSelect()
    {
        //Hides the healthbar
        healthGroup.alpha = 0;

        base.DeSelect();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        OnHealthChanged(health.MyCurrentValue);
    }
}
