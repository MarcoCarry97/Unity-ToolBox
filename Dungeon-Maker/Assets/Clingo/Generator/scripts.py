from clingo.script import Script, register_script
import random as rnd

def place(type,room,x,y):
    nx=x.number
    ny=y.number
    nx+=rnd.randrange(0,5)
    ny+=rnd.randrange(0.5)
    return nx,ny

class place_script(Script):
    def execute(self, location, code: str):
        exec(code,place.__dict__,place.__dict__)

    def call(self,location,name,args):
        return getattr(place,name)(*args)

    def callable(self, name: str) -> bool:
        return name in place.__dict__ and callable(place.__dict__[name])

