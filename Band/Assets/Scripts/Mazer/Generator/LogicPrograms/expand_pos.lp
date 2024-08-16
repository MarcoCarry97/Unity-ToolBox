



%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.







room_pos(R,(XC-XS/2+1)..(XC+XS/2-1), (YC-YS/2+1)..(YC+YS/2-1)) :-
    expand_center(E,R,XC,YC,_,_), expand_size(E,R,XS,YS,_,_).