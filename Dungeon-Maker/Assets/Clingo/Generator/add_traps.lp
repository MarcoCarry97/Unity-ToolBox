
%problem instance

#const max=num_rooms*2.

#const min_size=3.



#const init_pos_x=0.

#const init_pos_y=0.






%PART FIVE: TRAPS




trap(0..(num_trap-1)).

{trap_pos(T,R,X,Y)} :- trap(T), room_size(R,XS,YS), room_pos(R,X,Y), X<XS, Y<YS.

not trap_pos(T,R,X,Y) :- trap(T), room_size(R,A,B), room_pos(R,X,Y), X=A/2.

not trap_pos(T,R,X,Y) :- trap(T), room_size(R,A,B), room_pos(R,X,Y), Y=B/2.

:- room_size(R,XS,YS),{trap_pos(T,R,X,Y)}!=XS.

decoration(D,R,X,Y) :- trap_pos(D,R,X,Y).

:-decoration(D1,R,X,Y), decoration(D2,R,X,Y), D1!=D2.

