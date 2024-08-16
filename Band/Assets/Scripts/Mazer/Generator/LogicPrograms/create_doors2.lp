
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.


centre(R,X,Y,TX,TY) :- place_center(R,X,Y,TX,TY).

centre(R,X,Y,TX,TY) :- expansion(P,E,R), place_center(P,X,Y,TX,TY).


%PART FOUR: DOORS & PATHS

%{door(P1,P2,O)} :- place_center(P1,X1,Y1,TX1,TY1), place_center(P2,X2,Y2,TX2,TY2), delta(O,X2-X1,Y2-Y1).

%door(R1,R2,S) :- opposite(S,T), door(R2,R1,T).

%:- place_center(P,_,_,_,_), #count{Q : door(P,Q,_)}<0.

%:- place_center(P,_,_,_,_), #count{Q : door(P,Q,_)}>4.

%:-side(O), place_center(P,_,_,_,_), #count{Q : door(P,Q,O)}>1.

%:-side(O), place_center(P,_,_,_,_), #count{Q : door(Q,P,O)}>1.

%:-corridor(C), #count{R,O : door(C,R,O)}<2.




%path(P1,P2,1) :- door(P1,P2,_).

%path(P1,P2,N) :- path(P2,P1,N).

%path(P1,P2,N) :- place_center(P3,_,_,_,_), door(P1,P3,_), N<max, path(P3,P2,N-1).

%:-place_center(I,_,_,_,_), place_center(E,_,_,_,_), not path(I,E,_).

%:- room(R1), room(R2), path(R1,R2,N), N<max_path/2.

%:- room(R1), room(R2), path(R1,R2,N), N>max_path.