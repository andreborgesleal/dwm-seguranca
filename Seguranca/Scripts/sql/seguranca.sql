use seguranca
select * From sistema

select * from Transacao where url like 'Usuarios%' and sistemaId = 1

select b.nomeCurto, a.* from LogAuditoria a join Transacao b on a.transacaoId = b.transacaoId order by 2 desc

delete from LogAuditoria where logId <= 78

select * from Transacao where sistemaId = 1 and transacaoId_pai = 174 or transacaoId = 174

select * from Transacao where sistemaId = 1 and transacaoId_pai is null

select * from Transacao where sistemaId = 1 and transacaoId_pai = 159 or transacaoId = 159



select a.transacaoId from Transacao a
where nomeCurto = 'Editar dados do associado' and sistemaId = 2

select grupoId from Grupo where sistemaId = 2 and empresaId = 1 and descricao = 'Associado'

select * from GrupoTransacao where transacaoId = 130 and grupoId = 3
insert into GrupoTransacao(grupoId, transacaoId, situacao) values (@grupoId, @transacaoId, 'A')

