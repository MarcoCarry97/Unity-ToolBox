%problem instance

%variables

#const num_rooms=7.

#const max=num_rooms*2.

#const min_size=3.

#const max_size=3.

#const distance=5.






orientation(north).
orientation(south).
orientation(east).
orientation(west).

opposite(north,south).
opposite(west,east).
opposite(X,Y) :- opposite(Y,X).

delta(north,0,distance).
delta(east,distance,0).
delta(O,X,Y) :- opposite(O,P), delta(P,-X,-Y).



grid(max,max).

num(0..max).

pos((0..max)*distance).

center(X,Y) :- pos(X), pos(Y).

size(X,Y) :- num(X), num(Y), X>=min_size, X<=max_size, Y>=min_size, Y<=max_size.

size(1,1).





{room(R)} :- num(R), R<num_rooms.

:-{room(R)}!=num_rooms.

{corridor(R)} :- num(R), R<max, R>=num_rooms.

:-{corridor(R)}!=(max-num_rooms).





{room_center(R,X,Y)} :- room(R), center(X,Y).

:- {room_center(R,X,Y)}!=num_rooms.

:- room(R), #count{X,Y : room_center(R,X,Y)}!=1.

:- room_center(R1,X,Y), room_center(R2,X,Y), R1!=R2.






{corr_center(R,X,Y)} :- corridor(R), center(X,Y).

:- {corr_center(R,X,Y)}!=(max-num_rooms).

:- corr_center(R1,X,Y), corr_center(R2,X,Y), R1!=R2.

:- corridor(R), #count{X,Y : corr_center(R,X,Y)}!=1.






{room_size(R,X,Y)} :- room(R), size(X,Y), X>1, Y>1.

:- room(R),{room_size(R,X,Y)}!=1.

:- {room_size(R,X,Y)}!=num_rooms.




{corr_size(R,1,1)} :- corridor(R).

:- corridor(R),{corr_size(R,X,Y)}!=1.

:- {corr_size(R,X,Y)}!=num_rooms.




{room_size(R,1,1)} :- corridor(R).


place_center(P,X,Y) :- room_center(P,X,Y).
place_center(P,X,Y) :- corr_center(P,X,Y).

place_size(P,X,Y) :- room_size(P,X,Y).
place_size(P,X,Y) :- corr_size(P,X,Y).

:-place_center(P,X,Y), place_center(Q,X,Y), P!=Q.






{door(P1,P2,O)} :- place_center(P1,X1,Y1), place_center(P2,X2,Y2),
                 delta(O,X2-X1,Y2-Y1).

door(R1,R2,S) :- opposite(S,T), door(R2,R1,T).

:- place_center(P,_,_), #count{Q,O : door(P,Q,O)}<1.

:- place_center(P,_,_), #count{Q,O : door(P,Q,O)}>4.

:-{door(R1,R2,O)}<max.

:-corridor(C), #count{R,O : door(C,R,O)}<2.





{initial_room(R)} :- place_center(R,_,_), room(R).

:-{initial_room(R)}!=1.

{end_room(R)} :- place_center(R,_,_),room(R).

:-{end_room(R)}!=1.

{blind_room(R)} :- place_center(R,_,_), room(R), {door(R,_,_)}=1.

:-{blind_room(R)}!=num_rooms/2.





path(P1,P2,1) :- door(P1,P2,_).

path(P1,P2,N) :- path(P2,P1,N).

path(P1,P2,N):- place_center(P3,_,_), door(P1,P3,_), num(N), path(P3,P2,N-1).


:-initial_room(R), end_room(R).

:-initial_room(I), end_room(E), not path(I,E,_).

:-place_center(I,_,_), place_center(E,_,_), not path(I,E,_).






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





#show place_center/3.
#show place_size/3.
#show opposite/2.
#show delta/3.
#show room_center/3.
#show room_size/3.
#show num/1.
#show room/1.
#show initial_room/1.
#show end_room/1.
#show corridor/1.

















