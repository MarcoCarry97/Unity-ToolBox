import os

import clingo

import asp_solver as asp
import sys
import random
import json

res_model=None

def on_model(model):
    print(model)

def random_samples(l,num_samples):
    res=[]
    for i in range(0,num_samples):
        num=int(random.random())%len(l)
        res+=[l[num]]
    return res

def main():
    if(len(sys.argv)<2):
        print("Please enter the name of a .lp file");
        sys.exit(2)
    elif len(sys.argv)==3:
        num_samples=int(sys.argv[2])
    else: num_samples=1
    filename=sys.argv[1]
    solver=asp.asp_solver()
    res=random_samples(solver.solve(filename,num_iter=num_samples),num_samples)
    dictio=dict()
    dictio["status"]=solver.status
    dictio["levels"]=res
    sys.stdout.write(json.dumps(dictio))


# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    main()


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
