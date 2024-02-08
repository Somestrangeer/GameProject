using System.Collections;
using System.Collections.Generic;

public interface Enemy
{
    void TakeDamage(float damage);
}

public struct EnemyParams
{
    public static float damageSpeed = 0.5f;
    public static float visibleArea = 8f;
    public static float attackArea = 2f;
    public static int enemySpeed = 3;
}
