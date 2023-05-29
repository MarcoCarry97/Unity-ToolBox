
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.



pos(0,0).

pos(X,Y) :- size(A,B), X=(-A/2)..(A/2), Y=(-B/2)..(B/2).

room_pos(R,X,Y) :- room(R), pos(X,Y).





