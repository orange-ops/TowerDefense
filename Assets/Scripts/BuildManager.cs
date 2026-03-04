using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private Transform towerParent;

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
        if (Physics.Raycast(ray, out RaycastHit hit))
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
        if (_hoveredTile != null && _hoveredTile.IsBuildable())
        {
            GameObject tower = Instantiate(_towerPrefab, towerParent);
            tower.transform.position = _hoveredTile.transform.position;
            _hoveredTile.SetAsOccupied();
        }
    }

    
}
