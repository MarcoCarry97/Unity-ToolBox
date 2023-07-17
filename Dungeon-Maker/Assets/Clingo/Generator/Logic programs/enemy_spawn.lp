


%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.


enemy(0..(num_enemy-1)).


{enemy_pos(T,R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- enemy(T), room_pos(R,X,Y), place_size(R,XS,YS,XR,YR), X<XR, Y<YR.


:-place_center(R,XS,YS,XR,YR), {enemy_pos(E,R,X,Y,_,_)}!=1.

decoration(D,R,X,Y,PX,PY) :- enemy_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.