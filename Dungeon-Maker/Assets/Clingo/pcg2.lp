% problem instance

#const max=5.

#const max_size=5.

#const num_rooms=max*3.

grid(max,max).

size(max_size,max_size).

%num(0..num_rooms).

square(X,Y) :- grid(R,C), X=0..R, Y=0..C.

side(north).
side(south).
side(east).
side(west).

opposite(north,south).
opposite(east,west).

opposite(A,B) :- opposite(B,A).

delta(north,0,1).
delta(east,1,0).

delta(O,X,Y) :- opposite(O,P), delta(P,-X,-Y).

room(R) :- R=0..num_rooms.





%assign squares to rooms.

{room_square(R,X,Y)} :- room(R), square(X,Y).

:- room(R), #count{X,Y : room_square(R,X,Y)}!=1.

:- room_square(R,X,Y), room_square(S,X,Y), R!=S.




%initial & end rooms

initial_room(R) :- room(R), room_square(R,0,0).

:-{initial_room(R)}!=1.

end_room(E) :- room(E), grid(R,C), room_square(E,R,C).

:- {end_room(R)}!=1.








%create doors

{door(R1,R2,S)} :- room_square(R1,X1,Y1), room_square(R2,X2,Y2), R1!=R2,
                 delta(S,X2-X1,Y2-Y1).                

door(R1,R2,S) :- opposite(S,T), door(R2,R1,T).

:- room(R), #count{S : door(R,S,_)}<=0.




%blind rooms

blind_room(R) :- room(R), #count{S : door(R,S,_)}=1.




%reachability

route(R1,R2,1) :- door(R1,R2,_).

route(R1,R2,N+1) :- room_square(R3,_,_), door(R3,R2,_), N<num_rooms,route(R1,R3,N).






%reachability from start to end and blinds.

:- initial_room(S), end_room(E), not route(S,E,_).

:- initial_room(S), blind_room(B), not route(S,B,_).



#show size/2.

#show initial_room/1.

#show end_room/1.

#show blind_room/1.

#show room_square/3.

#show door/3.

#show delta/3.




