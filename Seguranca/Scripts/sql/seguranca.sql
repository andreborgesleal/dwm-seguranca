use seguranca
select * From sistema

select * from Transacao where url like 'Usuarios%' and sistemaId = 1

select b.nomeCurto, a.* from LogAuditoria a join Transacao b on a.transacaoId = b.transacaoId order by 2 desc

delete from LogAuditoria where logId <= 78

select * from Transacao where sistemaId = 1 and transacaoId_pai = 174 or transacaoId = 174

select * from Transacao where sistemaId = 1 and transacaoId_pai is null

select * from Transacao where sistemaId = 1 and transacaoId_pai = 159 or transacaoId = 159