using System;
using Godot;

[GlobalClass]
public partial class EntityData : Resource
{
    [Export] public Godot.Collections.Array<AudioStream> EntityDeathSound;
    [Export] public int ScoreValue = 0;
}