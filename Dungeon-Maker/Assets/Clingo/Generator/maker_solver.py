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

    def solve(self, num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init,corr_size):
        create_points="create_points.lp"
        files=["create_rooms.lp","create_doors.lp","assign_size.lp","add_traps.lp", "add_treasures.lp","add_keys.lp", "add_items.lp", "initial_end.lp","add_stairs.lp"]
        incomplete_models=[]
        for i in range(0,num_levels):
            incomplete_model, status, _ = method_utils.single_model_solving("", create_points, 1, num_rooms, size,
                                                                distance, path,space,num_trap, num_treasure, num_item, rand_init,corr_size,previous=incomplete_models)
            incomplete_models+=[incomplete_model]

        results=[]
        previouses=[]
        for model in incomplete_models:
            input=model
            for file in files:
                #print("File: "+file)
                #print("Input: "+input)
                input, status, _ = method_utils.single_model_solving(input, file, 1, num_rooms, size, distance, path,space, num_trap, num_treasure, num_item,rand_init,corr_size, previous=previouses)
            previouses+=[input]
            model_dict=method_utils.create_model_dict(input)
            results+=[model_dict]
        self.status = str(status)
        return results