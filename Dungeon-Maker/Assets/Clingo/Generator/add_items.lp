
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART SEVEN: ITEMS 

item(0..(num_item-1)).

{item_pos(T,R,X,Y)} :- item(T), room_pos(R,X,Y).

decoration(D,R,X,Y) :- item_pos(D,R,X,Y).

:-decoration(D1,R,X,Y), decoration(D2,R,X,Y), D1!=D2.














