using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Screens;

public class ScreenManager
{
    public Game1 Game { get; }
    public IScreen CurrentScreen { get; private set; }

    public ScreenManager(Game1 game)
    {
        Game = game;
    }

    public void ChangeScreen(IScreen newScreen)
    {
        CurrentScreen = newScreen;
        CurrentScreen.Initialize();
        CurrentScreen.LoadContent();
    }

    public void Update(GameTime gameTime)
    {
        CurrentScreen?.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        CurrentScreen?.Draw(gameTime, spriteBatch, pixelTexture);
    }
}
