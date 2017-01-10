select * from gameday
select * from game
select * from playergame
select * from gamesResult

create view  teste123 as (
select r.id - 8 as dia, p.Nickname as Peladeiro, r.jogos, r.vitorias, r.empates, r.derrotas, r.gp, r.gc, r.pos, r.ast from (
select gd.id, pg.player_id, count(*) as jogos, count(case pg.[time] when gr.vencedor then 1 else null end) as vitorias, count(case gr.vencedor when 0 then 1 else null end) as empates, count(case when gr.vencedor <> 0 then case when gr.vencedor <> pg.[time] then 1 else null end else null end) as derrotas, sum(pg.gols) as gp, sum(pg.golsContra) as gc, case when pg.goleiro = 1 then 'G' else '' end as pos , sum(pg.assistencias) as ast  from gameday gd inner join gamesresult gr on gd.id = gr.gameday_id inner join playergame pg on pg.game_id = gr.id
group by gd.id, pg.player_id, pg.goleiro
) as r
inner join player p
on p.id = r.player_id)



select * from jogador where dia = 11
order by Peladeiro



select Peladeiro,
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
from jogador
where Pos != 'G'
group by Peladeiro
order by 4 desc

select Peladeiro,
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
from teste123
where Pos != 'G'
group by Peladeiro
order by 4 desc

select * from jogador
select * from teste123

