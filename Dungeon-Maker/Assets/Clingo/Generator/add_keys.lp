
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS
key(0..(num_treasure-1)).

{key_pos(T,R,X,Y)} :- key(T), room_pos(R,X,Y).

decoration(D,R,X,Y) :- key_pos(D,R,X,Y).

:-decoration(D1,R,X,Y), decoration(D2,R,X,Y), D1!=D2.




%:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


