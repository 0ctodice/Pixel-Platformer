using Godot;
using static Godot.GD;

public class World : Node2D
{
    public override void _Ready()
    {
        VisualServer.SetDefaultClearColor(new Color("AAAAffff"));
    }
}