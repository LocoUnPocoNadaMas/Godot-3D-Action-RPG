﻿using System;
using Godot;

namespace Godot3DActionRPG.Assets.Scripts;

public abstract partial class Character: CharacterBody3D
{
    [Export] protected int CurHp;
    [Export] protected int MaxHp;
    [Export] protected int Damage;
    [Export] protected float AttackRate;
    [Export] protected float MoveSpeed;
    //protected Vector3 Vel;

    protected void OnInit()
    {
        if(CurHp != 0 && MaxHp != 0 && Damage != 0 && AttackRate != 0 && MoveSpeed != 0) return;
        else
        {
            throw new Exception("Falta asignar valor a uno o mas atributos");
        }
    }
}