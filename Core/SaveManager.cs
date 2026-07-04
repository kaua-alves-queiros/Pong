using System;
using System.IO;

namespace Pong.Core;

public static class SaveManager
{
    public static int LoadHighScore()
    {
        try
        {
            string path = GameSettings.HighScoreFilePath;
            if (File.Exists(path))
            {
                string text = File.ReadAllText(path);
                if (int.TryParse(text, out int highScore))
                {
                    return highScore;
                }
            }
        }
        catch (Exception)
        {
            // Fail silently or handle appropriately in game logs
        }
        return 0;
    }

    public static void SaveHighScore(int score)
    {
        try
        {
            string path = GameSettings.HighScoreFilePath;
            string directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(path, score.ToString());
        }
        catch (Exception)
        {
            // Fail silently
        }
    }
}
