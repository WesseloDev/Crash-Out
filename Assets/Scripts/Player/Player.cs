using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    // References
    public Rigidbody rb;
    public Camera cam;
    public Animator animator;
    public Transform mesh;
    //public RagdollHandler ragdoll;

    // Movement variables
    public float speed = 10f;
    public float rotateSpeed = 2f;
    public float jumpPower = 5f;

    // Grounded check
    public Vector3 groundedCheckOffset;
    public float groundedCheckRadius;
    public float groundedCheckDistance;

    // Internal variables
    private Vector3 _input;
    private Vector3 _velocity;
    private Vector3 _direction;

    private bool _isRagdolled = false;
    private bool _isJumping = false;
    private bool _isGrounded => IsGrounded();

    private WaitForSeconds _ragdollTime;

    void Awake()
    {
        if (!rb)
            rb = GetComponent<Rigidbody>();

        if (!cam)
            cam = Camera.main ? Camera.main : FindObjectOfType<Camera>();

        if (!animator)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isRagdolled)
            return;

        MoveInput();
        JumpInput();

        // Rotates the models mesh to the move direction.
        _direction = Vector3.Slerp(_direction, _input.magnitude > 0 ? _input : _direction, rotateSpeed * Time.deltaTime);
        mesh.transform.rotation = Quaternion.LookRotation(_direction);

        // Update values for animator.
        animator.SetBool("grounded?", _isGrounded);
        animator.SetFloat("speed", _input.magnitude * speed);
    }

    void FixedUpdate()
    {
        if (_isRagdolled)
            return;

        if (_input.magnitude > 1)
            _input.Normalize();

        _input *= (speed * Time.fixedDeltaTime);

        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(_input.x, rb.velocity.y, _input.z), ref _velocity, 0.1f);
    }

    private void MoveInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 inputTransformed = cam.transform.TransformDirection(_input);
        inputTransformed.y = 0f;
        inputTransformed.Normalize();

        if (_input.sqrMagnitude != 0 && inputTransformed.sqrMagnitude == 0)
        {
            _input.y = 0f;
            _input.Normalize();
        }
        else
            _input = inputTransformed;
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
        _isJumping = true;
        animator.SetTrigger("jump");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + groundedCheckOffset, groundedCheckRadius);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position + groundedCheckOffset, groundedCheckRadius, Vector3.down, out hit, groundedCheckDistance))
        {
            if (_isJumping)
                return false;

            return true;
        }

        _isJumping = false;

        return false;
    }

    /*public void GetHit(float downTime)
    {
        if (_isRagdolled)
            return;

        _isRagdolled = true;
        ragdoll.Toggle(true);
        StartCoroutine(GetUp(downTime));
    }

    private IEnumerator GetUp(float downTime)
    {
        yield return new WaitForSeconds(downTime);

        ragdoll.Toggle(false);

        _isRagdolled = false;
    }*/
}
