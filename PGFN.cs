using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteConversaoCSV
{
    public class PGFN
    {
        [Name("CPF_CNPJ")]
        public string CpfCnpj { get; set; }
        [Name("TIPO_PESSOA")]
        public string TipoPessoa { get; set; }
        [Name("TIPO_DEVEDOR")]
        public string TipoDevedor { get; set; }
        [Name("NOME_DEVEDOR")]
        public string NomeDevedor { get; set; }
        [Name("UF_DEVEDOR")]
        public string UfDevedor { get; set; }
        [Name("UNIDADE_RESPONSAVEL")]
        public string UnidadeResponsável { get; set; }
        [Name("NUMERO_INSCRICAO")]
        public string NumeroInscricao { get; set; }
        [Name("TIPO_SITUACAO_INSCRICAO")]
        public string TipoSituacaoInscricao { get; set; }
        [Name("SITUACAO_INSCRICAO")]
        public string SituacaoInscricao { get; set; }
        [Name("TIPO_CREDITO")]
        public string TipoCredito { get; set; }
        [Name("DATA_INSCRICAO")]
        public string DataInscricao { get; set; }
        [Name("INDICADOR_AJUIZADO")]
        public string IndicadorAjuizado { get; set; }
        [Name("VALOR_CONSOLIDADO")]
        public string ValorConsolidado { get; set; }
    }
}
