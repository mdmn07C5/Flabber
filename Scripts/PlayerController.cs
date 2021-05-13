using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Vector2 initial_position;
    Rigidbody2D rigidbody2D;
    Animator animator;
    [SerializeField] float jumpPower = 1.5f;
    [SerializeField] float rotationSpeed = 4f;
    [SerializeField] float fallAngle = -90f;
    [SerializeField] float flapAngle = 45;
    void Awake()
    {
        initial_position = transform.position;
        GameManager.OnJumpKeyPressed.AddListener(HandleOnJumpKeyPressed);
    }

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SetAnimation();
        SetTilt();
    }

    void OnDisable()
    {
        transform.position = initial_position;
    }

    void HandleOnJumpKeyPressed()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
    }

    void SetAnimation()
    {
        animator.SetFloat("Y_Velocity", rigidbody2D.velocity.y);
    }

    void SetTilt()
    {
        if (rigidbody2D.velocity.y <= 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.Euler(0, 0, fallAngle),
                Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, flapAngle);
        }
    }

}
