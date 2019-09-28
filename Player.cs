using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the player script, it contains functionality that is specific to the Player

public class Player : Character
{
    [SerializeField]
    private Stat health;
    [SerializeField]
    private Stat mana;

    private float initHealth = 100;
    private float initMana = 50;
    // overriding the characters update function, so that we can execute our own functions
    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);

        base.Start();
    }
    protected override void Update()
    {
        //Executes the GetInput function
        GetInput();

        base.Update();
    }



    // Listen's to the players input
    private void GetInput()
    {
        direction = Vector2.zero;

        //health bar debugging
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }
        //end of health bar debugging 


        if (Input.GetKey(KeyCode.W)) //Moves up
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A)) //Moves left
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S)) //moves down
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) //Moves right
        {
            direction += Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isAttacking && !IsMoving)
            {
                attackRoutine = StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        //if (!isAttacking && !IsMoving)
        //{
            isAttacking = true;

            animator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(1); //hard coded cast time

            StopAttack();

        //}

        //Debug.Log("Done Casting");
    }
}
