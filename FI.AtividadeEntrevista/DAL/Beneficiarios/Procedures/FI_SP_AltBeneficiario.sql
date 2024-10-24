CREATE PROC FI_SP_AltBeneficiario
    @Id           BIGINT,
	@NOME         VARCHAR (50),
    @CPF          VARCHAR (11),
    @IDCLIENTE    BIGINT
	
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		NOME = @NOME, 
		CPF = @CPF 
	WHERE ID = @Id
END