using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public sealed class GameManager
{
    // =====================================================
    // GAME STATE
    // =====================================================
    public enum GameState
    {
        StartScreen,
        Playing,
        Paused,
        Inventory,
        Map,
        GameOver
    }

    private static readonly Lazy<GameManager> _lazy =
        new(() => new GameManager());

    public static GameManager Instance => _lazy.Value;

    public GameState CurrentState { get; private set; } = GameState.StartScreen;

    // =====================================================
    // CORE SYSTEMS
    // =====================================================
    public Stage Stage { get; private set; }
    private readonly Random rng = new();

    // =====================================================
    // PLAYERS
    // =====================================================
    private readonly List<Player> _players = new();
    public IReadOnlyList<Player> Players => _players;
    public Player CurrentPlayer => _players.Count > 0 ? _players[0] : null;

    // =====================================================
    // ENEMIES
    // =====================================================
    private readonly List<Enemy> _enemies = new();
    private readonly List<Vector2> _spawnNodes = new();
    private readonly List<Projectile> _projectiles = new();
    private const float EnemyNodeRadius = 150f;

    // =====================================================
    // UI / HUD
    // =====================================================
    private readonly MenuManager _menuManager = new();
    private readonly DisplayHUD _hud = new();

    // =====================================================
    // AUDIO
    // =====================================================
    private SoundEffectInstance _bgmInstance;

    // =====================================================
    // CONSTRUCTOR (Singleton)
    // =====================================================
    private GameManager() { }

    // =====================================================
    // INITIALIZATION
    // =====================================================
    public void LoadContent(ContentManager content)
    {
        _menuManager.LoadContent(content);
    }

    public void SetStage(Stage stage)
    {
        Stage = stage;
    }

    // =====================================================
    // PLAYER MANAGEMENT
    // =====================================================
    public void AddPlayer(Player player)
    {
        if (player != null && !_players.Contains(player))
            _players.Add(player);
    }

    public void PlayerFellInHole(Player player)
    {
        TriggerGameOver();
    }

    // =====================================================
    // GAME STATE TRANSITIONS
    // =====================================================
    public void StartGame()
    {
        CurrentState = GameState.Playing;
    }

    public void TogglePause()
    {
        CurrentState = CurrentState switch
        {
            GameState.Playing => GameState.Paused,
            GameState.Paused => GameState.Playing,
            _ => CurrentState
        };
    }

    public void OpenInventory() => CurrentState = GameState.Inventory;
    public void OpenMap() => CurrentState = GameState.Map;

    public void TriggerGameOver()
    {
        CurrentState = GameState.GameOver;
        StopMusic();
    }

    public void ResetGame()
    {
        _players.Clear();
        _enemies.Clear();
        CurrentState = GameState.StartScreen;
    }

    // =====================================================
    // ENEMY MANAGEMENT
    // =====================================================
    public void InitializeSpawnNodes(IEnumerable<Vector2> nodes)
    {
        _spawnNodes.Clear();
        if (nodes != null)
            _spawnNodes.AddRange(nodes);
    }

    public void SpawnEnemies(List<(Texture2D texture, string type)> enemyList)
    {
        if (enemyList == null || enemyList.Count == 0 || _spawnNodes.Count == 0)
            return;

        _enemies.Clear();

        foreach (var node in _spawnNodes)
        {
            var choice = enemyList[rng.Next(enemyList.Count)];
            var enemy = new Enemy(choice.texture, node, choice.type);

            enemy.AddPatrolNode(node + new Vector2(50, 0));
            enemy.AddPatrolNode(node + new Vector2(0, 50));

            _enemies.Add(enemy);
        }
    }

    public void SpawnProjectile(Projectile projectile)
    {
        if (projectile != null)
            _projectiles.Add(projectile);
    }

    // =====================================================
    // UPDATE LOOP
    // =====================================================
    public void Update(GameTime gameTime, Camera2D camera)
    {
        _menuManager.Update(gameTime);

        if (CurrentState != GameState.Playing)
            return;

        foreach (var player in _players)
            player.Update(gameTime, Stage);

        UpdateEnemies(gameTime, camera);
    }

    private void UpdateEnemies(GameTime gameTime, Camera2D camera)
    {
        if (CurrentPlayer == null) return;

        Rectangle visible = camera?.GetVisibleArea()
            ?? new Rectangle(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);

        foreach (var enemy in _enemies)
        {
            bool active =
                _spawnNodes.Any(n => Vector2.Distance(enemy.Position, n) <= EnemyNodeRadius)
                && visible.Contains(enemy.Position.ToPoint());

            if (active)
                enemy.Update(gameTime, CurrentPlayer, Stage);
        }
    }

    // =====================================================
    // DRAW
    // =====================================================
    public void Draw(SpriteBatch spriteBatch, Camera2D camera)
    {
        _menuManager.Draw(spriteBatch);

        if (CurrentState != GameState.Playing)
            return;

        foreach (var player in _players)
            player.Draw(spriteBatch);

        foreach (var enemy in _enemies)
            enemy.Draw(spriteBatch);
    }

    // =====================================================
    // AUDIO CONTROL
    // =====================================================
    public void PlayBackgroundMusic(SoundEffect music)
    {
        _bgmInstance?.Stop();
        _bgmInstance = music.CreateInstance();
        _bgmInstance.IsLooped = true;
        _bgmInstance.Play();
    }

    public void StopMusic() => _bgmInstance?.Stop();
}
