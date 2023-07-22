
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.

room(R) :- R=0..(num_rooms-1).

corridor(C) :- C=num_rooms..(max-1).


%PART THREE: DIMENSIONS.

%size(min_size..max_size,min_size..max_size).

%{room_size(R,min_size..max_size,min_size..max_size)}=1 :- room(R).

%{room_size(R,@rand(min_size,max_size),@rand(min_size,max_size))} :- room(R).

%{room_size(R,X,Y,@rand_size(X,min_size,max_size), @rand_size(Y,min_size,max_size
%))} :- room(R), size(X,Y).

%:-{room_size(R,X,Y,XR,YR)}!=num_rooms.

%:- room(R), #count{X,Y : room_size(R,X,Y,XR,YR)}!=1.

corr_size(C,corr_dim,corr_dim,corr_dim,corr_dim) :- corridor(C).

num_rooms {room_size(R,min_size..max_size,min_size..max_size,@rand(min_size,max_size),@rand(min_size,max_size))} num_rooms :- room(R).



place_size(P,X,Y,XR,YR) :- room_size(P,X,Y,XR,YR).

place_size(P,X,Y,XR,YR) :- corr_size(P,X,Y,XR,YR).



pos(0..C-1,0..D-1) :- place_size(R,A,B,C,D).

room_pos(R,X,Y) :- place_size(R,A,B,C,D), pos(X,Y).

%not room_pos(R,X,Y) :- room(R),X=0; room(R),Y=0.