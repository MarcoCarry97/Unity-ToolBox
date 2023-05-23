
%problem instance

#const num_rooms=7.

#const max=num_rooms*2.

#const distance=num_rooms/2.



num(0..(max-1)).

grid(max,max).

{room(R)} :- num(R), R<num_rooms.

:-{room(R)}!=num_rooms.

center(X,Y) :- num(X), num(Y).





{room_center(R,X,Y)} :- room(R), center(X,Y).

:- {room_center(R,X,Y)}!=num_rooms.

:- room(R), #count{X,Y : room_center(R,X,Y)}!=1.

:- room_center(R1,X,Y), room_center(R2,X,Y), R1!=R2.

:- room_center(R1,X1,Y1), room_center(R2,X2,Y2), X1=X2.




%PART TWO

{corridor(C)} :- num(C), C>=num_rooms, C<max.

:-{corridor(C)}!=num_rooms.




%{corr_center(C,X1,Y2;C,X2,Y1)} :- corridor(C),room_center(R1,X1,Y1), room_center(R2,X2,Y2),R1!=R2.

%:-{corr_center(C,X,Y)}<1.