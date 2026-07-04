using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    // Textura de 1x1 pixel gerada em memória para desenharmos sem precisar de arquivos externos
    private Texture2D _pixelTexture;

    // Dimensões da tela fixas
    private const int Width = 800;
    private const int Height = 600;

    // Entidades do jogo (Retângulos contendo X, Y, Largura e Altura)
    private Rectangle _player;
    private Rectangle _ai;
    private Rectangle _ball;

    // Movimentação da bola e velocidades
    private Vector2 _ballVelocity;
    private float _ballSpeed = 6f;
    private int _paddleSpeed = 7;

    // Pontuação
    private int _playerScore = 0;
    private int _aiScore = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = Width;
        _graphics.PreferredBackBufferHeight = Height;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // Posicionamento inicial das raquetes (X, Y, Largura, Altura)
        _player = new Rectangle(30, Height / 2 - 50, 20, 100);
        _ai = new Rectangle(Width - 50, Height / 2 - 50, 20, 100);

        ResetBall(1); // 1 = Bola vai para o jogador, -1 = vai para a IA

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Criando a textura branca em memória
        _pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
        _pixelTexture.SetData(new[] { Color.White });
    }

    private void ResetBall(int direction)
    {
        // Centraliza a bola
        _ball = new Rectangle(Width / 2 - 10, Height / 2 - 10, 20, 20);
        
        // Define a direção inicial baseada em quem pontuou
        _ballVelocity = new Vector2(direction * _ballSpeed, 3f);
    }

    protected override void Update(GameTime gameTime)
    {
        // Sair do jogo ao apertar ESC
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var kState = Keyboard.GetState();

        // --- CONTROLE DO JOGADOR ---
        if (kState.IsKeyDown(Keys.W) && _player.Y > 0)
            _player.Y -= _paddleSpeed;
        if (kState.IsKeyDown(Keys.S) && _player.Y < Height - _player.Height)
            _player.Y += _paddleSpeed;

        // --- COMPORTAMENTO DA IA ---
        // A IA persegue o centro da bola no eixo Y
        int aiCenter = _ai.Y + (_ai.Height / 2);
        int ballCenter = _ball.Y + (_ball.Height / 2);

        if (ballCenter < aiCenter && _ai.Y > 0)
            _ai.Y -= _paddleSpeed - 1; // Um pouco mais lenta para dar chance de vitória
        if (ballCenter > aiCenter && _ai.Y < Height - _ai.Height)
            _ai.Y += _paddleSpeed - 1;

        // --- MOVIMENTAÇÃO DA BOLA ---
        // Como retângulos usam inteiros para pixels, acumulamos a velocidade na posição
        _ball.X += (int)_ballVelocity.X;
        _ball.Y += (int)_ballVelocity.Y;

        // --- FÍSICA E COLISÕES ---
        
        // Colisão com o teto e chão
        if (_ball.Y <= 0 || _ball.Y >= Height - _ball.Height)
        {
            _ballVelocity.Y *= -1;
            // Corrige a posição para não prender nas bordas
            _ball.Y = Math.Clamp(_ball.Y, 0, Height - _ball.Height);
        }

        // Colisão com as Raquetes usando a função nativa do MonoGame
        if (_ball.Intersects(_player))
        {
            _ballVelocity.X = _ballSpeed; // Rebate para a direita
            IncreaseBallSpeed();
        }
        else if (_ball.Intersects(_ai))
        {
            _ballVelocity.X = -_ballSpeed; // Rebate para a esquerda
            IncreaseBallSpeed();
        }

        // --- SISTEMA DE PONTOS ---
        if (_ball.X < 0) // Ponto da IA
        {
            _aiScore++;
            _ballSpeed = 6f; // Reseta a velocidade
            ResetBall(1);
        }
        else if (_ball.X > Width) // Ponto do Jogador
        {
            _playerScore++;
            _ballSpeed = 6f; // Reseta a velocidade
            ResetBall(-1);
        }

        base.Update(gameTime);
    }

    private void IncreaseBallSpeed()
    {
        // Aumenta ligeiramente a velocidade a cada rebatida para o jogo ficar desafiador
        _ballSpeed += 0.5f;
    }

    protected override void Draw(GameTime gameTime)
    {
        // Fundo preto clássico de arcade
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        // Desenha a linha central (rede)
        for (int y = 0; y < Height; y += 40)
        {
            _spriteBatch.Draw(_pixelTexture, new Rectangle(Width / 2 - 2, y, 4, 20), Color.DarkGray);
        }

        // Desenha as raquetes e a bola na tela
        _spriteBatch.Draw(_pixelTexture, _player, Color.White);
        _spriteBatch.Draw(_pixelTexture, _ai, Color.White);
        _spriteBatch.Draw(_pixelTexture, _ball, Color.White);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}