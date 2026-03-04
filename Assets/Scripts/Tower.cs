using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _fireRate = 0.75f;

    private float _timeFromLastShot = 0;


    private void Update()
    {
        _timeFromLastShot += Time.deltaTime;
        if (_timeFromLastShot > _fireRate)
        {
            Enemy enemyInReach = FindClosestEnemyInReach();
            if (enemyInReach != null)
            {
                Fire(enemyInReach);
                _timeFromLastShot = 0;
            }
        }
    }

    private Enemy FindClosestEnemyInReach()
    {
        float sqrRange = _range * _range;
        foreach (var enemy in Enemy.ActiveEnemies)
        {
            if ((enemy.transform.position - transform.position).sqrMagnitude <= sqrRange)
                return enemy;
        }
        return null;
    }

    private void Fire(Enemy enemy)
    {
        float sqrRange = _range * _range;
        enemy.Damage(_damage);
    }


}