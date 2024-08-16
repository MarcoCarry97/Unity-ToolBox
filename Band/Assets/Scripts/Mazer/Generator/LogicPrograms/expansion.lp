
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.





expand_room(0..3).

{expand_center(E,R,X,Y,X,Y)} :- expand_room(E), room_center(R,XC,YC,TX,TY), room_pos(R,X,Y), X!=TX,X!=TY.

%:- room(R), #count{E,X,Y : expand_center(E,R,X,Y,TX,TY)}<1.

%:- room(R), #count{E,X,Y : expand_center(E,R,X,Y,TX,TY)}>4.

:- room(R), #count{E,X,Y : expand_center(E,R,X,Y,TX,TY)}!=4.

:-{expand_center(E,R,X,Y,TX,TY)}<1.

:- room(R), expand_center(E1,R,X,Y,TX,TY), expand_center(E2,R,X,Y,TX,TY), E1!=E2.

%:- room_size(R,XS,YS,XR,YR), expand_center(E1,R,X1,Y1,TX1,TY1), expand_center(E2,R,X2,Y2,TX2,TY2),E1!=E2, TX2-TX1!=XR/3.

%:- room_size(R,XS,YS,XR,YR), expand_center(E1,R,X1,Y1,TX1,TY1), expand_center(E2,R,X2,Y2,TX2,TY2),E1!=E2, TY2-TY1!=YR/3.





{expand_size(E,R,X/2,Y/2,X/2,Y/2)} :- expand_center(E,R,_,_,_,_), room(R), place_size(R,_,_,XS,YS), size(X,Y).

:- expand_room(E), room(R), #count{X,Y : expand_size(E,R,X,Y,TX,TY)}!=1.

:- room(R), expand_size(E1,R,X,Y,X,Y), expand_size(E2,R,X,Y,X,Y), E1!=E2.



place(max..(max+num_rooms*4-1)).

{expansion(P,E,R)} :-place(P), expand_center(E,R,_,_,_,_), expand_size(E,R,_,_,_,_).

:- expansion(P1,E,R), expansion(P2,E,R), P1!=P2.

:- place(P), #count{E,R : expansion(P,E,R)}<1.


place_center(P,XC,YC,TX,TY) :- place(P), expansion(P,E,R), expand_center(E,R,XC,YC,TX,TY).

place_size(P,X,Y,TX,TY) :- place(P), expansion(P,E,R), expand_size(E,R,X,Y,TX,TY).
