
%problem instance

#const max=num_rooms*2.

#const min_size=3.



#const init_pos_x=0.

#const init_pos_y=0.




%PART THREE: DIMENSIONS.

size(min_size..max_size,min_size..max_size).

%{room_size(R,min_size..max_size,min_size..max_size)}=1 :- room(R).

%{room_size(R,@rand(min_size,max_size),@rand(min_size,max_size))} :- room(R).

{room_size(R,X,Y)} :- room(R), size(X,Y).

:-{room_size(R,X,Y)}!=num_rooms.

:- room(R), #count{X,Y : room_size(R,X,Y)}!=1.

corr_size(C,1,1) :- corridor(C).





place_size(P,X,Y) :- room_size(P,X,Y).

place_size(P,X,Y) :- corr_size(P,X,Y).



pos(0..A-1,0..B-1) :- room_size(R,A,B).



room_pos(R,X,Y) :- room_size(R,A,B), pos(X,Y).

%not room_pos(R,X,Y) :- room(R),X=0; room(R),Y=0.