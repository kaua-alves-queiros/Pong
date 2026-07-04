using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Entities;

public class Paddle
{
    public Rectangle Position;
    public int Speed { get; }
    public int Score { get; set; }

    public Paddle(int x, int y, int width, int height, int speed)
    {
        Position = new Rectangle(x, y, width, height);
        Speed = speed;
        Score = 0;
    }

    public void MoveUp(int limitY = 0)
    {
        Position.Y -= Speed;
        if (Position.Y < limitY)
        {
            Position.Y = limitY;
        }
    }

    public void MoveDown(int screenHeight)
    {
        Position.Y += Speed;
        if (Position.Y > screenHeight - Position.Height)
        {
            Position.Y = screenHeight - Position.Height;
        }
    }

    public void UpdateAI(Rectangle ball, int screenHeight)
    {
        int aiCenter = Position.Y + (Position.Height / 2);
        int ballCenter = ball.Y + (ball.Height / 2);

        // AI is slightly slower or has a buffer to be beatable
        int aiSpeed = Speed - 1;

        if (ballCenter < aiCenter && Position.Y > 0)
        {
            Position.Y -= aiSpeed;
        }
        else if (ballCenter > aiCenter && Position.Y < screenHeight - Position.Height)
        {
            Position.Y += aiSpeed;
        }
    }

    public void ResetPosition(int y)
    {
        Position.Y = y;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture, Color color)
    {
        spriteBatch.Draw(pixelTexture, Position, color);
    }
}
