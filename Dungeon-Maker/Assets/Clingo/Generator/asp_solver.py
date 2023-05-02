

import clingo

def to_list(s,regex):
    lis=[]
    for piece in str(s).split(regex):
        lis=lis+[piece]
    return lis

def create_room_dict(room,size_list):
    room_dict=dict()
    room=room.replace("room_square(","").replace(")","")
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
        size = size.replace("room_size(", "")
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
    model_dict["init_door"]=init
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
        if(parts[0]=="room_size"):
            size+=[literal]
        elif(parts[0]=="room_square"):
            rooms+=[literal]
        elif(parts[0]=="door"):
            doors+=[literal]
        elif (parts[0] == "delta"):
            deltas += [literal]
        elif(parts[0]=="initial_room"):
            init_room=literal
    return size, init_room, rooms, doors

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