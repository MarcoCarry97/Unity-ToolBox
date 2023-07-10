
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



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

point(0,0,@rand(-corr_dim,corr_dim),@rand(-corr_dim,corr_dim)).

{point(X,Y,X+@rand(-corr_dim,corr_dim),Y+@rand(-corr_dim,corr_dim))} :- num(X), num(Y), point(XS,YS,A,B), delta(_,X-XS,Y-YS).

:-{point(X,Y,CX,CY)}!=max.

%point(0,0).

%max {point((-max..max)*distance+A,(-max..max)*distance+B)} max :- point(X1,Y1), delta(_,X1-A,Y2-B).

%:- point(X1,Y1), point(X2,Y2), not delta(_,X2-X1,Y2-Y1).









