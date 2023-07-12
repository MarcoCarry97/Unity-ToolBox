
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



blind_room(R) :- room(R), #count{S : door(R,S,_)}=1.



%PART SIX: TREASURES & KEYS

treasure(0..(num_treasure-1)).

{treasure_pos(T,R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- treasure(T), blind_room(R), room_pos(R,X,Y), room_size(R,XS,YS,XR,YR), X<XR, Y<YR.

not treasure_pos(T,R,X,Y,PX,PY) :- treasure(T), room_size(R,A,B,XR,YR),room_pos(R,PX,PY), room_pos(R,X,Y), X=XR/2.

not treasure_pos(T,R,X,Y,PX,PY) :- treasure(T), room_size(R,A,B,XR,YR),room_pos(R,PX,PY), room_pos(R,X,Y), Y=YR/2.


:- blind_room(R), #count{T,X,Y : treasure_pos(T,R,X,Y,PX,PY)}!=1.

%:-{treasure_pos(T,R,X,Y)}!=num_treasure.

decoration(D,R,X,Y,PX,PY) :- treasure_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,_,_,PX,PY), decoration(D2,R,_,_,PX,PY), D1!=D2.


