
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.






%PART FIVE: TRAPS




trap(0..(num_trap-1)).

{trap_pos(T,R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- trap(T), room_size(R,XS,YS,XR,YR), room_pos(R,X,Y), X<XR, Y<YR.

not trap_pos(T,R,X,Y,PX,PY) :- trap(T), room_size(R,XS,YS,XR,YR), room_pos(R,X,Y), room_pos(R,PX,PY), PX=XR/2.

not trap_pos(T,R,X,Y,PX,PY) :- trap(T), room_size(R,XS,YS,XR,YR), room_pos(R,X,Y), room_pos(R,PX,PY), PY=YR/2.

:- room_size(R,XS,YS,XR,YR),{trap_pos(T,R,X,Y,PX,PY)}!=XS.

%:- S=#sum{XS,YS,RS : room_size(RS,XS,YS)}, {trap_pos(T,R,X,Y)}!=@rand(S/2,S).

decoration(D,R,X,Y,PX,PY) :- trap_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.

