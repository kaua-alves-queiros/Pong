using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Core;
using Pong.Rendering;

namespace Pong.Screens;

public class MainMenuScreen : IScreen
{
    private readonly ScreenManager _screenManager;
    private int _selectedOption = 0; // 0 = Start, 1 = Exit
    private int _highScore = 0;

    public MainMenuScreen(ScreenManager screenManager)
    {
        _screenManager = screenManager;
    }

    public void Initialize()
    {
        _highScore = SaveManager.LoadHighScore();
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        if (InputManager.IsKeyPressed(Keys.Up) || InputManager.IsKeyPressed(Keys.W))
        {
            _selectedOption = 0;
        }
        else if (InputManager.IsKeyPressed(Keys.Down) || InputManager.IsKeyPressed(Keys.S))
        {
            _selectedOption = 1;
        }

        if (InputManager.IsKeyPressed(Keys.Enter) || InputManager.IsKeyPressed(Keys.Space))
        {
            if (_selectedOption == 0)
            {
                // Start gameplay
                _screenManager.ChangeScreen(new GameplayScreen(_screenManager));
            }
            else if (_selectedOption == 1)
            {
                // Exit game
                _screenManager.Game.Exit();
            }
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        _screenManager.Game.GraphicsDevice.Clear(Color.Black);

        int width = GameSettings.Width;
        int height = GameSettings.Height;

        // Title "PONG" in large pixel scale
        string title = "PONG";
        int titleScale = 10;
        Vector2 titleSize = PixelFontRenderer.MeasureText(title, titleScale);
        int titleX = (width - (int)titleSize.X) / 2;
        int titleY = 120;
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, title, titleX, titleY, titleScale, Color.White);

        // Learning info subtitle
        string subtitle = "APRENDENDO MONOGAME";
        int subScale = 2;
        Vector2 subSize = PixelFontRenderer.MeasureText(subtitle, subScale);
        int subX = (width - (int)subSize.X) / 2;
        int subY = titleY + (int)titleSize.Y + 20;
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, subtitle, subX, subY, subScale, Color.DarkGray);

        // High Score display
        string highScoreText = $"RECORD: {_highScore}";
        int scoreScale = 3;
        Vector2 scoreSize = PixelFontRenderer.MeasureText(highScoreText, scoreScale);
        int scoreX = (width - (int)scoreSize.X) / 2;
        int scoreY = subY + 60;
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, highScoreText, scoreX, scoreY, scoreScale, Color.Yellow);

        // Menu options
        string startText = "JOGAR";
        string exitText = "SAIR";
        int menuScale = 4;

        Vector2 startSize = PixelFontRenderer.MeasureText(startText, menuScale);
        Vector2 exitSize = PixelFontRenderer.MeasureText(exitText, menuScale);

        int startX = (width - (int)startSize.X) / 2;
        int startY = height / 2 + 50;

        int exitX = (width - (int)exitSize.X) / 2;
        int exitY = startY + 60;

        if (_selectedOption == 0)
        {
            string selector = "> ";
            Vector2 selSize = PixelFontRenderer.MeasureText(selector, menuScale);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, selector, startX - (int)selSize.X, startY, menuScale, Color.Green);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, startText, startX, startY, menuScale, Color.Green);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, exitText, exitX, exitY, menuScale, Color.Gray);
        }
        else
        {
            string selector = "> ";
            Vector2 selSize = PixelFontRenderer.MeasureText(selector, menuScale);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, startText, startX, startY, menuScale, Color.Gray);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, selector, exitX - (int)selSize.X, exitY, menuScale, Color.Red);
            PixelFontRenderer.DrawText(spriteBatch, pixelTexture, exitText, exitX, exitY, menuScale, Color.Red);
        }

        // Instructions Footer
        string footer = "W/S OU SETAS PARA NAVEGAR - ENTER PARA SELECIONAR";
        int footerScale = 2;
        Vector2 footerSize = PixelFontRenderer.MeasureText(footer, footerScale);
        int footerX = (width - (int)footerSize.X) / 2;
        int footerY = height - 60;
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, footer, footerX, footerY, footerScale, Color.DarkGray);
    }
}
