
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.





room_pos(R,(XC-XS/2+1)..(XC+XS/2-1), (YC-YS/2+1)..(YC+YS/2)-1) :-
    place_center(R,_,_,XC,YC), place_size(R,_,_,XS,YS).




