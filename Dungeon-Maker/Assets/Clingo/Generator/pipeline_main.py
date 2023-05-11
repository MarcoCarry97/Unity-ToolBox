import os

import clingo

import pipeline_solver as pp
import sys
import random
import json


gen_room="D://Progetti/Dungeon-Maker/Dungeon-Maker/Assets/Clingo/gen_room.lp"
gen_corr="D://Progetti/Dungeon-Maker/Dungeon-Maker/Assets/Clingo/gen_corridor.lp"
place_decoration="D://Progetti/Dungeon-Maker/Dungeon-Maker/Assets/Clingo/place_decoration.lp"
to_squares="D://Progetti/Dungeon-Maker/Dungeon-Maker/Assets/Clingo/to_squares.lp"

file_list=[gen_room]

def to_asp_format(s):
    return s.replace(" ",".\n")

def main():
    input=""
    for file_name in file_list:
        input=to_asp_format(input)
        if(input!=""):
            input+="."
        solver=pp.pipeline_solver()
        input=solver.solve(file_name,input,num_iter=1)
    sys.stdout.write(input)

# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    main()


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
