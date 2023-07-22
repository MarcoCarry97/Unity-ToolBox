
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.





expand_room(0..3).

{expand_center(E,R,X,Y)} :- expand_room(E), room(R), not room_center(R,X,_,_,_), not room_center(R,_,Y,_,_), room_pos(R,X,Y).

:- room(R), #count{E,X,Y : expand_center(E,R,X,Y)}<1.

:- room(R), #count{E,X,Y : expand_center(E,R,X,Y)}>4.

:-{expand_center(E,R,X,Y)}<num_rooms.

:- room(R), expand_center(E1,R,X,Y), expand_center(E2,R,X,Y), E1!=E2.




{expand_size(E,R,X,Y)} :- expand_center(E,R,_,_), room(R), room_size(R,X,Y,_,_).

:- expand_room(E), room(R), #count{X,Y : expand_size(E,R,X,Y)}!=1.

%:- room(R), expand_size(E1,R,X,Y), expand_size(E2,R,X,Y), E1!=E2.



place(max..(max+num_rooms*4-1)).

{expansion(P,E,R)} :-place(P), expand_center(E,R,_,_), expand_size(E,R,_,_).

:- expansion(P1,E,R), expansion(P2,E,R), P1!=P2.

:- place(P), #count{E,R : expansion(P,E,R)}<1.


place_center(P,XC,YC,XC,YC) :- place(P), expansion(P,E,R), expand_center(E,R,X,Y), place_center(R,XR,YR,TX,TY), XC=X+TX, YC=Y+TY.

place_size(P,X,Y,X,Y) :- place(P), expansion(P,E,R), expand_size(E,R,X,Y).



expand_pos(E,R,0..(X-1),0..(Y-1)) :- expansion(_,E,R), expand_size(E,R,X,Y).

expand_pos(-1,R,X,Y) :- room_pos(R,X,Y).