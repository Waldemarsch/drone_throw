using System;
using Godot;

[GlobalClass] public partial class EntityData : Resource
{
    [Export] public Texture2D EntityTexture;
    [Export] public SpriteFrames SpriteFramesRes;
    [Export] public Godot.Collections.Array<AudioStream> EntityDeathSound;
}