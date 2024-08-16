
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.

room(R) :- R=0..(num_rooms-1).

corridor(C) :- C=num_rooms..(max-1).


%PART THREE: DIMENSIONS.

size(min_size..max_size,min_size..max_size).

{room_size(R,X,Y,X,Y)} :- room(R), size(X,Y).

:-{room_size(R,X,Y,XR,YR)}!=num_rooms.

:- room(R), #count{X,Y : room_size(R,X,Y,XR,YR)}!=1.

corr_size(C,corr_dim,corr_dim,corr_dim,corr_dim) :- corridor(C).




place_size(P,X,Y,XR,YR) :- room_size(P,X,Y,XR,YR).

place_size(P,X,Y,XR,YR) :- corr_size(P,X,Y,XR,YR).



