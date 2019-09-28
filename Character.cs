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

    protected Animator animator;

    //The Player's direction
    protected Vector2 direction;

    private Rigidbody2D myRigidBody;

    protected Coroutine attackRoutine;

    protected bool isAttacking = false;

    public bool IsMoving
    {
        get
        {
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Use this for initialization
    protected virtual void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is marked as virtual, so that we can override it in the subclasses
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Moves the player
    public void Move()
    {
        //Makes sure that the player moves
        //transform.Translate(direction * speed * Time.deltaTime); <--- old way of moving, inefficient
        myRigidBody.velocity = direction.normalized  * speed;

    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("Walk_Layer");
            //Sets the animation parameter so that he faces the correct direction
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

            StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("Attack_Layer");
        }
        else
        {
            ActivateLayer("Idle_Layer");
        }
    }

    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }

        animator.SetLayerWeight(animator.GetLayerIndex(layerName), 1);
        
    }

    public void StopAttack()
    {
        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);

            isAttacking = false;

            animator.SetBool("attack", isAttacking);
        }

    }
}
