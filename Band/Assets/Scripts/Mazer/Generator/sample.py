import random

import clingo

class Context:
    def rand(self,a,limit):
        na=a.number
        nlimit=limit.number
        rand_num=random.randrange(0,nlimit)
        return clingo.Number(na+rand_num)

file = open("C:/Users/marco/Desktop/sample.lp")
program = file.read()
args=["--model=0"]
control = clingo.Control(arguments=args)
control.add("base", [], program)

control.ground([("base", [])],context=Context())
handle=control.solve(yield_=True)

for model in handle:
    print(model)