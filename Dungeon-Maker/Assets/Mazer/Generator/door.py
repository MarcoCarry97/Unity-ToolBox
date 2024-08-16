
def create_door(start,end,orientation):
    obj=door()
    obj.start=start
    obj.end=end
    obj.orientation=orientation
    return obj

class door:
    def __init__(self):
        self.start=None
        self.end=None
        self.orientation=None

