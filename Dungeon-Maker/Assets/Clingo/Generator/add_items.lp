
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

{item_pos(T,R,X,Y)} :- item(T), room_pos(R,X,Y).

:- item_pos(T1,R,X,Y), item_pos(T2,R,X,Y),T1!=T2.

:- room(R), #count{T,X,Y : item_pos(T,R,X,Y)}!=num_item.















