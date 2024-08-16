
%problem instance

#const max=num_rooms*2.

#const min_size=corr_dim+2.



#const init_pos_x=0.

#const init_pos_y=0.


delta(north,0,10).delta(east,10,0).delta(west,-10,0).delta(south,0,-10).place_center(8,0,0,0,0).place_center(9,10,0,10,0).place_center(6,10,10,10,10).place_center(7,20,0,20,0).place_center(5,30,0,30,0).place_center(0,-30,0,-30,-1).place_center(1,0,-10,-1,-11).place_center(3,-20,0,-21,-1).place_center(4,0,-20,-1,-21).place_center(2,-10,0,-11,0).door(1,8,north).door(9,6,north).door(4,1,north).door(2,8,east).door(8,9,east).door(9,7,east).door(7,5,east).door(0,3,east).door(3,2,east).door(9,8,west).door(7,9,west).door(5,7,west).door(3,0,west).door(2,3,west).door(8,2,west).door(6,9,south).door(8,1,south).door(1,4,south).opposite(north,south).opposite(east,west).opposite(west,east).opposite(south,north).side(north).side(south).side(west).side(east).path(1,8,1).path(9,6,1).path(4,1,1).path(2,8,1).path(8,9,1).path(9,7,1).path(7,5,1).path(0,3,1).path(3,2,1).path(9,8,1).path(7,9,1).path(5,7,1).path(3,0,1).path(2,3,1).path(8,2,1).path(6,9,1).path(8,1,1).path(1,4,1).path(4,4,2).path(8,4,2).path(1,1,2).path(2,1,2).path(9,1,2).path(9,9,2).path(1,2,2).path(2,2,2).path(9,2,2).path(3,3,2).path(8,3,2).path(0,0,2).path(2,0,2).path(7,7,2).path(5,9,2).path(8,8,2).path(7,8,2).path(6,8,2).path(0,2,2).path(9,5,2).path(5,5,2).path(8,7,2).path(6,7,2).path(1,9,2).path(2,9,2).path(3,8,2).path(8,6,2).path(7,6,2).path(6,6,2).path(4,8,2).path(1,8,3).path(9,6,3).path(5,6,3).path(1,6,3).path(2,6,3).path(0,8,3).path(2,8,3).path(3,9,3).path(8,9,3).path(4,9,3).path(9,7,3).path(1,7,3).path(2,7,3).path(7,5,3).path(8,5,3).path(6,5,3).path(3,2,3).path(9,8,3).path(5,8,3).path(7,9,3).path(5,7,3).path(3,0,3).path(8,0,3).path(1,3,3).path(2,3,3).path(9,3,3).path(0,3,3).path(8,2,3).path(7,2,3).path(6,2,3).path(4,2,3).path(6,9,3).path(8,1,3).path(7,1,3).path(6,1,3).path(3,1,3).path(4,1,3).path(1,4,3).path(2,4,3).path(9,4,3).path(8,4,4).path(7,4,4).path(6,4,4).path(3,4,4).path(4,4,4).path(1,1,4).path(0,1,4).path(2,1,4).path(9,1,4).path(5,1,4).path(9,9,4).path(1,2,4).path(9,2,4).path(5,2,4).path(2,2,4).path(3,3,4).path(8,3,4).path(7,3,4).path(6,3,4).path(4,3,4).path(1,0,4).path(2,0,4).path(9,0,4).path(0,0,4).path(7,7,4).path(5,9,4).path(7,8,4).path(8,8,4).path(6,8,4).path(0,2,4).path(9,5,4).path(1,5,4).path(2,5,4).path(5,5,4).path(3,7,4).path(8,7,4).path(4,7,4).path(6,7,4).path(1,9,4).path(2,9,4).path(0,9,4).path(3,8,4).path(3,6,4).path(8,6,4).path(4,6,4).path(7,6,4).path(6,6,4).path(4,8,4).path(1,8,5).path(9,6,5).path(5,6,5).path(1,6,5).path(2,6,5).path(0,6,5).path(0,8,5).path(2,8,5).path(3,9,5).path(8,9,5).path(4,9,5).path(9,7,5).path(1,7,5).path(2,7,5).path(0,7,5).path(7,5,5).path(3,5,5).path(8,5,5).path(4,5,5).path(6,5,5).path(3,2,5).path(9,8,5).path(5,8,5).path(7,9,5).path(5,7,5).path(3,0,5).path(8,0,5).path(7,0,5).path(6,0,5).path(4,0,5).path(1,3,5).path(9,3,5).path(5,3,5).path(2,3,5).path(0,3,5).path(8,2,5).path(7,2,5).path(6,2,5).path(4,2,5).path(6,9,5).path(7,1,5).path(8,1,5).path(6,1,5).path(3,1,5).path(4,1,5).path(1,4,5).path(0,4,5).path(2,4,5).path(9,4,5).path(5,4,5).path(7,4,6).path(8,4,6).path(6,4,6).path(3,4,6).path(4,4,6).path(1,1,6).path(0,1,6).path(2,1,6).path(9,1,6).path(5,1,6).path(9,9,6).path(1,2,6).path(9,2,6).path(5,2,6).path(2,2,6).path(3,3,6).path(8,3,6).path(7,3,6).path(6,3,6).path(4,3,6).path(1,0,6).path(9,0,6).path(5,0,6).path(2,0,6).path(0,0,6).path(7,7,6).path(5,9,6).path(7,8,6).path(8,8,6).path(6,8,6).path(0,2,6).path(9,5,6).path(1,5,6).path(2,5,6).path(0,5,6).path(5,5,6).path(3,7,6).path(8,7,6).path(4,7,6).path(6,7,6).path(1,9,6).path(2,9,6).path(0,9,6).path(3,8,6).path(3,6,6).path(8,6,6).path(4,6,6).path(7,6,6).path(6,6,6).path(4,8,6).path(1,8,7).path(9,6,7).path(5,6,7).path(1,6,7).path(2,6,7).path(0,6,7).path(0,8,7).path(2,8,7).path(3,9,7).path(8,9,7).path(4,9,7).path(9,7,7).path(1,7,7).path(2,7,7).path(0,7,7).path(7,5,7).path(3,5,7).path(8,5,7).path(4,5,7).path(6,5,7).path(3,2,7).path(9,8,7).path(5,8,7).path(7,9,7).path(5,7,7).path(3,0,7).path(8,0,7).path(7,0,7).path(6,0,7).path(4,0,7).path(1,3,7).path(9,3,7).path(5,3,7).path(2,3,7).path(0,3,7).path(8,2,7).path(7,2,7).path(6,2,7).path(4,2,7).path(6,9,7).path(7,1,7).path(8,1,7).path(6,1,7).path(3,1,7).path(4,1,7).path(1,4,7).path(0,4,7).path(2,4,7).path(9,4,7).path(5,4,7).path(7,4,8).path(8,4,8).path(6,4,8).path(3,4,8).path(4,4,8).path(1,1,8).path(0,1,8).path(2,1,8).path(9,1,8).path(5,1,8).path(9,9,8).path(1,2,8).path(9,2,8).path(5,2,8).path(2,2,8).path(3,3,8).path(8,3,8).path(7,3,8).path(6,3,8).path(4,3,8).path(1,0,8).path(9,0,8).path(5,0,8).path(2,0,8).path(0,0,8).path(7,7,8).path(5,9,8).path(7,8,8).path(8,8,8).path(6,8,8).path(0,2,8).path(9,5,8).path(1,5,8).path(2,5,8).path(0,5,8).path(5,5,8).path(3,7,8).path(8,7,8).path(4,7,8).path(6,7,8).path(1,9,8).path(2,9,8).path(0,9,8).path(3,8,8).path(3,6,8).path(8,6,8).path(4,6,8).path(7,6,8).path(6,6,8).path(4,8,8).path(1,8,9).path(9,6,9).path(5,6,9).path(1,6,9).path(2,6,9).path(0,6,9).path(0,8,9).path(2,8,9).path(3,9,9).path(8,9,9).path(4,9,9).path(9,7,9).path(1,7,9).path(2,7,9).path(0,7,9).path(7,5,9).path(3,5,9).path(8,5,9).path(4,5,9).path(6,5,9).path(3,2,9).path(9,8,9).path(5,8,9).path(7,9,9).path(5,7,9).path(3,0,9).path(8,0,9).path(7,0,9).path(6,0,9).path(4,0,9).path(1,3,9).path(9,3,9).path(5,3,9).path(2,3,9).path(0,3,9).path(8,2,9).path(7,2,9).path(6,2,9).path(4,2,9).path(6,9,9).path(7,1,9).path(8,1,9).path(6,1,9).path(3,1,9).path(4,1,9).path(1,4,9).path(0,4,9).path(2,4,9).path(9,4,9).path(5,4,9).room(0).room(1).room(2).room(3).room(4).corridor(5).corridor(6).corridor(7).corridor(8).corridor(9).point(0,0).point(10,0).point(10,10).point(20,0).point(30,0).point(-10,0).point(0,-10).point(0,-20).point(-20,0).point(-30,0).corr_center(8,0,0,0,0).corr_center(9,10,0,10,0).corr_center(6,10,10,10,10).corr_center(7,20,0,20,0).corr_center(5,30,0,30,0).room_size(0,3,3,3,3).room_size(1,3,3,3,3).room_size(3,3,3,3,3).room_size(4,3,3,3,3).room_size(2,3,4,3,4).size(3,3).size(3,4).size(3,5).size(4,3).size(4,4).size(4,5).size(5,3).size(5,4).size(5,5).corr_size(5,1,1,1,1).corr_size(6,1,1,1,1).corr_size(7,1,1,1,1).corr_size(8,1,1,1,1).corr_size(9,1,1,1,1).place_size(5,1,1,1,1).place_size(6,1,1,1,1).place_size(7,1,1,1,1).place_size(8,1,1,1,1).place_size(9,1,1,1,1).place_size(0,3,3,3,3).place_size(1,3,3,3,3).place_size(3,3,3,3,3).place_size(4,3,3,3,3).place_size(2,3,4,3,4).pos(0,0).pos(1,0).pos(2,0).pos(0,1).pos(1,1).pos(2,1).pos(0,2).pos(1,2).pos(2,2).pos(0,3).pos(1,3).pos(2,3).room_pos(0,0,0).room_pos(0,1,0).room_pos(0,2,0).room_pos(0,0,1).room_pos(0,1,1).room_pos(0,2,1).room_pos(0,0,2).room_pos(0,1,2).room_pos(0,2,2).room_pos(0,0,3).room_pos(0,1,3).room_pos(0,2,3).room_pos(1,0,0).room_pos(1,1,0).room_pos(1,2,0).room_pos(1,0,1).room_pos(1,1,1).room_pos(1,2,1).room_pos(1,0,2).room_pos(1,1,2).room_pos(1,2,2).room_pos(1,0,3).room_pos(1,1,3).room_pos(1,2,3).room_pos(2,0,0).room_pos(2,1,0).room_pos(2,2,0).room_pos(2,0,1).room_pos(2,1,1).room_pos(2,2,1).room_pos(2,0,2).room_pos(2,1,2).room_pos(2,2,2).room_pos(2,0,3).room_pos(2,1,3).room_pos(2,2,3).room_pos(3,0,0).room_pos(3,1,0).room_pos(3,2,0).room_pos(3,0,1).room_pos(3,1,1).room_pos(3,2,1).room_pos(3,0,2).room_pos(3,1,2).room_pos(3,2,2).room_pos(3,0,3).room_pos(3,1,3).room_pos(3,2,3).room_pos(4,0,0).room_pos(4,1,0).room_pos(4,2,0).room_pos(4,0,1).room_pos(4,1,1).room_pos(4,2,1).room_pos(4,0,2).room_pos(4,1,2).room_pos(4,2,2).room_pos(4,0,3).room_pos(4,1,3).room_pos(4,2,3).num(0).num(10).num(20).num(30).num(40).num(50).num(60).num(70).num(80).num(90).num(100).num(-100).num(-90).num(-80).num(-70).num(-60).num(-50).num(-40).num(-30).num(-20).num(-10).room_center(0,-30,0,-30,-1).room_center(1,0,-10,-1,-11).room_center(3,-20,0,-21,-1).room_center(4,0,-20,-1,-21).room_center(2,-10,0,-11,0).


blind_corr(C):-corridor(C), #count{P : door(C,P,_)}=1.

near(P1,P2,S) :- place_center(P1,X1,Y1,_,_), place_center(P2,X2,Y2,_,_),
                    delta(S,X2-X1,Y2-Y1).

{more_door(P1,P2,S)} :- blind_corr(P1), near(P1,P2,S), #count{ P3 : near(P1,P3,_), door(P1,P3,_), P2!=P3}=0.

%{more_door(R1,R2,S)} :- opposite(S,T), more_door(R2,R1,T).

%:- place_center(P,_,_,_,_), #count{Q : more_door(P,Q,_)}<1.

%:- place_center(P,_,_,_,_), #count{Q : more_door(P,Q,_)}>4.

%:-side(O), place_center(P,_,_,_,_), #count{Q : more_door(P,Q,O)}>1.

%:-side(O), place_center(P,_,_,_,_), #count{Q : more_door(Q,P,O)}>1.

:-blind_corr(P1), #count{P2,S : more_door(P1,P2,S)}<1.

door(P1,P2,S) :- more_door(P1,P2,S).

door(P1,P2,S) :- opposite(S,T), door(P2,P1,T).

#show door/3.

#show more_door/3.

#show near/3.

#show blind_corr/1.