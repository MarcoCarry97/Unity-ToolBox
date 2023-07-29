import math
import random



import clingo

import scripts as s

from clingo.script import register_script

from mazer_context import mazer_context

import method_utils

import time







class maker_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init,corr_size,num_enemy):
       points=method_utils.get_models("","create_points.lp", num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init,corr_size,num_enemy)
       beginning_files=["assign_size.lp","rooms_doors.lp"]
       incomplete_models=method_utils.get_models_from_more_files(points,beginning_files, num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init,corr_size,num_enemy)
       last_files=["positions.lp", "expansion.lp","expand_pos.lp","initial_end.lp", "add_stairs.lp", "add_traps.lp","add_treasures.lp", "add_keys.lp","add_items.lp","enemy_spawn.lp"]
       res=method_utils.get_models_from_more_files(incomplete_models,last_files, 1, num_rooms, size, distance,path,1,num_trap,num_treasure,num_item,rand_init,corr_size,num_enemy)
       dicts=[]
       for model in res:
           dicts+=[method_utils.create_model_dict(model)]
       return dicts

class timed_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, num_levels, num_rooms, size, distance,path,space,num_trap,num_treasure,num_item,rand_init,corr_size,num_enemy):
        create_points="create_points.lp"
        #files=["assign_size.lp","create_rooms.lp","create_doors.lp","autocomplete.lp","add_traps.lp", "add_treasures.lp","add_keys.lp", "add_items.lp", "initial_end.lp","add_stairs.lp"]
        #files=["assign_size.lp","create_rooms.lp","create_doors.lp","autocomplete.lp","initial_end.lp"]
        files=["assign_size.lp","rooms_doors.lp", "initial_end.lp"]
        incomplete_models=[]

        start=time.time()
        for i in range(0,num_levels):
            incomplete_model, status, _ = method_utils.single_model_solving("", create_points, 1, num_rooms, size,distance, path,space,num_trap, num_treasure, num_item, rand_init,corr_size,num_enemy,previous=incomplete_models)

            incomplete_models+=[incomplete_model]
        end=time.time()
        print("Time of "+create_points+": "+str(end-start)+" s\n")

        results=[]
        previouses=[]

        for model in incomplete_models:
            input=model
            for file in files:
                start = time.time()
                print("File: "+file)
                #print("Input: "+input)
                input, status, _ = method_utils.single_model_solving(input, file, 1, num_rooms, size, distance, path,1, num_trap, num_treasure, num_item,rand_init,corr_size,num_enemy, previous=previouses)
                end = time.time()
                print("Time of " + file + ": " + str(end - start) + " s")
            print("\n")
            previouses+=[input]
            model_dict=method_utils.create_model_dict(input)
            results+=[model_dict]
        self.status = str(status)
        return results