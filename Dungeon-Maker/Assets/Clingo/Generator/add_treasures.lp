
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



blind_room(R) :- room(R), #count{S : door(R,S,_)}=1.



%PART SIX: TREASURES & KEYS

treasure(0..(num_treasure-1)).

{treasure_pos(T,R,X,Y)} :- treasure(T), blind_room(R), room_pos(R,X,Y), room_size(R,XS,YS), X<XS, Y<YS.

not treasure_pos(T,R,X,Y) :- treasure(T), room_size(R,A,B), room_pos(R,X,Y), X=A/2.

not treasure_pos(T,R,X,Y) :- treasure(T), room_size(R,A,B), room_pos(R,X,Y), Y=B/2.


:- blind_room(R), #count{T,X,Y : treasure_pos(T,R,X,Y)}!=1.

%:-{treasure_pos(T,R,X,Y)}!=num_treasure.

decoration(D,R,X,Y) :- treasure_pos(D,R,X,Y).

:-decoration(D1,R,X,Y), decoration(D2,R,X,Y), D1!=D2.


