
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.



%PART SEVEN: ITEMS 

item(0..(num_item-1)).

{item_pos(T,R,X,Y,@rand(0,XS-1),@rand(0,YS-1))} :- item(T), room_pos(R,X,Y), room_size(R,A,B,XS,YS).

decoration(D,R,X,Y,PX,PY) :- item_pos(D,R,X,Y,PX,PY).

:-decoration(D1,R,X,Y,PX,PY), decoration(D2,R,X,Y,PX,PY), D1!=D2.














