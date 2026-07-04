using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Core;
using Pong.Entities;
using Pong.Rendering;

namespace Pong.Screens;

public class GameplayScreen : IScreen
{
    private readonly ScreenManager _screenManager;
    private Paddle _player;
    private Paddle _ai;
    private Ball _ball;

    private int _highScore;

    public GameplayScreen(ScreenManager screenManager)
    {
        _screenManager = screenManager;
    }

    public void Initialize()
    {
        // Load existing high score to compare in real-time
        _highScore = SaveManager.LoadHighScore();

        // Create player and AI paddles (X, Y, Width, Height, Speed)
        _player = new Paddle(30, GameSettings.Height / 2 - 50, 20, 100, 7);
        _ai = new Paddle(GameSettings.Width - 50, GameSettings.Height / 2 - 50, 20, 100, 7);

        // Create ball (X, Y, Width, Height)
        _ball = new Ball(GameSettings.Width / 2 - 10, GameSettings.Height / 2 - 10, 20, 20);
        _ball.Reset(1, GameSettings.Width, GameSettings.Height);
    }

    public void LoadContent()
    {
    }

    public void Update(GameTime gameTime)
    {
        // Press ESC to go back to the Main Menu
        if (InputManager.IsKeyPressed(Keys.Escape))
        {
            _screenManager.ChangeScreen(new MainMenuScreen(_screenManager));
            return;
        }

        // --- PLAYER CONTROLS ---
        if (InputManager.IsKeyDown(Keys.W))
        {
            _player.MoveUp(0);
        }
        if (InputManager.IsKeyDown(Keys.S))
        {
            _player.MoveDown(GameSettings.Height);
        }

        // --- AI CONTROLS ---
        _ai.UpdateAI(_ball.Position, GameSettings.Height);

        // --- BALL PHYSICAL UPDATE ---
        _ball.Update();
        _ball.CheckWallCollisions(GameSettings.Height);

        // --- COLLISION DETECTIONS (PADDLES) ---
        if (_ball.Position.Intersects(_player.Position))
        {
            // Ball hits player paddle, reverse speed to the right
            _ball.BounceOffPaddle(toRight: true);
            // Move ball out of paddle to avoid multi-collision glitch
            _ball.Position.X = _player.Position.Right;
        }
        else if (_ball.Position.Intersects(_ai.Position))
        {
            // Ball hits AI paddle, reverse speed to the left
            _ball.BounceOffPaddle(toRight: false);
            // Move ball out of paddle to avoid multi-collision glitch
            _ball.Position.X = _ai.Position.Left - _ball.Position.Width;
        }

        // --- SCORING CONDITIONS ---
        if (_ball.Position.X < 0)
        {
            // Point for AI
            _ai.Score++;
            _ball.Reset(1, GameSettings.Width, GameSettings.Height);
        }
        else if (_ball.Position.X > GameSettings.Width)
        {
            // Point for Player
            _player.Score++;

            // If player exceeds current high score, save it
            if (_player.Score > _highScore)
            {
                _highScore = _player.Score;
                SaveManager.SaveHighScore(_highScore);
            }

            _ball.Reset(-1, GameSettings.Width, GameSettings.Height);
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D pixelTexture)
    {
        _screenManager.Game.GraphicsDevice.Clear(Color.Black);

        // Draw central dashed line
        for (int y = 0; y < GameSettings.Height; y += 40)
        {
            spriteBatch.Draw(pixelTexture, new Rectangle(GameSettings.Width / 2 - 2, y, 4, 20), Color.DarkGray);
        }

        // Draw Scores on Screen using custom pixel-art font
        string pScore = _player.Score.ToString();
        string aScore = _ai.Score.ToString();
        int fontScale = 6;

        Vector2 pScoreSize = PixelFontRenderer.MeasureText(pScore, fontScale);
        int pScoreX = GameSettings.Width / 2 - 100 - (int)pScoreSize.X;
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, pScore, pScoreX, 40, fontScale, Color.White);

        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, aScore, GameSettings.Width / 2 + 100, 40, fontScale, Color.White);

        // Render entities
        _player.Draw(spriteBatch, pixelTexture, Color.White);
        _ai.Draw(spriteBatch, pixelTexture, Color.White);
        _ball.Draw(spriteBatch, pixelTexture, Color.White);

        // Render footer help text
        string backText = "ESC - VOLTAR AO MENU";
        int backScale = 2;
        Vector2 backSize = PixelFontRenderer.MeasureText(backText, backScale);
        PixelFontRenderer.DrawText(spriteBatch, pixelTexture, backText, (GameSettings.Width - (int)backSize.X) / 2, GameSettings.Height - 30, backScale, Color.DimGray);
    }
}
