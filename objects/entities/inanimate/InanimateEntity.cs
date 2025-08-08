using Godot;
using System;

namespace DroneThrow 
{
    public partial class InanimateEntity : Entity
    {
        private Sprite2D SpriteNode;


        public override void _Ready()
        {
            base._Ready();

            SpriteNode = GetNode<Sprite2D>("Sprite2D");

            SpriteNode.Texture = EntityDataResource.EntityTexture;
        }


    }       
}
