using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atividade_3___POO_com_contas_bancarias
{
    public class ContaPoupanca : ContaCorrente
    {
        protected decimal _taxaRendimento;
        public decimal TaxaRendimento { get => _taxaRendimento; set => _taxaRendimento = value; }
        public ContaPoupanca() : base(){this._taxaRendimento = 0.1m; this.TarifaManutencao = 10m; }
        public ContaPoupanca(string titular, string numeroConta, decimal taxaRendimento) : base(titular, numeroConta)
        {this._taxaRendimento = taxaRendimento; this.TarifaManutencao = 10m; }

        public override  bool  AplicarRendimento()
        {
            decimal rendimento = _saldo * _taxaRendimento;
            _saldo += rendimento;
            return true;
        }
        public override decimal CalcularTarifaManutencao()
        {
            return TarifaManutencao;
        }
        public override string ToString()
        {
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            return $"ContaPoupanca(Titular: {Titular}, NumeroConta: {NumeroConta}, Saldo: {Saldo.ToString("C", culturaBrasileira)},, TaxaRendimento: {TaxaRendimento}, Tarifa de manutenção: {TarifaManutencao.ToString("C", culturaBrasileira)})";
        }
    }
}
