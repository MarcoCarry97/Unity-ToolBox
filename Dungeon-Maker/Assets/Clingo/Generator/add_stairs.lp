
%problem instance

#const max=num_rooms*2.

#const min_size=3.



#const init_pos_x=0.

#const init_pos_y=0.



%PART NINE: STAIRS

{stairs(R,X,Y)} :- end_room(R), room_pos(R,X,Y).

:-{stairs(R,X,Y)}!=1.

:-stairs(R,X,Y), decoration(D,R,X,Y).

:-stairs(R,X,Y), decoration(D,R,X+1,Y).

:-stairs(R,X,Y), decoration(D,R,X-1,Y).

:-stairs(R,X,Y), decoration(D,R,X,Y+1).

:-stairs(R,X,Y), decoration(D,R,X,Y-1).
%:-stairs(R,X,Y), trap_pos(T,R,X,Y).

%-stairs(R,X,Y), treasure_pos(T,R,X,Y).

%:-stairs(R,X,Y), item_pos(T,R,X,Y).

%:-stairs(R,X,Y), key_pos(T,R,X,Y).









