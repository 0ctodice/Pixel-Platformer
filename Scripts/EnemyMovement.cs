using Godot;
using static Godot.GD;

public class EnemyMovement : KinematicBody2D
{
    private RayCast2D _LeftEdgeCheck;
    private RayCast2D _RightEdgeCheck;
    private AnimatedSprite _animatedSprite;
    private Vector2 _direction = Vector2.Left;
    private Vector2 _velocity = Vector2.Zero;

    public override void _Ready()
    {
        _LeftEdgeCheck = GetNode<RayCast2D>("LeftEdgeCheck");
        _RightEdgeCheck = GetNode<RayCast2D>("RightEdgeCheck");
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        if (IsOnWall() || !_LeftEdgeCheck.IsColliding() || !_RightEdgeCheck.IsColliding())
        {
            _direction *= -1;
            _animatedSprite.FlipH = _direction.x > 0;
        }

        _velocity = _direction * 25;
        MoveAndSlide(_velocity, Vector2.Up);
    }
}