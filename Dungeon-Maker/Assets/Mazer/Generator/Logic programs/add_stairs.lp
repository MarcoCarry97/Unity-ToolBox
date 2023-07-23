
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART NINE: STAIRS



{stairs(R,X,Y,X,Y)} :- end_room(R), expand_pos(_,R,X,Y).

:-end_room(R), {stairs(R,X,Y,TX,TY)}!=1.

decoration(-1,R,X,Y,TX,TY) :- stairs(R,X,Y,TX,TY).





{start_point(R,X,Y,X,Y)} :- initial_room(R), expand_pos(_,R,X,Y).

:-initial_room(R), {start_point(R,X,Y,TX,TY)}!=1.

decoration(-1,R,X,Y,TX,TY) :- stairs(R,X,Y,TX,TY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.

%:-stairs(R,X,Y), decoration(D,R,X+1,Y).

%:-stairs(R,X,Y), decoration(D,R,X-1,Y).

%:-stairs(R,X,Y), decoration(D,R,X,Y+1).

%:-stairs(R,X,Y), decoration(D,R,X,Y-1).
%:-stairs(R,X,Y), trap_pos(T,R,X,Y).

%-stairs(R,X,Y), treasure_pos(T,R,X,Y).

%:-stairs(R,X,Y), item_pos(T,R,X,Y).

%:-stairs(R,X,Y), key_pos(T,R,X,Y).









