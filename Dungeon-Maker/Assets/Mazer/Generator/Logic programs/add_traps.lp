
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.






%PART FIVE: TRAPS


normal_room(R) :- room(R), not initial_room(R), not end_room(R).


trap(0..(num_trap-1)).

{trap_pos(T,R,X,Y,X,Y)} :- trap(T), normal_room(R), expand_pos(_,R,X,Y).

:- trap_pos(T1,R,X,Y,_,_), trap_pos(T2,R,X,Y,_,_), T1!=T2.

:- normal_room(R), room_size(R,_,_,XS,YS), {trap_pos(T,R,X,Y,TX,TY)}!=(XS+YS)/2.

decoration(T,R,X,Y,TX,TY) :- trap_pos(T,R,X,Y,TX,TY).