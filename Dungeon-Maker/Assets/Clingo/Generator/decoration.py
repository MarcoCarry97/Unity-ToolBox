
def create_decoration(type,room,position):
    obj=decoration()
    obj.type=type
    obj.room=room
    obj.position=position
    return obj

class decoration:
    def __init__(self):
        self.type=None
        self.room=None
        self.position=None

