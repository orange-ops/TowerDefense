using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed;
    private int _damage;
    private Transform _target;
    private ProjectilePool _pool;

    public void Shoot(float speed, int damage, Transform target, ProjectilePool pool)
    {
        _speed = speed;
        _damage = damage;
        _target = target;
        _pool = pool;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;
            if ((_target.position - transform.position).sqrMagnitude < 0.01f)
            {
                Enemy enemy = _target.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.Damage(_damage);
                gameObject.SetActive(false);
            }
        }
        else
        {
            _pool.ReturnProjectileToPool(this);
        }
    }
}
