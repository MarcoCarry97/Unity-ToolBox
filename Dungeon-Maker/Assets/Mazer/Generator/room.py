
def create_pair(x,y):
    obj=pair()
    obj.x=x
    obj.y=y
    return obj

class pair:
    def __init__(self):
        self.x=0
        self.y=0

def create_room(id,center,size):
    obj=room()
    obj.id=id
    obj.center=center
    obj.size=size
    return obj

class room:
    def __init__(self):
        self.id=None
        self.center=None
        self.size=None