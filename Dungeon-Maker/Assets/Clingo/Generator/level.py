
def create_level(rooms,doors,initial_room):
    obj=level()
    obj.rooms=rooms
    obj.doors=doors
    obj.initial_room=initial_room
    return obj

class level:

    def __init__(self):
        self.rooms=[]
        self.doors=[]
        self.initial_room=None

    def get_doors_of_room(self,room):
        res=[]
        for door in self.doors:
            if(door.start==room.id):
                res+=[door]
        return res

    def get_decorations_of_room(self,room):
        res = []
        for door in self.doors:
            if (door.start == room.id):
                res += [door]
        return res
