using Godot;
using static Godot.GD;

public class HitBox : Area2D
{

    public override void _Ready()
    {
        this.Connect("body_entered", this, "OnHitBoxBodyEntered");
    }

    public void OnHitBoxBodyEntered(Node body)
    {
        GetTree().ReloadCurrentScene();
    }
}