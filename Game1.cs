using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ToeJamEarl
{
    public class Game1 : Game
    {
        // =====================================================
        // CORE SYSTEMS
        // =====================================================
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;

        // =====================================================
        // STAGE MANAGEMENT
        // =====================================================
        private Stage _stage;
        private StageManager _stageManager = new StageManager();
        private bool _isTransitioningStage = false;

        // =====================================================
        // START SCREEN
        // =====================================================
        private StartScreen _startScreen;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        // =====================================================
        // INITIALIZE
        // =====================================================
        protected override void Initialize()
        {
            base.Initialize();

            _camera = new Camera2D(GraphicsDevice.Viewport);

            // Initialize enemy spawn nodes
            GameManager.Instance.InitializeSpawnNodes(new List<Vector2>
            {
                new Vector2(300, 200),
                new Vector2(600, 400),
                new Vector2(900, 100)
            });
        }

        // =====================================================
        // LOAD CONTENT
        // =====================================================
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // ---------------- START SCREEN ----------------
            SpriteFont font = Content.Load<SpriteFont>("GameFont");
            Texture2D bg = Content.Load<Texture2D>("Main_Menu");
            _startScreen = new StartScreen(font, bg);

            // ---------------- TILESET ----------------
            Texture2D tilesheet = Content.Load<Texture2D>("WorldTiles");

            Dictionary<int, Rectangle> tileMap = new()
            {
                { 0, new Rectangle(0,   0, 32, 32) }, // grass
                { 1, new Rectangle(32,  0, 32, 32) }, // dirt
                { 2, new Rectangle(64,  0, 32, 32) }, // water
                { 3, new Rectangle(96,  0, 32, 32) }, // elevator
                { 4, new Rectangle(128, 0, 32, 32) }, // sand
                { 5, new Rectangle(160, 0, 32, 32) }, // highway
                { 6, new Rectangle(192, 0, 32, 32) }, // ship part
                { 7, new Rectangle(224, 0, 32, 32) }  // hole
            };

            _stage = new Stage(tilesheet, 32, 60, 60, tileMap);
            _stage.GenerateStage(_stageManager.GetCurrentCriteria());
            GameManager.Instance.SetStage(_stage);

            // ---------------- PLAYERS ----------------
            Texture2D toeJamTex = Content.Load<Texture2D>("ToeJam_Transparent");
            Texture2D earlTex   = Content.Load<Texture2D>("Earl_Transparent");

            Player player1 = new Player(this, toeJamTex, new Vector2(64, 64), "ToeJam");
            //Player player2 = new Player(this, earlTex,   new Vector2(128, 64), "Earl");

            GameManager.Instance.AddPlayer(player1);
            //GameManager.Instance.AddPlayer(player2);

            // ---------------- ENEMIES ----------------
            List<(Texture2D, string)> enemies = new()
            {
                (Content.Load<Texture2D>("Lil_Devil"), "Goblin"),
                (Content.Load<Texture2D>("Cupid"), "Cupid"),
                (Content.Load<Texture2D>("AngryBees"), "AngryBees"),
                (Content.Load<Texture2D>("Tornado"), "Tornado"),
                (Content.Load<Texture2D>("Shark"), "Shark")
            };

            GameManager.Instance.SpawnEnemies(enemies);

            // ---------------- MENUS / HUD ----------------
            GameManager.Instance.LoadContent(Content);
        }

        // =====================================================
        // UPDATE
        // =====================================================
        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();

            // -------- START SCREEN --------
            if (GameManager.Instance.CurrentState == GameManager.GameState.StartScreen)
            {
                if (kb.IsKeyDown(Keys.Enter))
                    GameManager.Instance.StartGame();

                base.Update(gameTime);
                return;
            }

            // -------- GLOBAL INPUT --------
            if (kb.IsKeyDown(Keys.Escape))
                GameManager.Instance.TogglePause();

            if (kb.IsKeyDown(Keys.I))
                GameManager.Instance.OpenInventory();

            if (kb.IsKeyDown(Keys.M))
                GameManager.Instance.OpenMap();

            // -------- UPDATE GAME --------
            GameManager.Instance.Update(gameTime, _camera);

            // -------- CAMERA FOLLOW --------
            Player mainPlayer = GameManager.Instance.CurrentPlayer;
            if (mainPlayer != null)
                _camera.Follow(mainPlayer.Position);

            // -------- STAGE TRANSITION --------
            if (!_isTransitioningStage && mainPlayer != null &&
                _stage.IsElevator(mainPlayer.Position))
            {
                _isTransitioningStage = true;
                LoadNextStage();
            }

            base.Update(gameTime);
        }

        // =====================================================
        // DRAW
        // =====================================================
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // -------- START SCREEN --------
            if (GameManager.Instance.CurrentState == GameManager.GameState.StartScreen)
            {
                _spriteBatch.Begin();
                _startScreen.Draw(_spriteBatch);
                _spriteBatch.End();
                base.Draw(gameTime);
                return;
            }

            // -------- WORLD DRAW --------
            _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

            _stage.Draw(_spriteBatch, _camera.GetVisibleArea());
            GameManager.Instance.Draw(_spriteBatch, _camera);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // =====================================================
        // LOAD NEXT STAGE
        // =====================================================
        private void LoadNextStage()
        {
            _stageManager.AdvanceStage();

            _stage.GenerateStage(_stageManager.GetCurrentCriteria());

            foreach (var player in GameManager.Instance.Players)
                player.Reset();

            _isTransitioningStage = false;
        }
    }
}
