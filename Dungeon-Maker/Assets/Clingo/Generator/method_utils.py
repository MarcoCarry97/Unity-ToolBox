
import clingo

import scripts as s

from clingo.script import register_script

from mazer_context import mazer_context

import random

import clingo

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

def create_decoration_dict(dec_list, type):
    res=[]
    for dec in dec_list:
        dec=dec.replace(type+"_pos(","")
        dec=dec.replace(")","")
        parts=dec.split(",")
        dec_dict=dict()
        dec_dict["type"]=type
        dec_dict["index"]=int(parts[0])
        dec_dict["room"]=int(parts[1])
        pos_dict=dict()
        pos_dict["x"]=int(parts[2])
        pos_dict["y"]=int(parts[3])
        dec_dict["position"]=pos_dict
        res+=[dec_dict]
    return res

def create_stairs_dict(stairs):
    stairs_dict=dict()
    stairs=stairs.replace("stairs(","")
    stairs=stairs.replace(")","")
    parts=stairs.split(",")
    pos_dict = dict()
    pos_dict["x"] = int(parts[1])
    pos_dict["y"] = int(parts[2])
    stairs_dict["room"]=int(parts[0])
    stairs_dict["position"]=pos_dict
    return stairs_dict

def extract_init_room_id(init_room):
    init_room=init_room.replace("initial_room(","")
    init_room=init_room.replace(")","")
    return int(init_room)

def create_model_dict(model):
    model_list=to_asp_list(model)
    size,init_room,rooms, doors, traps, treasures,keys, items, stairs=divide_list(model_list)
    model_dict=dict()
    model_dict["rooms"]=[]
    model_dict["doors"]=[]
    size_list=create_size_dict(size)
    init=None
    if(init_room!=None):
        init=extract_init_room_id(init_room)
    model_dict["init_room"]=init
    model_dict["decorations"]=create_decoration_dict(traps,"trap")
    model_dict["decorations"]+=create_decoration_dict(treasures,"treasure")
    model_dict["decorations"]+=create_decoration_dict(keys,"key")
    model_dict["decorations"]+=create_decoration_dict(items,"item")
    if(stairs!=None):
        model_dict["stairs"]=create_stairs_dict(stairs)
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
    traps=[]
    treasures=[]
    items=[]
    stairs=None
    keys=[]
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
        elif (parts[0] == "trap_pos"):
            traps+= [literal]
        elif (parts[0] == "treasure_pos"):
            treasures+= [literal]
        elif (parts[0] == "key_pos"):
            keys+= [literal]
        elif (parts[0] == "item_pos"):
            items = [literal]
        elif (parts[0] == "stairs"):
            stairs = literal
    return size, init_room, rooms, doors,traps,treasures, keys,items,stairs

def single_model_solving(input,filename,num_levels,num_rooms, size, distance,path,space,num_trap, num_treasure, num_item,rand_init,corr_size, previous=None):
    input=to_asp_format(input)
    file = open("Logic programs/"+filename)
    program = input+file.read()
    args=["--model="+str(num_levels*space),
          "-c num_rooms="+str(num_rooms),
          "-c max_size="+str(size),
          "-c max_path="+str(path),
          "-c distance="+str(distance),
          "-c num_trap="+str(num_trap),
          "-c num_treasure="+str(num_treasure),
          "-c num_item="+str(num_item),
          "-c corr_dim="+str(corr_size)]
    control = clingo.Control(arguments=args)
    control.add("base", [], program)
    context=mazer_context()
    control.ground([("base", [])], context=context)
    handle=control.solve(yield_=True)
    models=to_model_list(handle)
    #res=get_rand_models(models,num_levels)
    res=get_distant_models(models,previous,num_levels,filename)
    return res, handle.get(), previous


def to_asp_format(s):
    s=str(s)
    s=s.replace(" ",".")
    if(s!=""):
        s+="."
    return s

def to_asp_list(sample):
    return to_list(to_asp_format(sample), ".")

def not_in_previous(literal, previous):
    for prev in previous:
        prev_list=to_asp_list(prev)
        if(literal in prev_list):
            return 0
    return 1

def to_model_list(handle):
    models=[]
    for model in handle:
        models+=[str(model)]
    return models

def random_formula(num,length):
    multiplier1 = random.randrange(0, length)
    addend1 = random.randrange(0, length)
    multiplier2 = random.randrange(0, length)
    addend2 = random.randrange(0, length)
    num = (num + (1 + addend1) * multiplier1 + (1 + addend2) * multiplier2 + 1) % length
    rand_exp=random.randrange(0, length)
    num= (num+num**rand_exp)%length
    return num

def convert_to_string(model):
    s=""
    for i in range(0,len(model)):
        s+=str(model[i])
        if(i<len(model)-1):
            s+=" "
    return s

def to_list(s,regex):
    lis=[]
    for piece in str(s).split(regex):
        lis=lis+[piece]
    return lis

def count_distance(current_level, previous):
    current_list=to_asp_list(current_level)
    previous_list=to_asp_list(previous)
    distance=0
    for cur_lit in current_list:
        if cur_lit not in previous_list:
            distance+=1
    return distance

def center_distance(current_level, previous):
    current_dict=create_model_dict(current_level)
    previous_dict=create_model_dict(previous)
    distance=0
    for cur_room in current_dict["rooms"]:
        for prev_room in previous_dict["rooms"]:
            if cur_room["id"]==prev_room["id"]:
                distx=abs(cur_room["center"]["x"]-prev_room["center"]["x"])
                disty=abs(cur_room["center"]["y"]-prev_room["center"]["y"])
                distance+=distx+disty
    return distance

def size_distance(current_level, previous):
    current_dict=create_model_dict(current_level)
    previous_dict=create_model_dict(previous)
    distance=0
    for cur_room in current_dict["rooms"]:
        for prev_room in previous_dict["rooms"]:
            if cur_room["id"]==prev_room["id"]:
                distx=abs(cur_room["size"]["x"]-prev_room["center"]["x"])
                disty=abs(cur_room["size"]["y"]-prev_room["center"]["y"])
                distance+=distx+disty
    return distance

def door_distance(current_level,previous):
    current_dict=create_model_dict(current_level)
    previous_dict=create_model_dict(previous)
    current_distance=len(current_dict["doors"])
    previous_distance=len(previous_dict["doors"])
    return abs(current_distance-previous_distance)


def get_measure_functions(file):
    lis = [count_distance]
    if file=="create_rooms.lp":
        lis=[center_distance]
    if file=="assign_size.lp":
        lis=[size_distance]
    if file=="create_doors.lp":
        lis=[door_distance]
    return lis


def get_rand_models(models,size):
    res=[]
    num=0
    for i in range(0,size):
        num=random_formula(num,len(models))
        res+=[models[num]]
    if size==1:
        return res[0]
    else:
        return res


def get_distant_models(models, previouses,size, file):
    res=[]
    measurements=get_measure_functions(file)
    for i in range(0,size):
        if(len(previouses)==0):
            res+=[get_rand_models(models,1)]
        else:
            for model in models:
                distance=0
                best=get_rand_models(models,1)
                for prev in previouses:
                    dist=0
                    for measurement in measurements:
                        dist+=measurement(model,prev)/len(previouses)
                if(distance<dist):
                    best=model
                    distance=dist
            if(best!=None):
                res += [best]
                models.remove(best)
    if(size==1):
        return res[0]
    else:
        return res

