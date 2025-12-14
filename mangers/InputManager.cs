using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class InputManager
{
    private static readonly KeyboardInputManager keyboard = new KeyboardInputManager();
    private static KeyboardState currentState;
    private static KeyboardState previousState;

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public static void Update()
    {
        keyboard.Update();
        previousState = currentState;
        currentState = Keyboard.GetState();
    }

    // -----------------------------
    // MOVEMENT INPUT
    // -----------------------------
    public static Vector2 GetMovementVector()
    {
        Vector2 direction = Vector2.Zero;

        if (IsKeyHeldOrJustPressed(Keys.W)) direction.Y -= 1;
        if (IsKeyHeldOrJustPressed(Keys.S)) direction.Y += 1;
        if (IsKeyHeldOrJustPressed(Keys.A)) direction.X -= 1;
        if (IsKeyHeldOrJustPressed(Keys.D)) direction.X += 1;

        if (direction != Vector2.Zero)
            direction.Normalize();

        return direction;
    }

    public static Direction GetPrimaryDirection()
    {
        var dir = GetMovementVector();

        if (dir == Vector2.Zero) return Direction.None;
        if (Math.Abs(dir.X) > Math.Abs(dir.Y))
            return dir.X > 0 ? Direction.Right : Direction.Left;
        else
            return dir.Y > 0 ? Direction.Down : Direction.Up;
    }

    // -----------------------------
    // BUTTON INPUTS
    // -----------------------------

    // "A" button (Pause/Menu)
    public static bool IsMenuTogglePressed()
    {
        return keyboard.GetKeyState(Keys.Escape) == KeyState.JUSTPRESSED;
    }

    // "B" button (Open/Close Presents List)
    public static bool IsInventoryButtonPressed()
    {
        // Using B key for "Presents"
        return keyboard.GetKeyState(Keys.K) == KeyState.JUSTPRESSED;
    }

    // "C" button (Open/Close Map)
    public static bool IsMapButtonPressed()
    {
        // Using C key for Map toggle
        return keyboard.GetKeyState(Keys.C) == KeyState.JUSTPRESSED;
    }

    public static bool isSneakButtonPressed()
    {
        return Keyboard.GetState().IsKeyDown(Keys.LeftShift);
    }

    // -----------------------------
    // HELPER
    // -----------------------------
    private static bool IsKeyHeldOrJustPressed(Keys key)
    {
        var state = keyboard.GetKeyState(key);
        return state == KeyState.HELD || state == KeyState.JUSTPRESSED;
    }
}
