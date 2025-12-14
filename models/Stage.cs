using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Stage
{
    // -----------------------------
    // TILE DEFINITIONS
    // -----------------------------
    public enum TileType
    {
        Grass = 0,
        Water = 2,
        Elevator = 3,
        Sand = 4,
        Highway = 5,
        ShipPart = 6,
        Hole = 7
    }

    private readonly Texture2D _tilesheet;
    private readonly int _tileSize;
    private readonly int[,] _grid;
    private TileType[,] tiles;
    private readonly Dictionary<int, Rectangle> _tileMap;
    private readonly Random rng = new();

    private Point elevatorTile;

    private const int MaxWidth = 100;
    private const int MaxHeight = 100;

    public int Width => _grid.GetLength(0);
    public int Height => _grid.GetLength(1);

    // -----------------------------
    // CONSTRUCTOR
    // -----------------------------
    public Stage(Texture2D tilesheet, int tileSize, int width, int height,
                 Dictionary<int, Rectangle> tileMap)
    {
        _tilesheet = tilesheet;
        _tileSize = tileSize;
        _tileMap = tileMap;

        width = Math.Min(width, MaxWidth);
        height = Math.Min(height, MaxHeight);

        _grid = new int[width, height];
    }

    // -----------------------------
    // GENERATION
    // -----------------------------
    public void GenerateStage(StageCriteria criteria)
    {
        for (int y = 0; y < Height; y++)
        for (int x = 0; x < Width; x++)
        {
            if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                tiles[x, y] = TileType.Water;
            else
                tiles[x, y] = TileType.Grass;
         }

        tiles[5, 5] = TileType.Elevator;
        tiles[8, 8] = TileType.Hole;
    }

    private void FillRegion(int startX, int startY, int width, int height, TileType type)
    {
        for (int x = startX; x < startX + width && x < Width; x++)
            for (int y = startY; y < startY + height && y < Height; y++)
                _grid[x, y] = (int)type;
    }

    private void PlaceHoles(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int x = rng.Next(1, Width - 1);
            int y = rng.Next(1, Height - 1);
            _grid[x, y] = (int)TileType.Hole;
        }
    }

    private void PlaceElevator()
    {
        int x = rng.Next(2, Width - 2);
        int y = rng.Next(2, Height - 2);
        _grid[x, y] = (int)TileType.Elevator;
        elevatorTile = new Point(x, y);
    }

    private void PlaceShipPart()
    {
        int x = rng.Next(0, Width);
        int y = rng.Next(0, Height);
        _grid[x, y] = (int)TileType.ShipPart;
    }

    // -----------------------------
    // TILE QUERIES (USED BY PLAYER / ENEMY)
    // -----------------------------
    private TileType GetTile(Vector2 position)
    {
        int x = (int)(position.X / _tileSize);
        int y = (int)(position.Y / _tileSize);

        if (x < 0 || y < 0 || x >= Width || y >= Height)
            return TileType.Water; // outside bounds = blocked

        return (TileType)_grid[x, y];
    }

    public bool IsBlocked(Vector2 position)
    {
        Point tile = WorldToTile(position);
        return IsSolid(tile.X, tile.Y);
    }
    public bool IsWater(Vector2 position) =>
        GetTile(position) == TileType.Water;

    public bool IsHole(Vector2 position) =>
        GetTile(position) == TileType.Hole;
    
    public Point WorldToTile(Vector2 position)
    {
        int x = (int)(position.X / _tileSize);
        int y = (int)(position.Y / _tileSize);
        return new Point(x, y);
    }

    public bool IsElevator(Vector2 position)
    {
        int x = (int)(position.X / _tileSize);
        int y = (int)(position.Y / _tileSize);
        return x == elevatorTile.X && y == elevatorTile.Y;
    }

    public bool IsSolid(int x, int y)
    {
        return tiles[x, y] == TileType.Water || tiles[x, y] == TileType.Hole;
    }

    // -----------------------------
    // DRAW
    // -----------------------------
    public void Draw(SpriteBatch spriteBatch, Rectangle visibleArea)
    {
        int startX = Math.Max(0, visibleArea.Left / _tileSize);
        int startY = Math.Max(0, visibleArea.Top / _tileSize);
        int endX = Math.Min(Width - 1, visibleArea.Right / _tileSize);
        int endY = Math.Min(Height - 1, visibleArea.Bottom / _tileSize);

        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                int idx = _grid[x, y];
                if (_tileMap.TryGetValue(idx, out var src))
                {
                    var dest = new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize);
                    spriteBatch.Draw(_tilesheet, dest, src, Color.White);
                }
            }
        }
    }
}
