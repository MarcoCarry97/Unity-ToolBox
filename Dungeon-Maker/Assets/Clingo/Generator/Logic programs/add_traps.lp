
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.






%PART FIVE: TRAPS




trap(0..(num_trap-1)).

{trap_pos(T,R,X,Y,@rand(0,XS-1),@rand(0,YS-1))} :- trap(T), room_size(R,XS,YS,XR,YR), room_pos(R,X,Y), X<XS, Y<YS.

not trap_pos(T,R,X,Y,PX,PY) :- trap(T), room_size(R,A,B,XR,YR), room_pos(R,X,Y), room_pos(R,PX,PY), X=XR/2.

not trap_pos(T,R,X,Y,PX,PY) :- trap(T), room_size(R,A,B,XR,YR), room_pos(R,X,Y), room_pos(R,PX,PY), Y=YR/2.

:- room_size(R,XS,YS,XR,YR),{trap_pos(T,R,X,Y,PX,PY)}!=XS.

%:- S=#sum{XS,YS,RS : room_size(RS,XS,YS)}, {trap_pos(T,R,X,Y)}!=@rand(S/2,S).

decoration(D,R,X,Y,PX,PY) :- trap_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,X,Y,PX,PY), decoration(D2,R,X,Y,PX,PY), D1!=D2.

