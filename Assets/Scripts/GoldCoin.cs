using Godot;

namespace Godot3DActionRPG.Assets.Scripts;

public partial class GoldCoin : Area3D
{
    [Export] private int _goldToGive = 1;
    private float _rotateSpeed = 5f;

    public override void _Process(double delta)
    {
        // Rotate along Y axis
        RotateY(_rotateSpeed * (float)delta);
    }

    public void OnBodyEntered(Node3D body)
    {
        /* Maybe just work in gdscript
        if (body.Name == "Player")
        {
            body.GiveGold(_goldToGive);
        } */
        if (body is not Player bodyInstance) return;
        bodyInstance.GiveGold(_goldToGive);
        QueueFree();
    }
}