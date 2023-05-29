
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.



%PART THREE: DIMENSIONS.

size(X,Y) :- X=min_size..max_size, Y=min_size..max_size.

room_size(R,X,Y) :- room(R), size(X,Y).

:-room(R), #count{X,Y : room_size(R,X,Y)}!=1.



corr_size(C,1,1) :- corridor(C).





place_size(P,X,Y) :- room_size(P,X,Y).

place_size(P,X,Y) :- corr_size(P,X,Y).





