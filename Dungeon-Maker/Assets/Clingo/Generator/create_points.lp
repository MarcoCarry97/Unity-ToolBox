
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

%PART ONE: POINTS

side(north).
side(south).
side(west).
side(east).

opposite(north,south).
opposite(east,west).

opposite(A,B) :- opposite(B,A).

delta(north,0,distance).
delta(east,distance,0).

delta(S,X,Y) :- opposite(S,T), delta(T,-X,-Y).

num(N) :- N=(-max..max)*distance.

point(0,0).

{point(X,Y)} :- num(X), num(Y), point(XS,YS), delta(_,X-XS,Y-YS).

:-{point(X,Y)}!=max.

















