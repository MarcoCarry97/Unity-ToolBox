
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART TWO: ROOMS & CORRIDORS.

room(R) :- R=0..(num_rooms-1).

corridor(C) :- C=num_rooms..(max-1).




{room_center(R,X,Y,@rand_center(X,XS,corr_dim),@rand_center(Y,YS,corr_dim))} :- room(R),room_size(R,_,_,XS,YS), point(X,Y).

:- {room_center(R,X,Y,TX,TY)}!=num_rooms.

:- room(R), #count{X,Y : room_center(R,X,Y,_,_)}!=1.

:- room_center(R1,X,Y,_,_), room_center(R2,X,Y,_,_), R1!=R2.

%{room_center(R,X,Y)}=1 :- point(X,Y), X=(-max..,max)*distance, Y=(-max..,max)*distance.

%room_center(R1,X,Y), room_center(R2,X,Y), R1!=R2.





{corr_center(C,X,Y,X,Y)} :- corridor(C), point(X,Y).

:- S=max-num_rooms, {corr_center(C,X,Y,_,_)}!=S.

:- corridor(C), #count{X,Y : corr_center(C,X,Y,_,_)}!=1.

:- corr_center(R1,X,Y,_,_), corr_center(R2,X,Y,_,_), R1!=R2.




place_center(P,X,Y,TX,TY) :- room_center(P,X,Y,TX,TY).

place_center(P,X,Y,TX,TY) :- corr_center(P,X,Y,TX,TY).

:- place_center(R1,X,Y,_,_), place_center(R2,X,Y,_,_), R1!=R2.






