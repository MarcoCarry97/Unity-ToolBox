
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS
key(0..(num_treasure-1)).

{key_pos(T,R,X,Y,X,Y)} :- key(T), room(R), not blind_room(R), place_size(R,XS,YS,XR,YR), room_pos(R,X,Y).

:- key_pos(T1,R,X,Y,_,_), key_pos(T2,R,X,Y,_,_), T1!=T2.

:- normal_room(R), not blind_room(R), room_size(R,_,_,XS,YS), {key_pos(T,R,X,Y,TX,TY)}!=1.

decoration(T,R,X,Y,TX,TY) :- key_pos(T,R,X,Y,TX,TY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.



%:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


