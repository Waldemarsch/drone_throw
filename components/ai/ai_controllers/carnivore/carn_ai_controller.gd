extends AIController

class_name CarnivoreAIController


func select_state(bodies: Array[Node2D]) -> Array:
	if len(bodies):
		var prey : CharacterBody2D = null
		for body in bodies:
			if body.is_in_group("herbivores") and !prey:
				prey = body
			elif body.is_in_group("carnivores"):
				return [FleeState, {"body": body}]
		if prey:
			return [ChaseState, {"prey": prey}]
	return [WanderState, null]
