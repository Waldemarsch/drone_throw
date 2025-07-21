extends AIController

class_name HerbivoreAIController


func select_state(bodies: Array[Node2D]) -> Array:
	if len(bodies):
		for body in bodies:
			if body.is_in_group("carnivores"):
				return [FleeState, {"body": body}]
	return [WanderState, null]
