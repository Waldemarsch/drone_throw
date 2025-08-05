using Godot;
using System;

public partial class DestructibleComponent : Node
{
    [ExportGroup("Audio")]
    [Export] public AudioStream DamageSound { get; set; }
}
