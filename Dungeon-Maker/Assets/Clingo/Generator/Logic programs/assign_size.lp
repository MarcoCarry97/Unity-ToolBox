
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.




%PART THREE: DIMENSIONS.

size(min_size..max_size,min_size..max_size).

%{room_size(R,min_size..max_size,min_size..max_size)}=1 :- room(R).

%{room_size(R,@rand(min_size,max_size),@rand(min_size,max_size))} :- room(R).

{room_size(R,X,Y,@rand(min_size,max_size), @rand(min_size,max_size
))} :- room(R), size(X,Y).

:-{room_size(R,X,Y,XR,YR)}!=num_rooms.

:- room(R), #count{X,Y : room_size(R,X,Y,XR,YR)}!=1.

corr_size(C,corr_dim,corr_dim,corr_dim,corr_dim) :- corridor(C).





place_size(P,X,Y,XR,YR) :- room_size(P,X,Y,XR,YR).

place_size(P,X,Y,XR,YR) :- corr_size(P,X,Y,XR,YR).



pos(0..C-1,0..D-1) :- room_size(R,A,B,C,D).



room_pos(R,X,Y) :- room_size(R,A,B,C,D), pos(X,Y).

%not room_pos(R,X,Y) :- room(R),X=0; room(R),Y=0.