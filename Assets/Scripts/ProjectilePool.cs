using UnityEngine;
using System.Collections.Generic;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private Projectile _projectilePrefab;
    [SerializeField] private int _poolSize = 0;

    private Queue<Projectile> _projectiles = new Queue<Projectile>();
    private int _lastProjectileId = 0;

    private void Awake()
    {
        for (int i=0; i<_poolSize; i++)
        {
            Projectile proj = Instantiate(_projectilePrefab, transform);
            proj.gameObject.SetActive(false); //deactivated prefabs to pool
            _projectiles.Enqueue(proj);
        }
    }

    public Projectile GetProjectile()
    {
        if (_projectiles.Count == 0) //empty queue -> create new projectile
        {
            Projectile proj = Instantiate(_projectilePrefab, transform);
            proj.gameObject.SetActive(false); //deactivated prefabs to pool
            _projectiles.Enqueue(proj);
        }

        Projectile projectile = _projectiles.Dequeue();
        projectile.gameObject.SetActive(true);
        return projectile;

    }

    public void ReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
        _projectiles.Enqueue(projectile);
    }
}
