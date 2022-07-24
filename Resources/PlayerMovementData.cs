using Godot;

public class PlayerMovementData : Resource
{
    [Export] public int _gravity = 4;
    [Export] public int _acceleration = 100;
    [Export] public int _jumpForce = -150;
    [Export] public int _fallForce = 20;
    [Export] public int _fallOffLimit = -770;
    [Export] public int _gravityLimit = 300;
}