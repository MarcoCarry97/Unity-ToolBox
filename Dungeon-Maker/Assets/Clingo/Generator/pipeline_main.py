import os

import clingo

import pipeline_solver as pp
import sys
import random
import json

def main():
    if(len(sys.argv)<3):
        print("Please enter the name of a .lp file");
        sys.exit(2)
    elif len(sys.argv)==4:
        num_samples=int(sys.argv[3])
    else: num_samples=1
    filename=sys.argv[1]
    input=sys.argv[2].replace("[","").replace("]","")
    solver=pp.pipeline_solver()
    res=solver.solve(filename,input,num_iter=num_samples)
    sys.stdout.write(res)

# Press the green button in the gutter to run the script.
if __name__ == '__main__':
    main()


# See PyCharm help at https://www.jetbrains.com/help/pycharm/
