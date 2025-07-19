extends CharacterBody2D

class_name Player

const SPEED = 100.0


@onready var anim_player = $AnimatedSprite2D

@export var evolution_progress : float = 0
@export var evolution_size_growth_factor : float = 0.1
@export var evolution_stage : int
var evolution_stage_sizes_minmax : Dictionary = {
	1: {"min": Vector2(1, 1), "max": Vector2(2, 2)},
}


func _ready() -> void:
	#GameManager.set_player(self)
	pass


func _process(delta):
	pass
	
	
func _physics_process(delta: float) -> void:
	move_player()

func evolution_progress_update(nutrition_value) -> void:
	evolution_progress = evolution_progress + nutrition_value

func evolution_scale_size(evol_stage) -> void:
	self.scale = evolution_stage_sizes_minmax[evol_stage]["min"] + (evolution_stage_sizes_minmax[evol_stage]["max"] - evolution_stage_sizes_minmax[evol_stage]["min"]) * evolution_progress / 100 * evolution_size_growth_factor
	
	
func move_player():
	velocity = Vector2(Input.get_axis("left", "right"), Input.get_axis("up", "down")).normalized() * SPEED
	if velocity.length() != 0:
		anim_player.play("moving")
		move_and_slide()
	else:
		anim_player.play("idle")
		
	
