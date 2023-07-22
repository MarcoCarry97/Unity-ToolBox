
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



blind_room(R) :- room(R), #count{S : door(R,S,_)}=1.



%PART SIX: TREASURES & KEYS

treasure(0..(num_treasure-1)).

{treasure_pos(T,R,X,Y,X,Y)} :- treasure(T), blind_room(R), expand_pos(_,R,X,Y).

:- treasure_pos(T1,R,X,Y,_,_), treasure_pos(T2,R,X,Y,_,_), T1!=T2.

:- blind_room(R), room_size(R,_,_,XS,YS), {treasure_pos(T,R,X,Y,TX,TY)}!=1.

decoration(T,R,X,Y,TX,TY) :- treasure_pos(T,R,X,Y,TX,TY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.


