
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART SIX: TREASURES & KEYS
key(0..(num_treasure-1)).

{key_pos(T,R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- key(T), room_pos(R,X,Y), room_size(R,XS,YS,XR,YR), X<XR, Y<YR.

decoration(D,R,X,Y,PX,PY) :- key_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,_,_,PX,PY), decoration(D2,R,_,_,PX,PY), D1!=D2.




%:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


