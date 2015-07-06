using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRMEducacional.Retencao.Teste.Web.Classes
{
    public class Contato
    {
        public virtual int IDContato { get; set; }

        public virtual int IDCliente { get; set; }

        #region Endereço Residencial

        public virtual string Rua { get; set; }

        public virtual string Numero { get; set; }

        public virtual string Complemento { get; set; }

        public virtual string Bairro { get; set; }

        public virtual string Cidade { get; set; }

        public virtual string Estado { get; set; }

        public virtual string CEP { get; set; }

        #endregion

        #region Informações de Contato
        public virtual string NomeCompleto { get; set; }

        public virtual string Email { get; set; }

        public virtual string TelefoneResidencial { get; set; }

        public virtual string TelefoneCelular { get; set; }

        #endregion

        #region Endereço Comercial
        public virtual string RuaComercial { get; set; }

        public virtual string NumeroComercial { get; set; }

        public virtual string ComplementoComercial { get; set; }

        public virtual string BairroComercial { get; set; }

        public virtual string CidadeComercial { get; set; }

        public virtual string EstadoComercial { get; set; }

        public virtual string CEPComercial { get; set; }

        #endregion

        #region Documentação
        public virtual string CPF { get; set; }

        public virtual string RG { get; set; }

        public virtual string OrgaoExpedidorRG { get; set; }

        public virtual string UFRG { get; set; }

        public virtual DateTime? DataEmissaoRG { get; set; }

        public virtual string TituloEleitor { get; set; }

        public virtual DateTime? DtEmissaoTituloEleitor { get; set; }

        public virtual string UFTituloEleitor { get; set; }

        public virtual string NumeroReservista { get; set; }


        #endregion

        #region Informações de Contato
        public virtual DateTime? DataNascimento { get; set; }

        public virtual string Sexo { get; set; }

        public virtual int? EstadoCivil { get; set; }

        public virtual string Nacionalidade { get; set; }

        public virtual string Naturalidade { get; set; }

        public virtual string UFNaturalidade { get; set; }

        public virtual string NomePai { get; set; }

        public virtual string NomeMae { get; set; }

        #endregion

        public virtual int IdentificadorFila { get; set; }
    }
}