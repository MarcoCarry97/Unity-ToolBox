
%problem instance

#const max=num_rooms*2.

#const min_size=3.



#const init_pos_x=0.

#const init_pos_y=0.





%PART EIGHT: INITIAL ROOM & END ROOM

1 {initial_room(0..(num_rooms-1))} 1.

1 {end_room(0..(num_rooms-1))} 1.

%initial_room(@rand(0,num_rooms)).

%end_room(@rand(0,num_rooms)).

:-initial_room(R), end_room(R).

%:- initial_room(I), end_room(E), path_length(L), not path(I,E,L).











