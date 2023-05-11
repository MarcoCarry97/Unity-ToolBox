

import clingo

class pipeline_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, filename, input, num_iter=1):
        models=[]
        file=open(filename,"r")
        program=input+"\n"+file.read()
        num=num_iter*100
        control=clingo.Control(arguments=["--model="+str(num)])
        control.add("base",[], program)
        control.ground([("base",[])])
        handle=control.solve(yield_=True)
        self.status=str(handle.get())
        res=""
        for model in handle:
            res+=str(model)
            break
        return res