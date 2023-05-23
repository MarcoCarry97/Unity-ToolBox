%problem instance

%variables

#const num_rooms=7.

#const max=num_rooms*2.

#const min_size=3.

#const max_size=3.

#const distance=5.







%PART TWO






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


