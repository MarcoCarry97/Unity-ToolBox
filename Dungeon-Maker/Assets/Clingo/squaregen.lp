%problem instance



#const max=5.

#const num_rooms=max*3.

#const gap=(max/num_rooms).

grid(max,max).

num(0..max).

square(X,Y) :- grid(A,B), X=0..(A-1), Y=0..(B-1).

size(1,Y) :- grid(A,B), Y=1..(B-1).

size(X,1) :- grid(A,B), X=1..(A-1).

%size(3,4).

%size(4,3).

%size(5,4).

%size(4,5).

size(5,5).






%generation rooms.

{room(R)} :-num(R).

:-{room(R)}!=num_rooms.




%assign squares to rooms.

%{room_square(R,X,Y)} :- room(R), square(X,Y).

%:- room(R), #count{X,Y : room_square(R,X,Y)}!=1.

%:- room_square(R,X,Y), room_square(S,X,Y), R!=S.



