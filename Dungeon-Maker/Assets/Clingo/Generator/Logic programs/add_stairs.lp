
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART NINE: STAIRS

{stairs(R,X,Y,@rand(0,XS-1),@rand(0,YS-1))} :- end_room(R), room_pos(R,X,Y), room_size(R,A,B,XS,YS).

:-{stairs(R,X,Y,PX,PY)}!=1.

:-stairs(R,X,Y,PX,PY), decoration(D,R,X,Y,PX,PY).

%:-stairs(R,X,Y), decoration(D,R,X+1,Y).

%:-stairs(R,X,Y), decoration(D,R,X-1,Y).

%:-stairs(R,X,Y), decoration(D,R,X,Y+1).

%:-stairs(R,X,Y), decoration(D,R,X,Y-1).
%:-stairs(R,X,Y), trap_pos(T,R,X,Y).

%-stairs(R,X,Y), treasure_pos(T,R,X,Y).

%:-stairs(R,X,Y), item_pos(T,R,X,Y).

%:-stairs(R,X,Y), key_pos(T,R,X,Y).









