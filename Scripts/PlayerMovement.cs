using Godot;
using static Godot.GD;
public class PlayerMovement : KinematicBody2D
{
    [Export] private PlayerMovementData data;

    private AnimatedSprite _animatedSprite;
    private Vector2 _velocity = Vector2.Zero;

    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
    }

    public override void _PhysicsProcess(float delta)
    {
        ApplyGravity();

        var input = GetInput();

        if (input.x == 0)
        {
            ApplyFriction();
            _animatedSprite.Play("Idle");
        }
        else
        {
            ApplyAcceleration(input.x);
            _animatedSprite.Play("Run");
            _animatedSprite.FlipH = input.x > 0;
        }

        if (IsOnFloor())
        {
            if (Input.IsActionPressed("ui_up")) _velocity.y = data._jumpForce;
        }
        else
        {
            _animatedSprite.Play("Jump");
            if (Input.IsActionJustReleased("ui_up") && _velocity.y < data._fallOffLimit) _velocity.y = data._fallOffLimit;
        }

        var wasOnFloor = IsOnFloor();
        _velocity = MoveAndSlide(_velocity, Vector2.Up);
        if (IsOnFloor() && !wasOnFloor)
        {
            _animatedSprite.Play("Run");
            _animatedSprite.Frame = 1;
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left"), 0f);
    }

    private void ApplyGravity()
    {
        _velocity.y += _velocity.y > 0 ? data._fallForce : data._gravity;
        _velocity.y = Mathf.Min(_velocity.y, data._gravityLimit);
    }

    private void ApplyFriction()
    {
        _velocity.x = Mathf.MoveToward(_velocity.x, 0, data._acceleration / 2f);
    }

    private void ApplyAcceleration(float amount)
    {
        _velocity.x = Mathf.MoveToward(_velocity.x, data._acceleration * amount, data._acceleration / 2f);
    }
}