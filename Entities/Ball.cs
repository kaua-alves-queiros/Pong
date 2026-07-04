using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Entities;

public class Ball
{
    public Rectangle Position;
    public Vector2 Velocity;
    public float Speed { get; private set; }
    private const float InitialSpeed = 6f;

    public Ball(int x, int y, int width, int height)
    {
        Position = new Rectangle(x, y, width, height);
        Speed = InitialSpeed;
    }

    public void Update()
    {
        Position.X += (int)Velocity.X;
        Position.Y += (int)Velocity.Y;
    }

    public void Reset(int direction, int screenWidth, int screenHeight)
    {
        Speed = InitialSpeed;
        Position.X = screenWidth / 2 - Position.Width / 2;
        Position.Y = screenHeight / 2 - Position.Height / 2;
        Velocity = new Vector2(direction * Speed, 3f);
    }

    public void CheckWallCollisions(int screenHeight)
    {
        // Roof and floor collisions
        if (Position.Y <= 0)
        {
            Velocity.Y = MathF.Abs(Velocity.Y); // Force downward velocity
            Position.Y = 0; // Clamp
        }
        else if (Position.Y >= screenHeight - Position.Height)
        {
            Velocity.Y = -MathF.Abs(Velocity.Y); // Force upward velocity
            Position.Y = screenHeight - Position.Height; // Clamp
        }
    }

    public void BounceOffPaddle(bool toRight)
    {
        if (toRight)
        {
            Velocity.X = Speed; // Bounce to the right
        }
        else
        {
            Velocity.X = -Speed; // Bounce to the left
        }
        IncreaseSpeed();
    }

    private void IncreaseSpeed()
    {
        Speed += 0.5f;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture, Color color)
    {
        spriteBatch.Draw(pixelTexture, Position, color);
    }
}
