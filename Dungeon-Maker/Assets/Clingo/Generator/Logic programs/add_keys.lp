
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS
key(0..(num_treasure-1)).

{key_pos(T,R,X,Y,@rand(0,XS-1),@rand(0,YS-1))} :- key(T), room_pos(R,X,Y), room_size(R,A,B,XS,YS).

decoration(D,R,X,Y,PX,PY) :- key_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,X,Y,PX,PY), decoration(D2,R,X,Y,PX,PY), D1!=D2.




%:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


