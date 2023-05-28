
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





%PART EIGHT: INITIAL ROOM & END ROOM

{initial_room(R)} :- room(R), room_center(R,X,Y).

{end_room(R)} :- room(R).

:- {initial_room(R)}!=1.

:- {end_room(R)}!=1.













