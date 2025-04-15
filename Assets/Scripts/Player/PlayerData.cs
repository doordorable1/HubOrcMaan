using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData", order = int.MaxValue)]
public class PlayerData : ScriptableObject
{
    public int maxHealth = 30;
    public int currentHealth;

    public float movementSpeed = 5f;
    public float dashSpeed = 17f;
    public float dashDuration = 0.17f;
    public float dashCooldown = 0.8f;
}
