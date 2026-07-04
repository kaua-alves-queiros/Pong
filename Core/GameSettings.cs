using System;
using System.IO;

namespace Pong.Core;

public static class GameSettings
{
    public const int Width = 800;
    public const int Height = 600;

    public static string HighScoreFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "PongMonoGame",
        "highscore.txt"
    );
}
