
%problem instance

#const max=num_rooms*2.

#const min_size=3.


#const init_pos_x=0.

#const init_pos_y=0.



%pos(X,Y) :- size(A,B), X=(-A/2)..(A/2), Y=(-B/2)..(B/2), X!=0, Y!=0.



%room_pos(R,X,Y) :- room_size(R,A,B), room(R),size(A,B),pos(X,Y),
%                   X>-A/2, X<A/2, Y>-B/2, Y<B/2.

room_pos(R,0..(A-1),0..(B-1)) :- room_size(R,A,B).



