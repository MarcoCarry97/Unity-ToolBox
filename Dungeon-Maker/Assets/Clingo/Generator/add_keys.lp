
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS
key(0..(num_treasure-1)).

{key_pos(T,R,X,Y)} :- key(T), room_pos(R,X,Y).

:- key_pos(T1,R,X,Y), key_pos(T2,R,X,Y),T1!=T2.

:- room(R), #count{T,X,Y : key_pos(T,R,X,Y)}!=num_treasure.




%:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


