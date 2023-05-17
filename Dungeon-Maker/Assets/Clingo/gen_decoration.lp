%problem instance

%variables

#const num_rooms=7.

#const max=num_rooms*2.

#const min_size=3.

#const max_size=3.

#const distance=5.













%PART THREE

#const num_trap=5.

trap(0..(num_trap-1)).



{trap_pos(T,R,X,Y)} :- trap(T), room_center(R,XC,YC), room_size(R,XS,YS),
                     num(X), num(Y),
                     H=XS/2, V=YS/2, X>=XC-H, X<=XC+H,
                     Y>=YC-V, Y<=YC+V.

:- room(R),room_size(R,XS,YS), #count{T,X,Y : trap_pos(T,R,X,Y)}!=XS.



stairs(R,X,Y) :- end_room(R), room_center(R,X,Y).

:- trap_pos(T,R,X,Y), stairs(R,X,Y).




