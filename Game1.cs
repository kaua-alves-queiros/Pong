using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core;
using Pong.Screens;

namespace Pong;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixelTexture;

    private ScreenManager _screenManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = GameSettings.Width;
        _graphics.PreferredBackBufferHeight = GameSettings.Height;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        _screenManager = new ScreenManager(this);
        _screenManager.ChangeScreen(new MainMenuScreen(_screenManager));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Generate 1x1 white texture programmatically to draw retro shapes/text
        _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { Color.White });
    }

    protected override void Update(GameTime gameTime)
    {
        // Update global input manager keyboard states (down vs pressed checks)
        InputManager.Update();

        // Route update call to active screen
        _screenManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // Set background color to black
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // Route rendering call to active screen
        _screenManager.Draw(gameTime, _spriteBatch, _pixelTexture);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}