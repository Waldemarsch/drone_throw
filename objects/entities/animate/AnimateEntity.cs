using Godot;
using System;

namespace DroneThrow 
{
    public partial class AnimateEntity : Entity
    {
        private AnimatedSprite2D AnimatedSpriteNode;


        public override void _Ready()
        {
            base._Ready();

            AnimatedSpriteNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

            AnimatedSpriteNode.SpriteFrames = EntityDataResource.SpriteFramesRes;

            AnimatedSpriteNode.Play("default");
        }


    }       
}
