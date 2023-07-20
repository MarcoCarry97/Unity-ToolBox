
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART NINE: STAIRS

{stairs(R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- end_room(R), room_pos(R,X,Y), room_size(R,XS,YS,XR,YR), X<XR, Y<YR.

:-{stairs(R,X,Y,PX,PY)}!=1.

:-stairs(R,_,_,PX,PY), decoration(D,R,_,_,PX,PY).




{start_point(R,X,Y,@rand_dec(X,XS,XR+1),@rand_dec(Y,YS,YR+1))} :- initial_room(R), room_pos(R,X,Y), room_size(R,XS,YS,XR,YR), X<XR, Y<YR.

:-{start_point(R,X,Y,PX,PY)}!=1.

:-start_point(R,_,_,PX,PY), decoration(D,R,_,_,PX,PY).

%:-stairs(R,X,Y), decoration(D,R,X+1,Y).

%:-stairs(R,X,Y), decoration(D,R,X-1,Y).

%:-stairs(R,X,Y), decoration(D,R,X,Y+1).

%:-stairs(R,X,Y), decoration(D,R,X,Y-1).
%:-stairs(R,X,Y), trap_pos(T,R,X,Y).

%-stairs(R,X,Y), treasure_pos(T,R,X,Y).

%:-stairs(R,X,Y), item_pos(T,R,X,Y).

%:-stairs(R,X,Y), key_pos(T,R,X,Y).









