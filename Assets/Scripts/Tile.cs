using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        Grass,
        Path
    }

    [SerializeField] private TileType tileType;

    public bool IsBuildable()
    {
        return tileType == TileType.Grass;
    }

}
