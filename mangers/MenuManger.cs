using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

public class MenuManager
{
    // =====================================================
    // CONTENT
    // =====================================================
    private SpriteFont _font;
    private Texture2D _background;

    // =====================================================
    // MENU STATES 
    // =====================================================
    private bool _isPaused;
    private bool _isInventoryOpen;
    private bool _isMapOpen;

    // =====================================================
    // CONSTRUCTOR
    // =====================================================
    public MenuManager() { }

    // =====================================================
    // LOAD CONTENT
    // =====================================================
    public void LoadContent(ContentManager content)
    {
        _font = content.Load<SpriteFont>("GameFont");
        _background = content.Load<Texture2D>("HUD_Display");
    }

    // =====================================================
    // UPDATE
    // =====================================================
    public void Update(GameTime gameTime)
    {
        // Toggle pause
        if (InputManager.IsMenuTogglePressed())
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _isInventoryOpen = false;
                _isMapOpen = false;
            }
        }

        // Inventory (only if not paused)
        if (!_isPaused && InputManager.IsInventoryButtonPressed())
        {
            _isInventoryOpen = !_isInventoryOpen;
            _isMapOpen = false;
        }

        // Map (only if not paused)
        if (!_isPaused && InputManager.IsMapButtonPressed())
        {
            _isMapOpen = !_isMapOpen;
            _isInventoryOpen = false;
        }
    }

    // =====================================================
    // DRAW ENTRY POINT (CALLED BY GAMEMANAGER)
    // =====================================================
    public void Draw(SpriteBatch spriteBatch)
    {
        if (_isPaused)
            DrawPauseMenu(spriteBatch);
        else if (_isInventoryOpen)
            DrawInventoryMenu(spriteBatch);
        else if (_isMapOpen)
            DrawMapMenu(spriteBatch);
    }

    // =====================================================
    // PAUSE MENU
    // =====================================================
    private void DrawPauseMenu(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 600), Color.White);
        spriteBatch.DrawString(_font, "PAUSED", new Vector2(100, 50), Color.Black);
        spriteBatch.DrawString(_font, "Press ESC to Resume", new Vector2(100, 100), Color.Gray);
    }

    // =====================================================
    // INVENTORY MENU
    // =====================================================
    private void DrawInventoryMenu(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 600), Color.White);
        spriteBatch.DrawString(_font, "INVENTORY", new Vector2(100, 50), Color.Black);

        Player player = GameManager.Instance.CurrentPlayer;
        if (player == null) return;

        int yOffset = 100;

        foreach (var item in player.Inventory)
        {
            spriteBatch.DrawString(
                _font,
                $"- {item.Name}",
                new Vector2(120, yOffset),
                Color.Black
            );
            yOffset += 30;
        }
    }

    // =====================================================
    // MAP MENU
    // =====================================================
    private void DrawMapMenu(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 800, 600), Color.LightBlue);
        spriteBatch.DrawString(_font, "MAP", new Vector2(100, 50), Color.Black);
        spriteBatch.DrawString(_font, "Press M to Close", new Vector2(100, 100), Color.Gray);
        spriteBatch.DrawString(_font, "Player Position:", new Vector2(100, 150), Color.Black);
        
        var player = GameManager.Instance.CurrentPlayer;
        if (player != null)
            {
                spriteBatch.DrawString(_font,$"X: {(int)player.Position.X}, Y: {(int)player.Position.Y}",new Vector2(100, 180), Color.Black);
            }
    }

    // =====================================================
    // STATE QUERIES (OPTIONAL)
    // =====================================================
    public bool IsPaused => _isPaused;
    public bool IsInventoryOpen => _isInventoryOpen;
    public bool IsMapOpen => _isMapOpen;

    // =====================================================
    // FORCE RESET (USED ON GAME RESET)
    // =====================================================
    public void CloseAll()
    {
        _isPaused = false;
        _isInventoryOpen = false;
        _isMapOpen = false;
    }
}
