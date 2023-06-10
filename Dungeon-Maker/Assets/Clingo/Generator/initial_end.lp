
%problem instance

#const max=num_rooms*2.

#const min_size=3.

#const num_trap=5.

#const num_treasure=5.

#const num_item=5.

#const init_pos_x=0.

#const init_pos_y=0.





%PART EIGHT: INITIAL ROOM & END ROOM

1 {initial_room(0..(num_rooms-1))} 1.

1 {end_room(0..(num_rooms-1))} 1.

:-initial_room(R), end_room(R).












