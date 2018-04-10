using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript: Scripts
{
    /// <summary>
    /// Allow anything that had this script to move
    /// From the input manager or from another source
    /// </summary>


    public enum Direction
    {
        right = 1,
        left = -1
    }
    public enum Movement
    {
        moving,
        stationary
    }
    Direction _currentDirection = Direction.right;
    Movement _movementState = Movement.stationary;

    bool isOnPlateform;
    bool isGrounded = true;
    bool canDoubleJump = true;


    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;

    public Direction GetDirection()
    {
        return _currentDirection;
    }
    public void SetPlayerDirection(Direction dir)
    {
        _currentDirection = dir;
        this.transform.localScale = new Vector3((float)dir*1.5f, 1.5f, 1.5f);
    }

    public void Move()
    {
        this.transform.Translate(new Vector3((float) _currentDirection, 0) * player.speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded || isOnPlateform)
        {
            ApplyJump();
        }
        else if(canDoubleJump)
        {
            ApplyJump();
            canDoubleJump = false;
        }
    }

    void ApplyJump()
    {
        rigidbody.velocity = new Vector2(0, 0);
        rigidbody.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
    }

    public void Fall()
    {
        if (isOnPlateform)
        {
            boxCollider.isTrigger = true;
        }
        else
        {
            boxCollider.isTrigger = false;
        }
    }

    public void SetMovementAndDirectionState(float horizontal)
    {
        if (horizontal > 0)
        {
            SetPlayerDirection(Direction.right);
            _movementState = Movement.moving;

        }
        else if(horizontal < 0)
        {
            SetPlayerDirection(Direction.left);
            _movementState = Movement.moving;

        }
        else
        {
            _movementState = Movement.stationary;
        }
    }

    private void setOnPlateformSetup()
    {
        isGrounded = false;
        canDoubleJump = true;
        isOnPlateform = true;
    }

    private void setOnGroundSetup()
    {
        isGrounded = true;
        canDoubleJump = true;
        isOnPlateform = false;
    }

    public override void Start()
    {
        base.Start();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    public void FixedUpdate()
    {
        if (_movementState == Movement.moving)
        {
            Move();
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == Constants.GROUND_TAG)
        {
            setOnGroundSetup();
        }
        if (collision.gameObject.tag == Constants.PLATEFORM_TAG || collision.gameObject.tag == Constants.MOVING_PLATEFORM_TAG)
        {
            setOnPlateformSetup();
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Constants.GROUND_TAG)
        {
            isGrounded = false;
        }

        if (collision.gameObject.tag == Constants.PLATEFORM_TAG || collision.gameObject.tag == Constants.MOVING_PLATEFORM_TAG)
        {
            isOnPlateform = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Constants.PLATEFORM_TAG)
        {
            boxCollider.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Constants.GROUND_TAG)
        {
            boxCollider.isTrigger = false;
        }
    }
}
