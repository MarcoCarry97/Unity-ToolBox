import argparse
import json

import sys

import maker_solver as ms

import os

def get_parser():
    parser = argparse.ArgumentParser(
        formatter_class=argparse.ArgumentDefaultsHelpFormatter,
        )
    parser.add_argument("--rooms","-r",type=int,default=3)
    parser.add_argument("--size","-s",type=int,default=3)
    parser.add_argument("--distance","-d",type=int,default=5)
    parser.add_argument("--levels","-l",type=int,default=1)
    parser.add_argument("--path","-p",type=int,default=1)
    parser.add_argument("--space", "-a", type=int, default=1)
    parser.add_argument("--num_trap", "-t", type=int, default=1)
    parser.add_argument("--num_treasure", "-c", type=int, default=1)
    parser.add_argument("--num_item", "-i", type=int, default=1)
    parser.add_argument("--rand_init", "-b", action="store_true")
    return parser
def main():
    parser=get_parser()
    args=parser.parse_args()
    num_levels=args.levels
    num_rooms=args.rooms
    distance=args.distance
    max_path=args.path
    size=args.size
    space=args.space
    num_trap=args.num_trap
    num_treasure=args.num_treasure
    num_item=args.num_item
    rand_init=args.rand_init
    solver=ms.maker_solver()
    res=solver.solve(num_levels,num_rooms,size,distance,max_path,space,num_trap,num_treasure,num_item,rand_init)
    dungeon=dict()
    dungeon["status"]=solver.status
    dungeon["levels"]=res
    json_str=json.dumps(dungeon)
    
    sys.stdout.write(json_str)

if __name__ == '__main__':
    main()