select * from rankingByYear(2016)
select * from rankingByYearGoalkeeper(2016)
select * from rankingByCurrentSemester()
select * from rankingByCurrentSemesterGoalkeeper()



CREATE FUNCTION rankingByYear (@year INT)
RETURNS TABLE
AS
RETURN
   select Peladeiro, r.player_id,
sum(Vitorias) * 3 + sum(Empates) as Pontos,
CAST(CAST(sum(Vitorias) * 3 + sum(Empates)as decimal(10,3)) / CAST(sum(jogos)as decimal(10,3)) as decimal(10,3)) as MediaPontos,
sum(Gp) as Gp,
sum(ast) as ast,
CAST(CAST(sum(Gp)as decimal(10,3))  / CAST(sum(jogos)as decimal(10,3))as decimal(10,3)) as MediaGols,
sum(jogos) as jogos,
sum(Vitorias) as Vitorias,
sum(Empates) as Empates,
sum(Derrotas) as Derrotas,
sum(gc) as gc,
count(*) as dias
from (
select r.id as dia,r.player_id, p.Nickname as Peladeiro, r.jogos, r.vitorias, r.empates, r.derrotas, r.gp, r.gc, r.pos, r.ast from (
select gd.id, pg.player_id, count(*) as jogos, count(case pg.[time] when gr.vencedor then 1 else null end) as vitorias, count(case gr.vencedor when 0 then 1 else null end) as empates, count(case when gr.vencedor <> 0 then case when gr.vencedor <> pg.[time] then 1 else null end else null end) as derrotas, sum(pg.gols) as gp, sum(pg.golsContra) as gc, case when pg.goleiro = 1 then 'G' else '' end as pos , sum(pg.assistencias) as ast  from gameday gd inner join gamesresult gr on gd.id = gr.gameday_id inner join playergame pg on pg.game_id = gr.id
where year(gd.gameDate) = @year
group by gd.id, pg.player_id, pg.goleiro
) as r
inner join player p
on p.id = r.player_id) as r
where Pos != 'G'
group by Peladeiro, r.player_id

create FUNCTION rankingByYearGoalkeeper (@year INT)
RETURNS TABLE
AS
RETURN
  select Peladeiro, r.player_id, sum(jogos) as jogos, sum(Vitorias) as Vitorias, sum(Empates) as Empates, sum(Derrotas) as Derrotas, sum(gc) as GolsSofridos,
CAST(CAST(sum(GC)as decimal(10,3))  / CAST(sum(jogos)as decimal(10,3))as decimal(10,3)) as MediaGols,
sum(Vitorias) * 3 + sum(Empates) as Pontos,
CAST(CAST(sum(Vitorias) * 3 + sum(Empates)as decimal(10,3)) / CAST(sum(jogos)as decimal(10,3)) as decimal(10,3)) as MediaPontos, count(*) as dias
from (select r.id as dia, p.Id as player_id, p.Nickname as Peladeiro, r.jogos, r.vitorias, r.empates, r.derrotas, r.gp, r.gc, r.pos, r.ast from (
select gd.id, 
pg.player_id, 
count(*) as jogos, 
count(case pg.[time] when gr.vencedor then 1 else null end) as vitorias, 
count(case gr.vencedor when 0 then 1 else null end) as empates, 
count(case when gr.vencedor <> 0 then case when gr.vencedor <> pg.[time] then 1 else null end else null end) as derrotas, 
sum(pg.gols) as gp, 
case when pg.goleiro = 1 then (select golssofridos from getGolsSofridos ggs where ggs.player_id = pg.player_id and ggs.gameday_id = gd.id) else sum(pg.golsContra) end as gc, 
case when pg.goleiro = 1 then 'G' else '' end as pos , 
sum(pg.assistencias) as ast  
from gameday gd inner join gamesresult gr on gd.id = gr.gameday_id inner join playergame pg on pg.game_id = gr.id
where year(gd.gameDate) = @year
group by gd.id, pg.player_id, pg.goleiro
) as r
inner join player p
on p.id = r.player_id
) as r
where Pos = 'G'
group by Peladeiro, r.player_id


CREATE FUNCTION rankingByCurrentSemester ()
RETURNS TABLE
AS
RETURN
   select Peladeiro, r.player_id,
sum(Vitorias) * 3 + sum(Empates) as Pontos,
CAST(CAST(sum(Vitorias) * 3 + sum(Empates)as decimal(10,3)) / CAST(sum(jogos)as decimal(10,3)) as decimal(10,3)) as MediaPontos,
sum(Gp) as Gp,
sum(ast) as ast,
CAST(CAST(sum(Gp)as decimal(10,3))  / CAST(sum(jogos)as decimal(10,3))as decimal(10,3)) as MediaGols,
sum(jogos) as jogos,
sum(Vitorias) as Vitorias,
sum(Empates) as Empates,
sum(Derrotas) as Derrotas,
sum(gc) as gc,
count(*) as dias
from (
select r.id as dia, p.id as player_id, p.Nickname as Peladeiro, r.jogos, r.vitorias, r.empates, r.derrotas, r.gp, r.gc, r.pos, r.ast from (
select gd.id, pg.player_id, count(*) as jogos, count(case pg.[time] when gr.vencedor then 1 else null end) as vitorias, count(case gr.vencedor when 0 then 1 else null end) as empates, count(case when gr.vencedor <> 0 then case when gr.vencedor <> pg.[time] then 1 else null end else null end) as derrotas, sum(pg.gols) as gp, sum(pg.golsContra) as gc, case when pg.goleiro = 1 then 'G' else '' end as pos , sum(pg.assistencias) as ast  from gameday gd inner join gamesresult gr on gd.id = gr.gameday_id inner join playergame pg on pg.game_id = gr.id
where year(gd.gameDate) = year((select max(gameDate) as lastGame from gameday)) and ((month(gd.gameDate) BETWEEN 1 and 6 and month((select max(gameDate) as lastGame from gameday)) BETWEEN 1 and 6) or (month(gd.gameDate) BETWEEN 7 and 12 and month((select max(gameDate) as lastGame from gameday)) BETWEEN 7 and 12))
group by gd.id, pg.player_id, pg.goleiro
) as r
inner join player p
on p.id = r.player_id) as r
where Pos != 'G'
group by Peladeiro, r.player_id

create FUNCTION rankingByCurrentSemesterGoalkeeper ()
RETURNS TABLE
AS
RETURN
  select Peladeiro, r.player_id, sum(jogos) as jogos, sum(Vitorias) as Vitorias, sum(Empates) as Empates, sum(Derrotas) as Derrotas, sum(gc) as GolsSofridos,
CAST(CAST(sum(GC)as decimal(10,3))  / CAST(sum(jogos)as decimal(10,3))as decimal(10,3)) as MediaGols,
sum(Vitorias) * 3 + sum(Empates) as Pontos,
CAST(CAST(sum(Vitorias) * 3 + sum(Empates)as decimal(10,3)) / CAST(sum(jogos)as decimal(10,3)) as decimal(10,3)) as MediaPontos, count(*) as dias
from (select r.id as dia, p.id as player_id, p.Nickname as Peladeiro, r.jogos, r.vitorias, r.empates, r.derrotas, r.gp, r.gc, r.pos, r.ast from (
select gd.id, 
pg.player_id, 
count(*) as jogos, 
count(case pg.[time] when gr.vencedor then 1 else null end) as vitorias, 
count(case gr.vencedor when 0 then 1 else null end) as empates, 
count(case when gr.vencedor <> 0 then case when gr.vencedor <> pg.[time] then 1 else null end else null end) as derrotas, 
sum(pg.gols) as gp, 
case when pg.goleiro = 1 then (select golssofridos from getGolsSofridos ggs where ggs.player_id = pg.player_id and ggs.gameday_id = gd.id) else sum(pg.golsContra) end as gc, 
case when pg.goleiro = 1 then 'G' else '' end as pos , 
sum(pg.assistencias) as ast  
from gameday gd inner join gamesresult gr on gd.id = gr.gameday_id inner join playergame pg on pg.game_id = gr.id
where year(gd.gameDate) = year((select max(gameDate) as lastGame from gameday)) and ((month(gd.gameDate) BETWEEN 1 and 6 and month((select max(gameDate) as lastGame from gameday)) BETWEEN 1 and 6) or (month(gd.gameDate) BETWEEN 7 and 12 and month((select max(gameDate) as lastGame from gameday)) BETWEEN 7 and 12))
group by gd.id, pg.player_id, pg.goleiro
) as r
inner join player p
on p.id = r.player_id
) as r
where Pos = 'G'
group by Peladeiro, r.player_id
