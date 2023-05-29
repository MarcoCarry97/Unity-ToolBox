
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.



%PART SEVEN: ITEMS 

item(0..(num_item-1)).

{item_pos(T,R,X,Y)} :- item(T), room_center(R,XC,YC), room_size(R,XS,YS),
                     num(X), num(Y),
                     H=XS/2, V=YS/2, X>=XC-H, X<=XC+H,
                     Y>=YC-V, Y<=YC+V.

:- room(R),room_size(R,XS,YS), #count{T,X,Y : item_pos(T,R,X,Y)}!=XS.















