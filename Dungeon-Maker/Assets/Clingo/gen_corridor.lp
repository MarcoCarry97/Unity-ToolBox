%problem instance

%variables

#const len_grid_x=50.

#const len_grid_y=50.

#const num_rooms=35.

grid(len_grid_x,len_grid_y).

num(0..((len_grid_x+len_grid_y)/2-1)).

center(X,Y) :- num(X), num(Y).

size(1,1).

orientation(north).
orientation(south).
orientation(east).
orientation(west).

opposite(north,south).
opposite(east,west).
opposite(X,Y) :- opposite(Y,X).

delta(north,0,1).
delta(east,1,0).
delta(O,X,Y) :- opposite(O,P), delta(P,-X,-Y).



%rooms

{room(R)} :- num(R), R<=num_rooms.

:- {room(R)} !=num_rooms.








%assign centers to rooms.

{room_center(R,X,Y)} :- room(R), center(X,Y).

:- room(R), #count{X,Y : room_center(R,X,Y)}!=1.

:- room_center(R,X,Y), room_center(S,X,Y), R!=S.





%assign size to rooms.

{room_size(R,X,Y)} :- room(R), size(X,Y).

:- room(R), #count{X,Y : room_size(R,X,Y)}!=1.




%assigning doors.

{door(R1,R2,S)} :- room_center(R1,X1,Y1), room_center(R2,X2,Y2), R1!=R2,
                 delta(S,X2-X1,Y2-Y1).                

door(R1,R2,S) :- opposite(S,T), door(R2,R1,T).

:- room(R), #count{S : door(R,S,_)}<1.





%#show room_center/3.
#show room_size/3.

%HERE I APPEND THE RESULTS OF THE PREVIOUS PHASE.