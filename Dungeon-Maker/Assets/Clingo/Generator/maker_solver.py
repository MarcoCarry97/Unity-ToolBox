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

def get_distant_models(models,input,size,previous=None):
    res=[]
    best_model=None
    for i in range(0,size):
        best_model = count_distance(models,previous=previous)
        if (best_model != None):
            models.remove(best_model)
        res += [best_model]
    if size==1:
        return res[0]
    else:
        return res

def to_asp_list(sample):
    return to_list(to_asp_format(sample), ".")

def not_in_previous(literal, previous):
    for prev in previous:
        prev_list=to_asp_list(prev)
        if(literal in prev_list):
            return 0
    return 1



def count_distance(models,previous=None):
    if(previous==None):
        return get_rand_models(models,1)
    elif not isinstance(previous,list):
        previous=[previous]
    sample=get_rand_models(models,1)
    sample_list=to_asp_list(sample)
    best=sample
    best_count=0
    for model in models:
        model_list=to_asp_list(model)
        count=0
        for literal in model_list:
            count+=not_in_previous(literal,previous)
        if best_count<count:
            best_count=count
            best=model
    return best


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

def single_model_solving(input,filename,num_levels,num_rooms, size, distance,path,rand_init, previous=None):
    input=to_asp_format(input)
    file = open(filename)
    program = input+file.read()
    args=["--model="+str(num_levels*100),
          "-c num_rooms="+str(num_rooms),
          "-c max_size="+str(size),
          "-c max_path="+str(path),
          "-c distance="+str(distance)]
    control = clingo.Control(arguments=args)
    control.add("base", [], program)
    control.ground([("base", [])])
    handle=control.solve(yield_=True)
    models=to_model_list(handle)
    #res=get_rand_models(models,num_levels)
    res=get_distant_models(models,input,num_levels, previous=previous)
    return res, handle.get(), previous


def to_asp_format(s):
    s=str(s)
    if(s!=""):
        s+=" "
    s=s.replace(" ",". ")
    s=s.replace("..",".")
    return s

class maker_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, num_levels, num_rooms, size, distance,path,rand_init):
        create_points="create_points.lp"
        files=["create_rooms.lp","create_doors.lp","assign_size.lp","add_traps.lp", "add_treasures.lp","add_keys.lp", "add_items.lp", "initial_end.lp","add_stairs.lp"]
        incomplete_model=None
        incomplete_models=[]
        for i in range(0,num_levels):
            incomplete_model, status, _ = single_model_solving("", create_points, 1, num_rooms, size,
                                                                distance, path, rand_init,previous=incomplete_models)
            incomplete_models+=[incomplete_model]
        results=[]
        previous=None
        for model in incomplete_models:
            input=model
            previous=None
            for file in files:
                #print(file)
                input, status, _= single_model_solving(input, file, 1, num_rooms, size, distance, path,rand_init, previous=previous)
            previous=input
            results+=[create_model_dict(input)]
        self.status = str(status)
        return results