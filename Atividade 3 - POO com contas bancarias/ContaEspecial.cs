using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Atividade_3___POO_com_contas_bancarias
{
    internal class ContaEspecial : ContaCorrente
    {
        protected decimal _limite;
        public decimal Limite { get => _limite; }
        public ContaEspecial() : base(){this._limite = 0; this.TarifaManutencao = CalcularTarifaManutencao();}
        public ContaEspecial(string titular, string numeroConta, decimal limite) : base(titular, numeroConta)
        { this._limite = limite; this.TarifaManutencao = CalcularTarifaManutencao(); }
        public override bool Sacar(decimal valor)
        {
            if (valor <= 0 || valor > (Saldo + _limite)){return false;}

            else
            {
                decimal totalDisponivel = Saldo + Limite;

                if (valor > totalDisponivel)  { return false; }

                this._saldo -= valor;
                return true; 
            }
               
        }

        public override decimal CalcularTarifaManutencao()
        {
            if (Limite <= 1000)
            {
                this.TarifaManutencao = Limite * 0.07m;
                return TarifaManutencao;
            }
            else if (Limite > 1000 && Limite <= 5000)
            {
                this.TarifaManutencao = Limite * 0.05m;
                return TarifaManutencao;
            }
            else
            {
                this.TarifaManutencao = Limite * 0.02m;
                return TarifaManutencao;
            }
        }

        public override string ToString()
        {
            CultureInfo culturaBrasileira = new CultureInfo("pt-BR");
            return $"ContaEspecial(Titular: {Titular}, NumeroConta: {NumeroConta}, Saldo: {Saldo.ToString("C", culturaBrasileira)}, Limite: {Limite.ToString("C", culturaBrasileira)}, Total disponível: {(Saldo + Limite).ToString("C", culturaBrasileira)}, Tarifa de manutenção: {TarifaManutencao.ToString("C", culturaBrasileira)})";
        }
       

        }
}

