using Godot;

namespace Godot3DActionRPG.Assets.Scripts;

public partial class Enemy : Character
{
    [Export] private float _attackDist;
    private Timer _timer;
    private Player _player;
    private float _distance;
    private Vector3 _direction;

    public override void _Ready()
    {
        //OnInit();
        _timer = GetNode<Timer>("Timer");
        var sceneTree = GetTree();
        //GD.Print("a: "+sceneTree.CurrentScene.Name);

        var rootNode = sceneTree.Root;
        //GD.Print("root: "+root_node.Name);

        _player = rootNode.GetNode<Player>("/root/Main/Level 1/Player");
        // Set timer wait time
        _timer.WaitTime = AttackRate;
        _timer.Start();
        if (_attackDist <= 0)
        {
            GD.Print("no hay distancia de ataque");
            GetTree().Quit();
        }

        if (_player is null)
        {
            GD.Print("Null");
            GetTree().Quit();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Distance to player
        _distance =  Transform.Origin.DistanceTo(_player.Transform.Origin);
        // Chase
        if (_distance > _attackDist)
        {
            _direction = (_player.Transform.Origin - Transform.Origin).Normalized();
        }

        Vel.X = _direction.X * MoveSpeed;
        Vel.Y = 0;
        Vel.Z = _direction.Z * MoveSpeed;
        Velocity = Vel;
        MoveAndSlide();
    }

    // Called every "attackRate" seconds
    public void OnTimerTimeout()
    {
        // GD.Print("tiempo");
        /*
         if translation.distance_to(player.translation) <= attackDist:
        player.take_damage(damage)

        https://forum.godotengine.org/t/distance-to-in-3d-how-to-get-distance-between-2-bodies-in-3d/19096/2

            Vector3.distance_to(Vector3)
            translation, transform.origin and global_transform.origin can be used to get the Vector3’s and try not to mix one with the other for example
            translation.distance_to(other_node.global_transform.origin)
            will not work properly if the nodes are parented either to each other or to the same spatial node that’s not at (0,0,0).
        */
        var aux = Transform.Origin.DistanceTo(_player.Transform.Origin);
        if (aux <= _attackDist)
        {
            // GD.Print("auch");
            _player.TakeDamage(Damage);
        }
        // GD.Print("aux: "+aux+" dist: "+_attackDist);
    }

    public override void TakeDamage(int damage)
    {
        CurHp -= damage;
        if (CurHp <= 0)
            Die();
    }

    protected override void Die()
    {
        QueueFree();
    }
}