import math
import random



import clingo

import scripts as s

from clingo.script import register_script

from mazer_context import mazer_context

import method_utils








class maker_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init):
        create_points="create_points.lp"
        files=["create_rooms.lp","create_doors.lp","assign_size.lp","add_traps.lp", "add_treasures.lp","add_keys.lp", "add_items.lp", "initial_end.lp","add_stairs.lp"]
        incomplete_model=None
        incomplete_models=[]
        for i in range(0,num_levels):
            incomplete_model, status, _ = method_utils.single_model_solving("", create_points, 1, num_rooms, size,
                                                                distance, path,space,num_trap, num_treasure, num_item, rand_init,previous=incomplete_models)
            incomplete_models+=[incomplete_model]
        results=[]
        previous=None
        for model in incomplete_models:
            input=model
            previous=None
            for file in files:
                #print(file)
                #print(input)
                input, status, _ = method_utils.single_model_solving(input, file, 1, num_rooms, size, distance, path,space, num_trap, num_treasure, num_item,rand_init, previous=previous)
            previous=input
            results+=[method_utils.create_model_dict(input)]
        self.status = str(status)
        return results