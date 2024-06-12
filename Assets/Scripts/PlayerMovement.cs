using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        //rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        Move();
        UpdateAnimator();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ).normalized * moveSpeed;

        // 이동 방향이 있을 때만 캐릭터의 방향을 변경
        if (moveDirection.sqrMagnitude > 0)
        {
            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }

    void UpdateAnimator()
    {
        bool isMoving = moveDirection != Vector3.zero;
        animator.SetBool("IsMoving", isMoving);
    }
}
