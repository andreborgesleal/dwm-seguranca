use seguranca

delete from GrupoTransacao where grupoId = 3 and transacaoId not in (141, 139, 140, 138, 142)

select * from sistema

select * from Transacao where sistemaId = 1 and url like 'UsuarioGrupo%' order by transacaoId_pai, posicao

select * from GrupoTransacao  where grupoId = 1 


select * from Grupo a join Sistema b on a.sistemaId = b.sistemaId where empresaId = 1

select * From Usuario where nome like '%Paulo%'



select * from Transacao a join GrupoTransacao b on a.transacaoId = b.transacaoId
where a.sistemaId = 1 and grupoId = 1 

select * from Transacao a left outer join GrupoTransacao b on a.transacaoId = b.transacaoId
where (a.sistemaId = 1 and grupoId = 1) or (b.grupoId is null and a.sistemaId = 1)

select * from Transacao where sistemaId = 1

delete GrupoTransacao where grupoId = 1 and transacaoId = 152


select * from Usuario a left outer join UsuarioGrupo b on a.usuarioId = b.usuarioId
where a.empresaId = 1 and ((b.grupoId = 1) or (b.grupoId is null)) and (nome like 'André%' or login = 'andreborgesleal@live.com')

