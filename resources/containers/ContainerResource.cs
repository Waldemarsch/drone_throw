using Godot;
using System;

[GlobalClass]
public partial class ContainerResource : Resource
{
    [Export] public Godot.Collections.Dictionary Paths;
}
