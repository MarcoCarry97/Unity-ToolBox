%problem instance

#const max_len=50.

#const num_level=5.

#const num_leaves=2**num_level.

grid(max_len,max_len).

axe(horiz).
axe(vert).

perpendicural(horiz,vert).
perpendicural(vert,horiz).

delta(horiz,0,1).
delta(vert,1,0).

num(0..max_len).


side(A,B,X) :- num(A), num(B), axe(X),B>A.


space(ID,X1,X1+1,Y1,Y1+1,1) :- num(ID),side(X1,X1+1,horiz), side(Y1,Y1+1,vert).

%:- space(ID1,X1,X2,Y1,Y2,L),space(ID2,X1,X2,Y1,Y2,L), ID1!=ID2.



space(ID,X1,X2,Y1,Y2,L) :- L>1,num(ID), XM=X1+(X2-X1)/2, space(ID1,X1,XM-1,Y1,Y2,L-1),
                             space(ID2,XM,X2,Y1,Y2,L-1), L<num_level.

                             