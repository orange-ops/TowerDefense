using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private Transform towerParent;
    [SerializeField] private LayerMask _tileMask;

    [SerializeField] private PlayerEconomy _playerEconomy;
    [SerializeField] private ProjectilePool _projectilePool;

    private Tile _hoveredTile;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        HandleHover();
        HandleClick();

    }

    private void HandleHover()
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, _tileMask))
        {
            Tile tile = hit.collider.GetComponent<Tile>();
            if (tile != _hoveredTile)
            {
                _hoveredTile?.OnHoverExit();
                _hoveredTile = tile;
                _hoveredTile?.OnHoverEnter();
            }
        }
    }

    private void HandleClick()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryBuild(_hoveredTile);
        }
    }

    private void TryBuild(Tile tile)
    {
        if (_playerEconomy.SpendGold(_towerPrefab.GetComponent<Tower>().Price)) 
        {
            if (_hoveredTile != null && _hoveredTile.IsBuildable())
            {
                GameObject tower = Instantiate(_towerPrefab, towerParent);
                tower.transform.position = _hoveredTile.transform.position;
                tower.GetComponent<Tower>().SetProjectilePool(_projectilePool);
                _hoveredTile.SetAsOccupied();
            }
        }
    }

    
}
