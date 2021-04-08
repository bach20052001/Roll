using UnityEngine;

[CreateAssetMenu(fileName = "Config",menuName = "GameConfiguration",order = 0)]
public class GameConfiguration : ScriptableObject
{
    [Header("Player without powerup")]
    public float speed;
    public float forceToEnemy;
    [Header("Player with powerup")]
    public float speed_powerup;
    public float forceToEnemy_powerup;
    [Header("Enemy")]
    public float enemySpeed;
}
