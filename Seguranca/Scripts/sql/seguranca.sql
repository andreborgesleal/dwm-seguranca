﻿use seguranca

update Sessao set dt_desativacao = getdate()

select * from GrupoTransacao
select * from Transacao
select * from Sistema
select * from grupo where empresaId = 1

insert into UsuarioGrupo values(1, 1, 'A')

select * from UsuarioGrupo where usuarioId = 1
select * from grupo where grupoId = 1

select * from Transacao where sistemaId = 1 and transacaoId_pai = 152 order by posicao