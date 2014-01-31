use seguranca
select * From sistema

select * from Transacao where url like 'Usuarios%' and sistemaId = 1

select b.nomeCurto, a.* from LogAuditoria a join Transacao b on a.transacaoId = b.transacaoId