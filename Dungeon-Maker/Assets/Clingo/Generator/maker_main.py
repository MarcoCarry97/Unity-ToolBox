import argparse
import json

import sys

import maker_solver as ms

def get_parser():
    parser = argparse.ArgumentParser(
        formatter_class=argparse.ArgumentDefaultsHelpFormatter,
        )
    parser.add_argument("--room_file","-rf",type=str,default="")
    parser.add_argument("--corr_file","-cf",type=str,default="")
    parser.add_argument("--dec_file","-df",type=str,default="")
    parser.add_argument("--rooms","-r",type=int,default=3)
    parser.add_argument("--size","-s",type=int,default=3)
    parser.add_argument("--distance","-d",type=int,default=5)
    parser.add_argument("--levels","-l",type=int,default=1)
    parser.add_argument("--path","-p",type=int,default=2)
    parser.add_argument("--rand_init", "-i", action="store_true")
    return parser
def main():
    parser=get_parser()
    args=parser.parse_args()
    num_levels=args.levels
    num_rooms=args.rooms
    distance=args.distance
    max_path=args.path
    size=args.size
    room_file=args.room_file
    corr_file=args.corr_file
    dec_file=args.dec_file
    rand_init=args.rand_init
    solver=ms.maker_solver()
    res=solver.solve(room_file,corr_file,dec_file,num_levels,num_rooms,size,distance,max_path,rand_init)
    dungeon=dict()
    dungeon["status"]=solver.status
    dungeon["levels"]=res
    sys.stdout.write(json.dumps(dungeon))

if __name__ == '__main__':
    main()