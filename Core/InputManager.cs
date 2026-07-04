using Microsoft.Xna.Framework.Input;

namespace Pong.Core;

public static class InputManager
{
    private static KeyboardState _currentKeyboardState;
    private static KeyboardState _previousKeyboardState;

    public static void Update()
    {
        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = Keyboard.GetState();
    }

    public static bool IsKeyDown(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key);
    }

    public static bool IsKeyPressed(Keys key)
    {
        return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
    }
}
