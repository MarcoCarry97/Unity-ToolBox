import math
import random



import clingo

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
    num = int(math.exp(num))%length
    rand_exp=random.randrange(0, length)
    num= (num+num**rand_exp)%length
    return num

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

def single_model_solving(input,filename,num_levels,num_rooms, size, distance,path,rand_init):
    input=to_asp_format(input)
    file = open(filename)
    program = input+file.read()
    control = clingo.Control(arguments=["--model="+str(num_levels*100)])
    control.add("base", [], program)
    control.ground([("base", [])])
    handle=control.solve(yield_=True)
    models=to_model_list(handle)
    res=get_rand_models(models,num_levels)
    return res, handle.get()

def to_asp_format(s):
    s=str(s)
    if(s!=""):
        s+=" "
    return s.replace(" ",". ")

class maker_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, room_file, corr_file, dec_file, num_levels, num_rooms, size, distance,path,rand_init):
        incomplete_models,status=single_model_solving("",room_file,num_levels,num_rooms,size,distance,path,rand_init)
        results=[]
        for incomplete_model in incomplete_models:
            input,status=single_model_solving(incomplete_model,corr_file,1,num_rooms, size, distance,path,rand_init)
            model, status=single_model_solving(input,dec_file,1,num_rooms, size, distance,path,rand_init)
            results+=[create_model_dict(model)]
        self.status = str(status)
        return results