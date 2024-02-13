using Godot;

namespace Godot3DActionRPG.Assets.Scripts;

public partial class CameraOrbit : Node3D
{
    [Export]
    private float _lookSensitivity = 15f;

    [Export]
    private static float _minLookAngle = -20f;
    [Export]
    private static float _maxLookAngle = 75f;

    private Vector2 _mouseDelta = new Vector2();

    private CharacterBody3D _player;
    private Vector3 _rotation  = new Vector3();

    
    private EventBus _eventBus;
    
    private Vector3 _aux = new Vector3();


    public override void _Ready()
    {
        _player = GetParent<CharacterBody3D>();
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            _mouseDelta = mouseMotion.Relative;
        }
    }

    public override void _Process(double delta)
    {
        _rotation = new Vector3(_mouseDelta.Y, _mouseDelta.X, 0) * _lookSensitivity * (float)delta;

        if (_aux == _rotation) return;
        _aux = _rotation;
        var rotationDeg = RotationDegrees;
        rotationDeg.X += _rotation.X;
        rotationDeg.X = Mathf.Clamp(rotationDeg.X, _minLookAngle, _maxLookAngle);
        RotationDegrees = rotationDeg;


        var playerRotationDeg = _player.RotationDegrees;
        playerRotationDeg.Y -= _rotation.Y;
        _player.RotationDegrees = playerRotationDeg;

        _mouseDelta = new Vector2();
    }
}