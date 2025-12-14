using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

public class KeyboardInputManager
{
    private KeyboardState currentState;
    private KeyboardState previousState;
    private Dictionary<Keys, KeyState> keyStates = new();

    public void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();

        foreach (Keys key in Enum.GetValues(typeof(Keys)))
        {
            bool wasDown = previousState.IsKeyDown(key);
            bool isDown = currentState.IsKeyDown(key);

            if (!wasDown && isDown)
                keyStates[key] = KeyState.JUSTPRESSED;
            else if (wasDown && isDown)
                keyStates[key] = KeyState.HELD;
            else if (wasDown && !isDown)
                keyStates[key] = KeyState.JUSTRELEASED;
            else
                keyStates[key] = KeyState.UNPRESSED;
        }
    }

    public KeyState GetKeyState(Keys key)
    {
        if (keyStates.TryGetValue(key, out var state))
            return state;
        return KeyState.UNPRESSED;
    }
}
