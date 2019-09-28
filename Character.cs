using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <summary>
// This is an abstract class that all characters needs to inherit from
// </summary>
public abstract class Character : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Animator animator;

    //The Player's direction
    protected Vector2 direction;

    // Use this for initialization
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is marked as virtual, so that we can override it in the subclasses
    protected virtual void Update()
    {
        Move();
    }

    // Moves the player
    public void Move()
    {
        //Makes sure that the player moves
        transform.Translate(direction * speed * Time.deltaTime);

        if (direction.x != 0 || direction.y != 0)
        {
            //Animate's the Player's movement
            AnimateMovement(direction);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }


    }

    // Makes the player animate in the correct direction
    /// <param name="direction"></param>
    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        //Sets the animation parameter so that he faces the correct direction
        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }
}
