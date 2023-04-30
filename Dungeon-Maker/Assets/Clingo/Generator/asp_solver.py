

import clingo

import json

def to_list(s,regex):
    lis=[]
    for piece in str(s).split(regex):
        lis=lis+[piece]
    return lis

def create_room_dict(room,size):
    room_dict=dict()
    room=room.replace("room_square(","").replace(")","")
    parts=room.split(",")
    center=dict()
    center["x"]=int(parts[1])
    center["y"]=int(parts[2])
    room_dict["id"]=int(parts[0])
    room_dict["center"]=center
    room_dict["size"]=size
    return room_dict

def create_size_dict(size):
    size_dict=dict()
    size=size.replace("size(","")
    size=size.replace(")","")
    parts=size.split(",")
    size_dict["x"]=int(parts[0])
    size_dict["y"]=int(parts[1])
    return size_dict

def create_door_dict(door):
    door_dict=dict()
    door=door.replace("door(","")
    door=door.replace(")","")
    parts=door.split(",")
    door_dict["start"]=int(parts[0])
    door_dict["end"]=int(parts[1])
    door_dict["orientation"]=parts[2]
    return door_dict

def create_model_dict(model):
    model_list=to_list(model," ")
    size,rooms, doors=divide_list(model_list)
    model_dict=dict()
    model_dict["rooms"]=[]
    model_dict["doors"]=[]
    size_dict=create_size_dict(size)
    for room in rooms:
        room_dict=create_room_dict(room,size_dict)
        model_dict["rooms"]+=[room_dict]
    for door in doors:
        door_dict=create_door_dict(door)
        model_dict["doors"]+=[door_dict]
    return model_dict

def divide_list(lis):
    size=None
    rooms=[]
    doors=[]
    deltas=[]
    for literal in lis:
        parts=literal.split("(")
        if(parts[0]=="size"):
            size=literal
        elif(parts[0]=="room_square"):
            rooms+=[literal]
        elif(parts[0]=="door"):
            doors+=[literal]
        elif (parts[0] == "delta"):
            deltas += [literal]
    return size, rooms, doors

class asp_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, filename,num_iter=1):
        models=[]
        file=open(filename,"r")
        program=file.read()
        num=num_iter*100
        control=clingo.Control(arguments=["--model="+str(num)])
        control.add("base",[], program)
        control.ground([("base",[])])
        handle=control.solve(yield_=True)
        self.status=str(handle.get())
        for model in handle:
            models+=[create_model_dict(model)]


        return models