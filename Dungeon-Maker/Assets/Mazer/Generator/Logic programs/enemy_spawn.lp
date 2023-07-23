


%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.


enemy(0..(num_enemy-1)).

{enemy_pos(T,R,X,Y,X,Y)} :- enemy(T), place_center(R,_,_,_,_), expand_pos(_,R,X,Y).

:- enemy_pos(T1,R,X,Y,_,_), enemy_pos(T2,R,X,Y,_,_), T1!=T2.

:- blind_room(R), room_size(R,_,_,XS,YS), {enemy_pos(T,R,X,Y,TX,TY)}!=1.

decoration(T,R,X,Y,TX,TY) :- enemy_pos(T,R,X,Y,TX,TY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.