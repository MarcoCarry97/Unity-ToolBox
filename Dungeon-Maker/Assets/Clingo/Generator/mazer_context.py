import random
import clingo

class mazer_context:

    def __init__(self):
        self.memory=dict()

    def rand(self,offset,limit):
        if((offset,limit) not in self.memory.keys()):
            rand_num = random.randrange(offset.number, limit.number)
            self.memory[(offset, limit)] = rand_num
        return clingo.Number(self.memory[(offset,limit)])

    def rand_num(self,limit):
        return self.rand(clingo.Number(0),limit)

    def rand_path_length(self,limit):
        return self.rand(clingo.Number(1),limit)

    def rand_tile_in_room(self,length):
        if((length) not in self.memory.keys()):
            tiles = list(range(0, length.number))
            half = int(length.number / 2)
            if (half in tiles):
                tiles.remove(half)
            rand_choice = random.randrange(0, len(tiles))
            self.memory[(length)] = tiles[rand_choice]
        return clingo.Number(self.memory[(length)])

    def rand_neighbor(self,room,coord,dist):
        ncoord=coord.number
        ndist=dist.number
        lis=[ncoord,ncoord+ndist,ncoord-ndist]
        return clingo.Number(random.randrange(0,len(lis)))