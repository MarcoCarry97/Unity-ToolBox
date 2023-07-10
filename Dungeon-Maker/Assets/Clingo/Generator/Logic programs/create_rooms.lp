
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART TWO: ROOMS & CORRIDORS.

room(R) :- R=0..(num_rooms-1).

corridor(C) :- C=num_rooms..(max-1).




{room_center(R,X,Y,TX,TY)} :- room(R), point(X,Y,TX,TY).

:- {room_center(R,X,Y,TX,TY)}!=num_rooms.

:- room(R), #count{X,Y : room_center(R,X,Y,_,_)}!=1.

:- room_center(R1,X,Y,_,_), room_center(R2,X,Y,_,_), R1!=R2.

%{room_center(R,X,Y)}=1 :- point(X,Y), X=(-max..,max)*distance, Y=(-max..,max)*distance.

%room_center(R1,X,Y), room_center(R2,X,Y), R1!=R2.





{corr_center(C,X,Y,X,Y)} :- corridor(C), point(X,Y,A,B).

:- S=max-num_rooms, {corr_center(C,X,Y,X,Y)}!=S.

:- corridor(C), #count{X,Y : corr_center(C,X,Y,X,Y)}!=1.

:- corr_center(R1,X,Y,X,Y), corr_center(R2,X,Y,X,Y), R1!=R2.




place_center(P,X,Y,TX,TY) :- room_center(P,X,Y,TX,TY).

place_center(P,X,Y,TX,TY) :- corr_center(P,X,Y,TX,TY).

:- place_center(R1,X,Y,_,_), place_center(R2,X,Y,_,_), R1!=R2.






