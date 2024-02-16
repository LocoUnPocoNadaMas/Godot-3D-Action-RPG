using Godot;

namespace Godot3DActionRPG.Assets.Scripts;

public partial class Player : Character
{
    /*
    private int _curHp = 10;
    private int _maxHp = 10;
    private int _damage = 1;
    private float _attackRate = .3f;
    private float _moveSpeed = 5f;*/
    private Vector3 Vel = new Vector3();
    
    private int _goldCoin = 5;
    private float _lastAttack = 0f;
    private float _jumpForce = 10f;
    private float _gravity = 15f;
    private Node3D _cameraOrbit = null;
    private RayCast3D _ray = null;
    private Vector3 _input = new Vector3();
    private Vector3 _dir = new Vector3();

    public override void _Ready()
    {
        _cameraOrbit = GetNode<Node3D>("CameraOrbit");
        _ray = GetNode<RayCast3D>("AttackRayCast");
        OnInit();
    }

    public override void _PhysicsProcess(double delta)
    {
        //_velocit.X = 0;
        //_velocit.Z = 0;
        Vel = Velocity;
        _input = new Vector3(0,0,0);
        
        // Movement input
        if (Input.IsActionPressed("ui_up"))
        {
            GD.Print("arriba");
            _input.Z += 1;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            GD.Print("abajo");
            _input.Z -= 1;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            GD.Print("izquierda");
            _input.X += 1;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            GD.Print("derecha");
            _input.X -= 1;
        }
        //if(_input.Equals(Vector3.Zero)) return;

        // Normalize the input to prevent increase diagonal
        _input = _input.Normalized();
        // Get the relative direction
        _dir = new Vector3();
        _dir = (Transform.Basis.Z * _input.Z + Transform.Basis.X * _input.X);
        // Apply the direction to our velocity
        Vel.X = _dir.X * MoveSpeed;
        Vel.Z = _dir.Z * MoveSpeed;
        // Gravity
        Vel.Y -= _gravity * (float)delta;
        // Jump input
        if (Input.IsActionPressed("ui_select") && IsOnFloor())
        {
            GD.Print("saltito");
            Vel.Y = _jumpForce;
        }
        // Move along the current velocity
        /* godot 3.5
         * _velocit = MoveAndSlide(_velocit, Vector3.Up);
         */
        Velocity = Vel;
        MoveAndSlide();
    }

    // Called when collect a coin
    public void GiveGold(int goldToGive)
    {
        _goldCoin += goldToGive;
    }

}