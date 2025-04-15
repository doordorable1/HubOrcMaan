using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    public PlayerData playerData;
    protected GameObject _oxygenObject;
    protected bool _isOxygenPickedUp;
    protected PlayerPick _otherPlayerPick;
    protected Color _originalColor;

    [Header("Pickable Settings")]
    public Color pickableColor = Color.white;
    public float detectionRange = 2f;
    public LayerMask oxygenLayer;

    [Header("Input")]
    public KeyCode pickKey;

    protected virtual void Start()
    {
        _otherPlayerPick = FindOtherPlayerPick();
    }

    protected virtual void Update()
    {
        DetectPickable();
        if (Input.GetKeyDown(pickKey))
        {
            if (_oxygenObject != null && !_isOxygenPickedUp)
            {
                PickUpPickable();
                _isOxygenPickedUp = true;
            }
            else if (_isOxygenPickedUp)
            {
                DropPickable();
                GameManager.Instance.RemoveBox();
                _isOxygenPickedUp = false;
            }
        }
    }

    protected virtual void DetectPickable()
    {
        if (_isOxygenPickedUp)
            return;

        RaycastHit hit;
        // 플레이어의 전방을 기준으로 약간의 각도를 적용하여 탐지
        Vector3 direction = Quaternion.Euler(10, transform.eulerAngles.y, 0) * Vector3.forward;

        // 기존에 탐지된 객체가 있으면 원래 색상으로 복원
        if (_oxygenObject != null && !_isOxygenPickedUp)
        {
            Renderer rend = _oxygenObject.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = _originalColor;
        }

        // Raycast로 새 객체 탐지
        if (Physics.Raycast(transform.position, direction, out hit, detectionRange, oxygenLayer))
        {
            if (hit.collider.CompareTag("Pickable") && !_isOxygenPickedUp)
            {
                GameObject detectedObject = hit.collider.gameObject;
                if (_oxygenObject != detectedObject)
                {
                    _oxygenObject = detectedObject;
                    Renderer rend = _oxygenObject.GetComponent<Renderer>();
                    if (rend != null)
                        _originalColor = rend.material.color;
                }
                Renderer detectedRend = _oxygenObject.GetComponent<Renderer>();
                if (detectedRend != null)
                    detectedRend.material.color = pickableColor;
                Debug.Log("Pickable detected");
            }
        }
        else
        {
            _oxygenObject = null;
        }
    }

    protected virtual void PickUpPickable()
    {
        Debug.Log("Picked up Pickable");
        _oxygenObject.transform.SetParent(transform);
        _oxygenObject.transform.localPosition = new Vector3(0f, 1.5f, 0f);

        // 물리 계산 중단을 위해 Rigidbody를 kinematic으로 전환
        Rigidbody rb = _oxygenObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // 픽업 시 원래 색상으로 복원
        Renderer rend = _oxygenObject.GetComponent<Renderer>();
        if (rend != null)
            rend.material.color = _originalColor;
    }

    protected virtual void DropPickable()
    {
        GameObject objectToDrop = _oxygenObject;
        objectToDrop.transform.SetParent(null);
        Vector3 dropPosition = transform.position + transform.forward;
        objectToDrop.transform.position = new Vector3(dropPosition.x, -0.5f, dropPosition.z);

        // 물리 연산 재개
        Rigidbody rb = objectToDrop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        _oxygenObject = null;
    }

    protected PlayerPick FindOtherPlayerPick()
    {
        PlayerPick[] playerPicks = FindObjectsOfType<PlayerPick>();
        foreach (PlayerPick playerPick in playerPicks)
        {
            if (playerPick != this)
            {
                return playerPick;
            }
        }
        return null;
    }
}
