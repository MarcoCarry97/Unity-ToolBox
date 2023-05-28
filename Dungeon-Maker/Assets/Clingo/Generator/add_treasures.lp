
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




%PART SIX: TREASURES & KEYS

treasure(0..(num_treasure-1)).

{treasure_pos(T,R,X,Y)} :- treasure(T), room_center(R,XC,YC), room_size(R,XS,YS),
                     num(X), num(Y),
                     H=XS/2, V=YS/2, X>=XC-H, X<=XC+H,
                     Y>=YC-V, Y<=YC+V.

:- room(R),room_size(R,XS,YS), #count{T,X,Y : treasure_pos(T,R,X,Y)}!=XS.



key(0..(num_treasure-1)).

{key_pos(T,R,X,Y)} :- key(T), room_center(R,XC,YC), room_size(R,XS,YS),
                     num(X), num(Y),
                     H=XS/2, V=YS/2, X>=XC-H, X<=XC+H,
                     Y>=YC-V, Y<=YC+V.

:- room(R),room_size(R,XS,YS), #count{T,X,Y : key_pos(T,R,X,Y)}!=XS.


:- T={treasure_pos(T1,R1,X1,Y1)},K={key_pos(T1,R1,X1,Y1)}, T!=K.


