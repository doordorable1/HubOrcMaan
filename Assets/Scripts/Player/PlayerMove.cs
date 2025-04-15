using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public PlayerData playerData;
    [Header("Physics")]
    [SerializeField] protected Rigidbody playerRigidbody;

    [Header("Key Bindings")]
    public KeyCode moveUp;
    public KeyCode moveDown;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode dashKey;

    protected Vector3 _inputDirection;
    protected bool _isDashing = false;
    protected float _dashTime = 0f;
    protected float _lastDashTime = -Mathf.Infinity;

    protected virtual void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;
        if (Input.GetKey(moveUp)) verticalInput += 1f;
        if (Input.GetKey(moveDown)) verticalInput -= 1f;
        if (Input.GetKey(moveLeft)) horizontalInput -= 1f;
        if (Input.GetKey(moveRight)) horizontalInput += 1f;

        _inputDirection = new Vector3(horizontalInput, 0, verticalInput);
        
        // Adjust dash cooldown if Fever Time is active
        float currentDashCooldown = playerData.dashCooldown;
        if (GameManager.Instance != null && GameManager.Instance.isFeverTime)
        {
            currentDashCooldown *= GameManager.Instance.feverDashCooldownMultiplier;
        }

        if (Input.GetKeyDown(dashKey) && Time.time >= _lastDashTime + currentDashCooldown)
        {
            StartCoroutine(Dash());
        }
    }

    protected virtual void FixedUpdate()
    {
        if (!_isDashing)
        {
            MoveThePlayer();
        }
        TurnThePlayer();
    }

    protected void MoveThePlayer()
    {
        // Apply movement speed multiplier during Fever Time
        float finalSpeed = playerData.movementSpeed;
        if (GameManager.Instance != null && GameManager.Instance.isFeverTime)
        {
            finalSpeed *= GameManager.Instance.feverMoveSpeedMultiplier;
        }
        playerRigidbody.linearVelocity = _inputDirection.normalized * finalSpeed;
    }

    protected void TurnThePlayer()
    {
        if (playerRigidbody.linearVelocity.magnitude > 0.1f && _inputDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(_inputDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 15f);
        }
    }

    protected IEnumerator Dash()
    {
        _isDashing = true;
        _dashTime = 0f;
        _lastDashTime = Time.time;

        // Adjust dash speed if needed (optional)
        float finalDashSpeed = playerData.dashSpeed;

        while (_dashTime < playerData.dashDuration)
        {
            playerRigidbody.linearVelocity = _inputDirection.normalized * finalDashSpeed;
            _dashTime += Time.deltaTime;
            yield return null;
        }

        _isDashing = false;
    }
}
