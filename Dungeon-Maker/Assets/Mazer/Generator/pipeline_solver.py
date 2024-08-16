import random

import clingo

class pipeline_solver:

    def __init__(self):
        self.status="UNKNOWN"

    def solve(self, filename, input, num_iter=1):
        models=[]
        file=open(filename,"r")
        program=input+file.read()
        num=num_iter*1000
        control=clingo.Control(arguments=["--model="+str(num)])
        control.add("base",[], program)
        control.ground([("base",[])])
        handle=control.solve(yield_=True)
        self.status=str(handle.get())
        res=""
        model_list=[]
        for model in handle:
            model_list+=[model]
        res=str(model_list[random.randrange(0,len(model_list)-1)])
        return res
