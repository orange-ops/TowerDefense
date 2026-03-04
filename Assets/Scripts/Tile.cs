using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Grass,
        Path
    }

    [SerializeField] private TileType _tileType;
    [SerializeField] private Color _hoverReadyColor = Color.blue;
    [SerializeField] private Color _hoverNotReadyColor = Color.red;
    private Color _initialColor;
    private Renderer _renderer;

    private bool _isOccupied;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _initialColor = _renderer.material.color;
    }

    public bool IsBuildable()
    {
        if (_isOccupied)
            return false;
        return _tileType == TileType.Grass;
    }

    public void SetAsOccupied()
    {
        _isOccupied = true;
    }

    public void OnHoverEnter()
    {
        if (IsBuildable())
            _renderer.material.color = _hoverReadyColor;
        else
            _renderer.material.color = _hoverNotReadyColor;
    }

    public void OnHoverExit()
    {
        _renderer.material.color = _initialColor;
    }

}
