
%problem instance

#const num_rooms=3.

#const distance=9.

#const max=num_rooms*2.

#const min_size=3.

#const max_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.



%PART NINE: STAIRS

{stairs(R,X,Y)} :- end_room(R), room_center(R,XC,YC), room_size(R,XS,YS),
                     num(X), num(Y),
                     H=XS/2, V=YS/2, X>=XC-H, X<=XC+H,
                     Y>=YC-V, Y<=YC+V.

:-{stairs(R,X,Y)}!=1.

%:-stairs(R,X,Y), trap_pos(T,R,X,Y).

%-stairs(R,X,Y), treasure_pos(T,R,X,Y).

%:-stairs(R,X,Y), item_pos(T,R,X,Y).

%:-stairs(R,X,Y), key_pos(T,R,X,Y).









