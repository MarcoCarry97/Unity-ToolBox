
%problem instance

#const num_rooms=3.

#const distance=9.

#const max=num_rooms*2.

#const min_size=3.

#const max_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.






%PART TWO: ROOMS & CORRIDORS.

room(R) :- R=0..(num_rooms-1).

corridor(C) :- C=num_rooms..(max-1).




{room_center(R,X,Y)} :- room(R), point(X,Y).

:- {room_center(R,X,Y)}!=num_rooms.

:- room(R), #count{X,Y : room_center(R,X,Y)}!=1.

:- room_center(R1,X,Y), room_center(R2,X,Y), R1!=R2.





{corr_center(C,X,Y)} :- corridor(C), point(X,Y).

:- S=max-num_rooms, {corr_center(C,X,Y)}!=S.

:- corridor(C), #count{X,Y : corr_center(C,X,Y)}!=1.

:- corr_center(R1,X,Y), corr_center(R2,X,Y), R1!=R2.




place_center(P,X,Y) :- room_center(P,X,Y).

place_center(P,X,Y) :- corr_center(P,X,Y).

:- place_center(R1,X,Y), place_center(R2,X,Y), R1!=R2.




