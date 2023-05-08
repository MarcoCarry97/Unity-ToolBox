% problem instance

#const max=5.

#const max_size=5.

#const num_rooms=max*3.

#const num_angles=num_rooms/3.

#const num_corr=num_rooms/3.

#const num_traps=5.

grid(max,max).

axe(horiz).

axe(vert).

size(3,3).

center(X,Y) :- grid(R,C), X=0..R, Y=0..C.

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

orient_axe(east,horiz).
orient_axe(north,vert).

orient_axe(O,A) :- opposite(O,P), orient_axe(P,A).

room(R) :- R=0..num_rooms.



%assign centers to rooms.

{room_center(R,X,Y)} :- room(R), center(X,Y).

:- room(R), #count{X,Y : room_center(R,X,Y)}!=1.

:- room_center(R,X,Y), room_center(S,X,Y), R!=S.





%assign size to rooms.

{room_size(R,X,Y)} :- room(R), size(X,Y).

:- room(R), #count{X,Y : room_size(R,X,Y)}!=1.





%compute num doors

num_doors(R,N) :- room_center(R,X,Y), D=#count{R1 : door(R,R1,_)}, N=D.





%initial & end rooms

initial_room(0) :- room_center(0,_,_).

%:- {initial_room(R)}!=1.

end_room(num_rooms) :- room_center(num_rooms,_,_).

%:- end_room(num_rooms)!=1.








%create doors

{door(R1,R2,S)} :-room_center(R1,X1,Y1), room_center(R2,X2,Y2), R1!=R2,
                 delta(S,X2-X1,Y2-Y1).                

door(R1,R2,S) :- opposite(S,T), door(R2,R1,T).

:- room(R), #count{S : door(R,S,_)}<=0.




%blind rooms

blind_room(R) :- room(R), #count{S : door(R,S,_)}=1.

:-{blind_room(R)}!=num_rooms/2.

%:-blind_room(R), room_size(R,1,Y;R,X,1).









%reachability

route(R1,R2,1) :- door(R1,R2,_).

route(R1,R2,N+1) :- room_center(R3,_,_), door(R3,R2,_), N<num_rooms,route(R1,R3,N).






%reachability from start to end and blinds.

:- initial_room(S), end_room(E), not route(S,E,_).

:- initial_room(S), blind_room(B), not route(S,B,_).







%positioning stairs

%{stairs(R,X,Y)} :- end_room(R), center(X,Y), room_size(R,XD,YD), X>=0, Y>=0,
%                 X<=XD, Y<=YD.

%:- {stairs(R,X,Y)} !=1.








#show initial_room/1.

#show end_room/1.

#show blind_room/1.

#show room_center/3.

#show door/3.

#show delta/3.

#show room_size/3.

%#show stairs/3.

