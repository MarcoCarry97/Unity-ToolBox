
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS

treasure(0..(num_treasure-1)).

{treasure_pos(T,R,X,Y)} :- treasure(T), room_pos(R,X,Y).

:- treasure_pos(T1,R,X,Y), treasure_pos(T2,R,X,Y),T1!=T2.

:- room(R), #count{T,X,Y : treasure_pos(T,R,X,Y)}!=num_treasure.



