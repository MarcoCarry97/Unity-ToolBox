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



def to_list(s,regex):
    lis=[]
    for piece in str(s).split(regex):
        lis=lis+[piece]
    return lis

def create_room_dict(room,size_list):
    room_dict=dict()
    room=room.replace("place_center(","").replace(")","")
    parts=room.split(",")
    center=dict()
    center["x"]=int(parts[1])
    center["y"]=int(parts[2])
    room_dict["id"]=int(parts[0])
    room_dict["center"]=center
    for size in size_list:
        if(size["room"]==room_dict["id"]):
            size_dict=dict()
            size_dict["x"]=size["x"]
            size_dict["y"]=size["y"]
            room_dict["size"]=size_dict
    return room_dict

def create_size_dict(size_list):
    s_list=[]
    for size in size_list:
        size_dict = dict()
        size = size.replace("place_size(", "")
        size = size.replace(")", "")
        parts = size.split(",")
        size_dict["room"]=int(parts[0])
        size_dict["x"] = int(parts[1])
        size_dict["y"] = int(parts[2])
        s_list+=[size_dict]
    return s_list

def create_door_dict(door):
    door_dict=dict()
    door=door.replace("door(","")
    door=door.replace(")","")
    parts=door.split(",")
    door_dict["start"]=int(parts[0])
    door_dict["end"]=int(parts[1])
    door_dict["orientation"]=parts[2]
    return door_dict

def extract_init_room_id(init_room):
    init_room=init_room.replace("initial_room(","")
    init_room=init_room.replace(")","")
    return int(init_room)

def create_model_dict(model):
    model_list=to_list(model," ")
    size,init_room,rooms, doors=divide_list(model_list)
    model_dict=dict()
    model_dict["rooms"]=[]
    model_dict["doors"]=[]
    size_list=create_size_dict(size)
    init=extract_init_room_id(init_room)
    model_dict["init_room"]=init
    for room in rooms:
        room_dict=create_room_dict(room,size_list)
        model_dict["rooms"]+=[room_dict]
    for door in doors:
        door_dict=create_door_dict(door)
        model_dict["doors"]+=[door_dict]
    return model_dict

def divide_list(lis):
    size=[]
    init_room=None
    rooms=[]
    doors=[]
    deltas=[]
    for literal in lis:
        parts=literal.split("(")
        if(parts[0]=="place_size"):
            size+=[literal]
        elif(parts[0]=="place_center"):
            rooms+=[literal]
        elif(parts[0]=="door"):
            doors+=[literal]
        elif (parts[0] == "delta"):
            deltas += [literal]
        elif(parts[0]=="initial_room"):
            init_room=literal
    return size, init_room, rooms, doors


def to_asp_format(s):
    return s.replace(" ",". ")

def main():
    if(len(sys.argv)<3):
        print("There the need of three filename.lp")
        exit(2)
    gen_room = sys.argv[1]
    gen_corridor = sys.argv[2]
    gen_decoration = sys.argv[3]
    input=""
    file_list=[gen_room,gen_corridor,gen_decoration]
    res=None
    for file_name in file_list:
        input=to_asp_format(input)
        if(input!=""):
            input+="."
        solver=pp.pipeline_solver()
        res=solver.solve(file_name,input,num_iter=1)
        input=res
    #print("Input: "+input)
    dungeon=dict()
    dungeon["status"]="SATISFIED"
    dungeon["levels"]=[create_model_dict(input)]
    sys.stdout.write(json.dumps(dungeon))

# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    main()


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
