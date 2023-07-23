
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART SEVEN: ITEMS 

item(0..(num_item-1)).

{item_pos(T,R,X,Y,X,Y)} :- item(T), normal_room(R), expand_pos(_,R,X,Y).

:- item_pos(T1,R,X,Y,_,_), item_pos(T2,R,X,Y,_,_), T1!=T2.

:- normal_room(R), room_size(R,_,_,XS,YS), {item_pos(T,R,X,Y,TX,TY)}!=1.

decoration(T,R,X,Y,TX,TY) :- item_pos(T,R,X,Y,TX,TY).

:-decoration(D1,R,X,Y,_,_), decoration(D2,R,X,Y,_,_), D1!=D2.














