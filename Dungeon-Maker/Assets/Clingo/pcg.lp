
%problem instance

#const dim=5.

#const num_rooms=20.

grid(num_rooms,num_rooms).

size(dim,dim).

orientation(north).
orientation(south).
orientation(west).
orientation(east).

opposite(north,south).
opposite(west, east).

delta(north,1,0).
delta(east,0,1).

opposite(A,B) :- opposite(B,A).
delta(D,X,Y) :- delta(E,-X,-Y), opposite(D,E).

num(1..num_rooms).

flag(start).
flag(normal).
flag(next).






%create rooms

{room(R)} :- num(R).

:-{room(R)}<num_rooms/2.

:-{room(R)}>num_rooms.






%assign flag

{has_flag(R,F)} :- room(R), flag(F).

:- room(R), #count{F : has_flag(R,F)}!=1.

:-{has_flag(R,start)}!=1.

:-{has_flag(R,next)}!=1.

:-N=num_rooms-2, {has_flag(R,normal)}!=N.





%assign doors.

{door(R1,R2,O)} :- room(R1), room(R2),R1!=R2, orientation(O).

door(R1,R2,O) :- door(R2,R1,P), opposite(O,P).

:- orientation(O), door(R1,R2,O), door(R2,R1,O).

:-orientation(O), room(R), #count{S : door(R,S,O)}!=1.

:-room(R), has_flag(R,normal), #count{S : door(R,S,_)}=2.

:-room(R), has_flag(R,normal), #count{S : door(R,S,_)}=4.





%each room must be reachable

reachable(R,R) :- room(R).

reachable(R1,R2) :- door(R1,R2,_).

reachable(R1,R2) :- room(R3), R1!=R3, R2!=R3, R1!=R2, reachable(R1,R3), reachable(R3,R2).

:- #count{R1,R2 : reachable(R1,R2)}<1.






%assign initial room

initial_room(R) :- room(R), has_flag(R,start).





#show door/3.

#show reachable/2.


